using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CrossSceneCarryManager : MonoBehaviour
{
    private static CrossSceneCarryManager instance;

    public static CrossSceneCarryManager Instance
    {
        get
        {
            if (instance == null)
                new GameObject(nameof(CrossSceneCarryManager)).AddComponent<CrossSceneCarryManager>();
            return instance;
        }
    }

    private sealed class CarriedObject
    {
        public XRGrabInteractable grab;
        public bool leftHand;
        public bool throwOnDetach;
    }

    private readonly List<CarriedObject> pending = new List<CarriedObject>();
    private readonly HashSet<XRGrabInteractable> carriedObjects = new HashSet<XRGrabInteractable>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (instance != this)
            return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        instance = null;
    }

    public void CaptureHeldObjects(XRBaseInteractor leftInteractor, XRBaseInteractor rightInteractor)
    {
        CaptureHand(leftInteractor, true);
        CaptureHand(rightInteractor, false);
    }

    private void CaptureHand(XRBaseInteractor interactor, bool leftHand)
    {
        if (interactor == null)
            return;

        XRGrabInteractable grab = null;
        foreach (var selected in interactor.interactablesSelected)
        {
            grab = selected as XRGrabInteractable;
            if (grab != null)
                break;
        }

        if (grab == null)
            return;

        foreach (var item in pending)
            if (item.grab == grab)
                return;

        var carried = new CarriedObject
        {
            grab = grab,
            leftHand = leftHand,
            throwOnDetach = grab.throwOnDetach
        };

        // End every old selection, including a second hand, before its rig is destroyed.
        grab.throwOnDetach = false;
        while (grab.interactorsSelecting.Count > 0)
        {
            var selecting = grab.interactorsSelecting[0];
            grab.interactionManager.SelectExit(selecting, (IXRSelectInteractable)grab);
        }

        // Unregister from the old scene's interaction manager during transit.
        grab.enabled = false;
        grab.transform.SetParent(null, true);
        ClearVelocity(grab);
        DontDestroyOnLoad(grab.gameObject);
        carriedObjects.Add(grab);
        pending.Add(carried);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pending.Count > 0)
            StartCoroutine(RestoreHeldObjects(scene));
    }

    private IEnumerator RestoreHeldObjects(Scene scene)
    {
        // Allow the new rig and interaction manager to finish their Start methods.
        yield return null;

        SceneCarryTargets targets = null;
        foreach (var root in scene.GetRootGameObjects())
        {
            targets = root.GetComponentInChildren<SceneCarryTargets>();
            if (targets != null)
                break;
        }

        foreach (var carried in pending)
        {
            var grab = carried.grab;
            if (grab == null)
                continue;

            // Once delivered, normal scene ownership ensures dropped objects unload too.
            grab.transform.SetParent(null, true);
            SceneManager.MoveGameObjectToScene(grab.gameObject, scene);
            var hand = targets == null ? null :
                (carried.leftHand ? targets.leftInteractor : targets.rightInteractor);

            if (hand != null && hand.isActiveAndEnabled && hand.interactionManager != null)
            {
                var attach = hand.GetAttachTransform(grab);
                grab.transform.SetPositionAndRotation(attach.position, attach.rotation);
                ClearVelocity(grab);
                grab.interactionManager = hand.interactionManager;
                grab.enabled = true;
                hand.interactionManager.SelectEnter((IXRSelectInteractor)hand, (IXRSelectInteractable)grab);
            }
            else
            {
                grab.interactionManager = null;
                grab.enabled = true;
                Debug.LogWarning("Carried object could not be reattached: assign active SceneCarryTargets hands.", grab);
            }

            grab.throwOnDetach = carried.throwOnDetach;
        }

        pending.Clear();
        carriedObjects.RemoveWhere(grab => grab == null);
    }

    private static void ClearVelocity(XRGrabInteractable grab)
    {
        if (grab.TryGetComponent<Rigidbody>(out var body) && !body.isKinematic)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }

    public void ClearCarriedObjects()
    {
        StopAllCoroutines();
        foreach (var grab in carriedObjects)
        {
            if (grab == null)
                continue;

            grab.throwOnDetach = false;
            grab.gameObject.SetActive(false);
            Destroy(grab.gameObject);
        }

        pending.Clear();
        carriedObjects.Clear();
    }
}

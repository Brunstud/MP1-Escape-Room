using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PhysicsRoomTimeMachineGate : MonoBehaviour
{
    [Header("Machine")]
    public GameObject glassCover;
    public XRSimpleInteractable machineInteractable;

    [Header("Transition")]
    public GameObject transferCanvas;
    public float transitionDelay = 2f;
    public string nextSceneName = "02_PrincipalOffice";

    private bool unlocked = false;
    private bool travelling = false;

    private void Start()
    {
        if (machineInteractable != null)
            machineInteractable.enabled = false;

        if (transferCanvas != null)
            transferCanvas.SetActive(false);
    }

    // Called when all three capsule locks are completed
    public void UnlockMachine()
    {
        if (unlocked)
            return;

        unlocked = true;

        if (glassCover != null)
            glassCover.SetActive(false);

        if (machineInteractable != null)
            machineInteractable.enabled = true;

        Debug.Log("Physics Room Time Machine unlocked.");
    }

    // Called when player activates the time machine
    public void UseTimeMachine()
    {
        if (!unlocked || travelling)
            return;

        travelling = true;

        if (transferCanvas != null)
            transferCanvas.SetActive(true);

        StartCoroutine(Travel());
    }

    private IEnumerator Travel()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}
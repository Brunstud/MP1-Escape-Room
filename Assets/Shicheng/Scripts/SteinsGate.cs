using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TimeMachineGate : MonoBehaviour
{
    public TimeMachineProgress progress;

    public GameObject xrOrigin;

    public GameObject leftHandInteractor;
    public GameObject rightHandInteractor;

    private bool travelling = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }


    // -------------------------
    // Use Time Machine
    // -------------------------

    public void UseTimeMachine()
    {
        if (travelling)
            return;


        // Time Machine must be completely ready
        if (!progress.MachineReady())
        {
            Debug.Log("Time Machine is not ready.");
            return;
        }


        // Find the next Scene
        int currentSceneIndex =
            SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex =
            currentSceneIndex + 1;


        // Make sure there is a next Scene
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("No next scene.");
            return;
        }


        travelling = true;


        // Keep objects currently held by the player
        KeepHeldObject(leftHandInteractor);
        KeepHeldObject(rightHandInteractor);


        // Keep XR player between Scenes
        DontDestroyOnLoad(xrOrigin);


        // Move player to PlayerSpawn after new Scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;


        // Load the next Scene
        SceneManager.LoadScene(nextSceneIndex);
    }


    // -------------------------
    // Keep held objects
    // -------------------------

    private void KeepHeldObject(GameObject handObject)
    {
        if (handObject == null)
            return;


        XRBaseInteractor interactor =
            handObject.GetComponent<XRBaseInteractor>();

        if (interactor == null)
            return;


        if (interactor.interactablesSelected.Count == 0)
            return;


        GameObject heldObject =
            interactor.interactablesSelected[0]
            .transform.gameObject;


        DontDestroyOnLoad(heldObject);
    }


    // -------------------------
    // New Scene loaded
    // -------------------------

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;


        GameObject spawn =
            GameObject.Find("PlayerSpawn");


        if (spawn != null)
        {
            xrOrigin.transform.position =
                spawn.transform.position;

            xrOrigin.transform.rotation =
                spawn.transform.rotation;
        }
        else
        {
            Debug.Log("PlayerSpawn not found.");
        }
    }
}
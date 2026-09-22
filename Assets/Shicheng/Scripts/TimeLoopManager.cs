using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using System.Collections;

public class TimeLoopManager : MonoBehaviour
{
    // -------------------------
    // Audio
    // -------------------------

    public AudioSource musicSource;
    public AudioSource metronomeSource;

    // Music loops at 1:40
    public float loopTime = 100f;


    // -------------------------
    // Timer
    // -------------------------

    public CountdownTimer countdownTimer;


    // -------------------------
    // Player
    // -------------------------

    public GameObject xrOrigin;

    // Where the player returns after a time loop
    public Transform PlayerSpawn;

    // Where the player goes after losing
    public Transform SecretRoomSpawn;


    // -------------------------
    // Failure UI
    // -------------------------

    public GameObject failurePanel;


    // -------------------------
    // Internal state
    // -------------------------

    private float previousMusicTime = 0f;

    private bool rewinding = false;
    private bool gameFailed = false;

    private static bool resetPlayerToStart = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (failurePanel != null)
            failurePanel.SetActive(false);


        FindXROrigin();


        if (musicSource != null)
            previousMusicTime = musicSource.time;


        // Scene was reloaded by rewind or restart
        if (resetPlayerToStart)
        {
            resetPlayerToStart = false;

            StartCoroutine(MovePlayerToStart());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (rewinding || gameFailed)
            return;

        if (musicSource == null || metronomeSource == null)
            return;


        float currentMusicTime = musicSource.time;


        // Case 1:
        // Music clip is longer than 1:40,
        // and playback directly crosses 100 seconds
        bool reachedLoopTime =
            previousMusicTime < loopTime &&
            currentMusicTime >= loopTime;


        // Case 2:
        // Music itself is exactly about 1:40 and loops
        // from approximately 100 seconds back to 0
        bool musicLooped =
            currentMusicTime < previousMusicTime &&
            previousMusicTime >= loopTime - 1f;


        if ((reachedLoopTime || musicLooped) &&
            metronomeSource.isPlaying)
        {
            RewindRoom();
            return;
        }


        previousMusicTime = currentMusicTime;
    }


    // -------------------------
    // Time Loop
    // -------------------------

    private void RewindRoom()
    {
        if (rewinding)
            return;

        rewinding = true;


        // Player should return to the beginning
        // after this Scene reloads
        resetPlayerToStart = true;


        // Reloading the Scene resets
        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }


    // -------------------------
    // Game Over
    // -------------------------

    public void GameOver()
    {
        if (gameFailed)
            return;

        gameFailed = true;


        FindXROrigin();


        // Stop the time-loop audio
        if (musicSource != null)
            musicSource.Pause();

        if (metronomeSource != null)
            metronomeSource.Pause();


        // Move player into Secret Room
        if (xrOrigin != null && SecretRoomSpawn != null)
        {
            xrOrigin.transform.position = SecretRoomSpawn.position;
            xrOrigin.transform.rotation = SecretRoomSpawn.rotation;
        }


        // Show failure screen
        if (failurePanel != null)
            failurePanel.SetActive(true);
    }


    // -------------------------
    // Restart
    // -------------------------

    public void RestartRoom()
    {
        resetPlayerToStart = true;


        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }


    // -------------------------
    // Move Player
    // -------------------------

    private IEnumerator MovePlayerToStart()
    {
        // Wait one frame so the Scene and XR Origin
        // finish loading
        yield return null;

        FindXROrigin();

        if (xrOrigin == null || PlayerSpawn == null)
            yield break;

        xrOrigin.transform.position = PlayerSpawn.position;
        xrOrigin.transform.rotation = PlayerSpawn.rotation;
    }


    // -------------------------
    // Find XR Origin
    // -------------------------

    private void FindXROrigin()
    {
        if (xrOrigin != null)
            return;


        XROrigin origin =
            FindFirstObjectByType<XROrigin>();


        if (origin != null)
            xrOrigin = origin.gameObject;
    }
}
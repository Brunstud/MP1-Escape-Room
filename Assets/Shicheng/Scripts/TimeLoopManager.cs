using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using System.Collections;
using TMPro;

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
    public float initialTimeLimit = 180f;
    public float timePenalty = 30f;
    public float pitchIncreasePerLoop = 0.08f;
    public float maxAudioPitch = 1.5f;

    public float CurrentTimeLimit => currentTimeLimit;
    public int RewindCount => loopCount;


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
    public TMP_Text failureTitleText;
    public TMP_Text failureBodyText;
    public TMP_Text failureHintText;
    public TMP_Text nextWindowText;
    public GameObject reEnterLoopButton;
    public GameObject restartExperimentButton;


    // -------------------------
    // Internal state
    // -------------------------

    private float previousMusicTime = 0f;

    private bool rewinding = false;
    private bool gameFailed = false;

    private static bool resetPlayerToStart = false;
    private static float currentTimeLimit = -1f;
    private static int loopCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!resetPlayerToStart)
        {
            currentTimeLimit = initialTimeLimit;
            loopCount = 0;
        }

        if (countdownTimer != null)
            countdownTimer.SetTimeLimit(currentTimeLimit);

        float pitch = Mathf.Min(1f + loopCount * pitchIncreasePerLoop, maxAudioPitch);
        if (musicSource != null)
            musicSource.pitch = pitch;
        if (metronomeSource != null)
            metronomeSource.pitch = pitch;

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

        if (!ConsumeRegression())
            return;


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
        if (gameFailed || rewinding)
            return;

        if (GetNextTimeLimit() <= 0f)
        {
            ShowPermanentFailure();
            return;
        }

        bool metronomeWasPlaying =
            metronomeSource != null && metronomeSource.isPlaying;

        UpdateFailureText(metronomeWasPlaying);

        ShowFailurePanel();
    }

    private void ShowFailurePanel()
    {
        gameFailed = true;
        if (countdownTimer != null)
            countdownTimer.StopTimer();

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
        if (rewinding)
            return;

        rewinding = true;
        if (!ConsumeRegression())
            return;
        resetPlayerToStart = true;


        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }


    // -------------------------
    // Temporal Compression
    // -------------------------

    public void RestartWholeGame()
    {
        currentTimeLimit = -1f;
        loopCount = 0;
        resetPlayerToStart = false;
        GameSessionData.ResetResults();
        GameSessionData.LabMode = LabEntryMode.Initial;
        CrossSceneCarryManager.Instance.ClearCarriedObjects();
        SceneManager.LoadScene("00_AIONLab");
    }

    private float GetNextTimeLimit()
    {
        return Mathf.Max(0f, currentTimeLimit - timePenalty);
    }

    private string FormatTime(float seconds)
    {
        int totalSeconds = Mathf.CeilToInt(Mathf.Max(0f, seconds));
        int minutes = totalSeconds / 60;
        int secs = totalSeconds % 60;
        return minutes.ToString("00") + ":" + secs.ToString("00");
    }

    private bool ConsumeRegression()
    {
        float next = GetNextTimeLimit();
        if (next <= 0f)
        {
            currentTimeLimit = 0f;
            ShowPermanentFailure();
            return false;
        }

        currentTimeLimit = next;
        loopCount++;
        return true;
    }

    private void UpdateFailureText(bool metronomeWasPlaying)
    {
        if (failureTitleText != null)
            failureTitleText.text = "TEMPORAL WINDOW EXPIRED";

        if (failureBodyText != null)
        {
            failureBodyText.text =
                "SUBJECTIVE TIME EXPIRED\n\n" +
                "The reconstructed memory can no longer\n" +
                "bhold this timeline.\n\n" +
                "Every return destabilizes the anchor\n" +
                "and compresses the next temporal window.";
        }

        if (nextWindowText != null)
            nextWindowText.text = "NEXT WINDOW\n" + FormatTime(GetNextTimeLimit());
        if (reEnterLoopButton != null)
            reEnterLoopButton.SetActive(true);
        if (restartExperimentButton != null)
            restartExperimentButton.SetActive(false);

        if (failureHintText == null)
            return;

        if (metronomeWasPlaying)
        {
            failureHintText.text =
                "AION FIELD NOTE\n\n" +
                "The metronome is still sustaining the regression.\n" +
                "Break the rhythm before the melody returns.";
        }
        else
        {
            failureHintText.text =
                "AION FIELD NOTE\n\n" +
                "The return loop has been disrupted.\n" +
                "Use the remaining window to finish reconstructing\n" +
                "the Time Machine.";
        }
    }

    private void ShowPermanentFailure()
    {
        // Prevent any further internal retries, including queued button clicks.
        rewinding = true;
        if (failureTitleText != null)
            failureTitleText.text = "TEMPORAL ANCHOR LOST";
        if (failureBodyText != null)
        {
            failureBodyText.text =
                "RETURN TO AION FAILED\n\n" +
                "ST. DYMPHNA ASYLUM\n" +
                "INCIDENT RECORD — 1926\n" +
                "The AION supervisor failed to complete the regression\n" +
                "and was discovered by a search party in the restricted wing.\n" +
                "Unable to explain where they came from or provide\n" +
                "a verifiable identity, the subject was detained on suspicion\n" +
                "of involvement in St. Dymphna Asylum's secret experiments.\n" +
                "No record of their release was found.\n\n" +
                "STRANDED IN 1926.";
        }
        if (failureHintText != null)
            failureHintText.text = "";
        if (nextWindowText != null)
            nextWindowText.text = "RETURN WINDOW\n00:00";
        if (reEnterLoopButton != null)
            reEnterLoopButton.SetActive(false);
        if (restartExperimentButton != null)
            restartExperimentButton.SetActive(true);

        ShowFailurePanel();
        if (musicSource != null)
            musicSource.Stop();
        if (metronomeSource != null)
            metronomeSource.Stop();
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

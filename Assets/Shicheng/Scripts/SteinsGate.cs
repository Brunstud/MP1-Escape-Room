using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;

public class TimeMachineGate : MonoBehaviour
{
    public TimeMachineProgress progress;
    public CountdownTimer countdownTimer;
    public CollectibleManager collectibleManager;
    public XRBaseInteractor leftInteractor;
    public XRBaseInteractor rightInteractor;

    // NEW
    public GameObject winCanvas;
    public float transitionDelay = 2f;

    private bool travelling = false;

    public void UseTimeMachine()
    {
        if (travelling)
            return;

        if (!progress.MachineReady())
        {
            Debug.Log("Time Machine is not ready.");
            return;
        }

        travelling = true;

        // Save results before the transition delay
        GameSessionData.ResetResults();

        if (countdownTimer != null)
        {
            GameSessionData.PrincipalRemainingTime =
                countdownTimer.TimeLeft;

            // Freeze final score while transition UI is shown
            countdownTimer.StopTimer();
        }

        if (collectibleManager != null)
        {
            GameSessionData.PrincipalCollectedCount =
                collectibleManager.CollectedCount;

            GameSessionData.PrincipalTotalCollectibles =
                collectibleManager.TotalCollectibles;
        }

        GameSessionData.LabMode = LabEntryMode.FinalWin;

        // Show transition UI
        if (winCanvas != null)
            winCanvas.SetActive(true);

        StartCoroutine(TravelToAION());
    }

    private IEnumerator TravelToAION()
    {
        yield return new WaitForSeconds(transitionDelay);

        CrossSceneCarryManager.Instance.CaptureHeldObjects(
            leftInteractor,
            rightInteractor
        );

        SceneManager.LoadScene("00_AIONLab");
    }
}
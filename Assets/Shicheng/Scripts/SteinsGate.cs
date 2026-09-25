using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TimeMachineGate : MonoBehaviour
{
    public TimeMachineProgress progress;
    public CountdownTimer countdownTimer;
    public CollectibleManager collectibleManager;
    public XRBaseInteractor leftInteractor;
    public XRBaseInteractor rightInteractor;

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
        GameSessionData.ResetResults();
        if (countdownTimer != null)
            GameSessionData.PrincipalRemainingTime = countdownTimer.TimeLeft;

        if (collectibleManager != null)
        {
            GameSessionData.PrincipalCollectedCount = collectibleManager.CollectedCount;
            GameSessionData.PrincipalTotalCollectibles = collectibleManager.TotalCollectibles;
        }

        GameSessionData.LabMode = LabEntryMode.FinalWin;
        CrossSceneCarryManager.Instance.CaptureHeldObjects(leftInteractor, rightInteractor);
        SceneManager.LoadScene("00_AIONLab");
    }
}

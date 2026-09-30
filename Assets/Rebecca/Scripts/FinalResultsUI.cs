using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalResultsUI : MonoBehaviour
{
    public TMP_Text resultsText;

    private bool restarting;

    private void OnEnable()
    {
        if (resultsText == null)
            return;

        int seconds = Mathf.FloorToInt(Mathf.Max(0f, GameSessionData.PrincipalRemainingTime));
        resultsText.text =
            "MISSION COMPLETE\n\nPRINCIPAL'S OFFICE RESULTS\n\nTIME REMAINING\n" +
            $"{seconds / 60:00}:{seconds % 60:00}\n\nEVIDENCE RECOVERED\n" +
            $"{GameSessionData.PrincipalCollectedCount} / {GameSessionData.PrincipalTotalCollectibles}";
    }

    public void RestartGame()
    {
        if (restarting)
            return;

        restarting = true;
        GameSessionData.ResetResults();
        GameSessionData.LabMode = LabEntryMode.Restart;
        CrossSceneCarryManager.Instance.ClearCarriedObjects();
        SceneManager.LoadScene("00_AIONLab");
    }
}

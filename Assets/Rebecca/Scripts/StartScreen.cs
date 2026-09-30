using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject finalWinCanvas;

    void Start()
    {
        LabEntryMode mode = GameSessionData.LabMode;

        if (startScreen != null)
            startScreen.SetActive(mode == LabEntryMode.Initial);

        if (finalWinCanvas != null)
            finalWinCanvas.SetActive(mode == LabEntryMode.FinalWin);

        GameSessionData.LabMode = LabEntryMode.Initial;
    }

    public void BeginExperiment()
    {
        if (startScreen != null)
            startScreen.SetActive(false);
    }
}

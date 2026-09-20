using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;

    public void BeginExperiment()
    {
        startScreen.SetActive(false);
    }
}
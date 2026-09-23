using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;

    public void BeginExperiment()
    {
        Debug.Log("Button clicked");
        startScreen.SetActive(false);
    }
}
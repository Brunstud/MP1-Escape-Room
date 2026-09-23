using UnityEngine;

public class NavPuzzle : MonoBehaviour
{
    private int currentStep = 0;

    public GameObject panel;

    public void PressButton(int buttonNumber)
    {
        if (currentStep == 0 && buttonNumber == 3)
        {
            currentStep = 1;
            Debug.Log("Correct! Step 1");
        }
        else if (currentStep == 1 && buttonNumber == 1)
        {
            currentStep = 2;
            Debug.Log("Correct! Step 2");
        }
        else if (currentStep == 2 && buttonNumber == 2)
        {
            currentStep = 3;
            Debug.Log("NAV PUZZLE COMPLETE!");
            panel.SetActive(false);
        }
        else
        {
            currentStep = 0;
            Debug.Log("Wrong sequence. Try again.");
        }
    }
}
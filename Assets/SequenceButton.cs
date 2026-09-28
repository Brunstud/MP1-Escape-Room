using UnityEngine;

public class SequenceButton : MonoBehaviour
{
    [Tooltip("Unique ID matching the numbers in the Manager's correct sequence")]
    public int buttonID;

    public ButtonSequenceManager sequenceManager;

    // Call this method when the player interacts with or presses this button
    public void PressButton()
    {
        if (sequenceManager != null)
        {
            sequenceManager.RegisterButtonPressed(buttonID);
        }
    }
}
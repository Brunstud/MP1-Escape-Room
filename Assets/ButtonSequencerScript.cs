using System.Collections.Generic;
using UnityEngine;

public class ButtonSequenceManager : MonoBehaviour
{
    [Header("Sequence Setup")]
    [Tooltip("The correct order of button IDs, e.g., 0, 1, 2, 3")]
    public List<int> correctSequence = new List<int> { 1, 0, 1, 3 };

    [Header("Target Spawn")]
    public GameObject keyPrefab;
    public Transform spawnLocation;

    private List<int> currentInputSequence = new List<int>();
    private bool isSolved = false;

    // Call this method whenever any button in the sequence is pressed
    public void RegisterButtonPressed(int buttonID)
    {
        if (isSolved) return;

        currentInputSequence.Add(buttonID);

        // Check if the current press matches the expected step in sequence
        int currentIndex = currentInputSequence.Count - 1;

        if (currentInputSequence[currentIndex] != correctSequence[currentIndex])
        {
            // Wrong button pressed — reset input
            Debug.Log("Wrong sequence! Resetting...");
            ResetSequence();
            return;
        }

        // Check if the full sequence has been entered correctly
        if (currentInputSequence.Count == correctSequence.Count)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        isSolved = true;
        Debug.Log("Sequence Correct! Spawning Key...");

        if (keyPrefab != null && spawnLocation != null)
        {
            Instantiate(keyPrefab, spawnLocation.position, spawnLocation.rotation);
        }
    }

    public void ResetSequence()
    {
        currentInputSequence.Clear();
    }
}
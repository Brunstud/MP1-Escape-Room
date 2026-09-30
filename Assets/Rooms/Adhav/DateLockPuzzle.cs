using UnityEngine;
using TMPro;

public class DateLockPuzzle : MonoBehaviour
{
    [Header("Puzzle Solution Setup")]
    [Tooltip("The correct date code sequence, e.g., 0527 for May 27th or 1024 for Oct 24th.")]
    public string correctDateCode = "0527";

    [Header("UI & Display")]
    public TextMeshProUGUI displayScreen;
    public string defaultPrompt = "MMDD";

    [Header("Reward & Spawn Point")]
    public GameObject keyPrefab;
    public Transform keySpawnPoint;
    public ParticleSystem spawnParticles;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private string currentInput = "";
    private bool isUnlocked = false;

    private void Start()
    {
        UpdateDisplay();
    }

    /// <summary>
    /// Call this method from UI Buttons or XR Poke Buttons on the keypad.
    /// </summary>
    public void PressDigit(string digit)
    {
        if (isUnlocked) return;

        if (currentInput.Length < correctDateCode.Length)
        {
            currentInput += digit;
            UpdateDisplay();
        }

        if (currentInput.Length == correctDateCode.Length)
        {
            CheckCode();
        }
    }

    public void ClearInput()
    {
        if (isUnlocked) return;
        currentInput = "";
        UpdateDisplay();
    }

    private void CheckCode()
    {
        if (currentInput == correctDateCode)
        {
            UnlockPuzzle();
        }
        else
        {
            if (audioSource && wrongSound) audioSource.PlayOneShot(wrongSound);
            displayScreen.text = "ERROR";
            Invoke(nameof(ClearInput), 1.2f);
        }
    }

    private void UnlockPuzzle()
    {
        isUnlocked = true;
        displayScreen.text = "OPEN";

        if (audioSource && correctSound) audioSource.PlayOneShot(correctSound);
        if (spawnParticles) spawnParticles.Play();

        // Spawn the 3rd Key
        if (keyPrefab && keySpawnPoint)
        {
            Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
        }
    }

    private void UpdateDisplay()
    {
        if (displayScreen)
        {
            displayScreen.text = string.IsNullOrEmpty(currentInput) ? defaultPrompt : currentInput;
        }
    }
}
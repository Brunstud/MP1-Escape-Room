using UnityEngine;
using UnityEngine.InputSystem;

public class SpatialSoundCycle : MonoBehaviour
{
    public InputActionReference playNextAction;
    public AudioSource[] audioSources;

    private int currentIndex = 0;

    private void OnEnable()
    {
        playNextAction.action.Enable();
        playNextAction.action.performed += PlayNextSound;
    }

    private void OnDisable()
    {
        playNextAction.action.performed -= PlayNextSound;
        playNextAction.action.Disable();
    }

    private void PlayNextSound(InputAction.CallbackContext context)
    {
        if (audioSources == null || audioSources.Length == 0)
            return;

        foreach (AudioSource source in audioSources)
        {
            if (source != null)
                source.Stop();
        }

        AudioSource currentSource = audioSources[currentIndex];

        if (currentSource != null)
        {
            currentSource.Play();

            Debug.Log(
                "Playing Spatial Sound " +
                (currentIndex + 1) +
                "/" +
                audioSources.Length +
                ": " +
                currentSource.gameObject.name
            );
        }

        currentIndex++;
        if (currentIndex >= audioSources.Length)
            currentIndex = 0;
    }
}
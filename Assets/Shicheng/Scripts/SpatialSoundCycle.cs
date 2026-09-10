using UnityEngine;
using UnityEngine.InputSystem;

public class SpatialSoundCycle : MonoBehaviour
{
    public InputActionReference playNextAction;

    public AudioSource[] audioSources;
    public ParticleSystem[] particleSystems;

    private int currentIndex = 0;

    private void OnEnable()
    {
        playNextAction.action.Enable();
        playNextAction.action.performed += PlayNextEffect;
    }

    private void OnDisable()
    {
        playNextAction.action.performed -= PlayNextEffect;
        playNextAction.action.Disable();
    }

    private void PlayNextEffect(InputAction.CallbackContext context)
    {
        if (audioSources == null || audioSources.Length == 0)
            return;

        // Stop all sounds
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
                source.Stop();
        }

        // Stop all particle effects
        foreach (ParticleSystem particle in particleSystems)
        {
            if (particle != null)
            {
                particle.Stop();
                particle.Clear();
            }
        }

        // Play current sound
        AudioSource currentSource = audioSources[currentIndex];

        if (currentSource != null)
        {
            currentSource.Play();
        }

        // Play current particle effect
        if (particleSystems != null &&
            currentIndex < particleSystems.Length)
        {
            ParticleSystem currentParticle = particleSystems[currentIndex];

            if (currentParticle != null)
            {
                currentParticle.Play();
            }
        }

        Debug.Log(
            "Playing Effect " +
            (currentIndex + 1) +
            "/" +
            audioSources.Length
        );

        currentIndex++;

        if (currentIndex >= audioSources.Length)
            currentIndex = 0;
    }
}
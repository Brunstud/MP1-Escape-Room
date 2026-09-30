using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    public AudioSource audioSource;

    private bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Toggle()
    {
        if (audioSource == null)
            return;

        if (!paused)
        {
            audioSource.Pause();
            paused = true;
        }
        else
        {
            audioSource.UnPause();
            paused = false;
        }
    }
}
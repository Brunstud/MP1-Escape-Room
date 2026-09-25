using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 180f;

    public TMP_Text timerText;

    public UnityEvent onTimeUp;

    private float timeLeft;
    public float TimeLeft => timeLeft;
    private bool timerRunning = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = startTime;
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerRunning = false;

                UpdateTimerText();
                TimeUp();
            }
            else
            {
                UpdateTimerText();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void TimeUp()
    {
        Debug.Log("Time is up!");

        onTimeUp.Invoke();
    }

    public void ResetTimer()
    {
        timeLeft = startTime;
        timerRunning = true;

        UpdateTimerText();
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }
}

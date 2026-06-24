using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime;
    private bool timerRunning = true;

    void Update()
    {
        if (!timerRunning)
            return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);

        timerText.text =
            string.Format(
                "{0:00}:{1:00}.{2:00}",
                minutes,
                seconds,
                milliseconds);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public float GetTime()
    {
        return elapsedTime;
    }
}
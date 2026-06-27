using System.Collections;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    public static bool RoundRunning = true;

    [Header("Countdown")]
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TMP_Text countdownText;

    [Header("Current Level Timer (Count Up)")]
    [SerializeField] private TMP_Text timerText;

    [Header("Future Levels (Countdown Timer)")]
    // [SerializeField] private float roundDuration = 120f;

    private float currentTime;
    private bool timerStarted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        RoundRunning = false;
        timerStarted = false;
    }

    private void Start()
    {
        currentTime = 0f;

        if (countdownPanel != null)
            countdownPanel.SetActive(false);

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
            timerText.text = "00:00";
        }
    }

    public void StartCountdown()
    {
        StopAllCoroutines();

        RoundRunning = false;
        timerStarted = false;
        currentTime = 0f;

        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        countdownPanel.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownPanel.SetActive(false);

        currentTime = 0f;

        if (timerText != null)
        {
            timerText.text = "00:00";
            timerText.gameObject.SetActive(true);
        }

        timerStarted = true;
        RoundRunning = true;
    }

  private int displayedSecond = -1;

private void Update()
{
    if (!timerStarted)
        return;

    if (!RoundManager.RoundRunning)
        return;

    currentTime += Time.deltaTime;

    int currentSecond = Mathf.FloorToInt(currentTime);

    // Only update the UI when the displayed second changes
    if (currentSecond == displayedSecond)
        return;

    displayedSecond = currentSecond;

    if (timerText != null)
    {
        int minutes = currentSecond / 60;
        int seconds = currentSecond % 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

    public void EndRound()
    {
        RoundRunning = false;
        timerStarted = false;

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        Debug.Log("ROUND OVER");
    }

    public void PauseRound()
    {
        RoundRunning = false;
    }

    public void ResumeRound()
    {
        RoundRunning = true;
    }

    public float GetElapsedTime()
    {
        return currentTime;
    }
}
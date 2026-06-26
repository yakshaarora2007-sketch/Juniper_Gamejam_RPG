using System.Collections;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    public static bool RoundRunning = false;

    [Header("Countdown")]
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TMP_Text countdownText;

    [Header("Current Level Timer (Count Up)")]
    [SerializeField] private TMP_Text timerText;

    [Header("Future Levels (Countdown Timer)")]
    // [SerializeField] private float roundDuration = 120f;

    private float currentTime;

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
    }

    private void Start()
    {
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
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        RoundRunning = false;

        currentTime = 0f;

        if (countdownPanel != null)
            countdownPanel.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "FIGHT!";
        yield return new WaitForSeconds(0.75f);

        if (countdownPanel != null)
            countdownPanel.SetActive(false);

        if (timerText != null)
            timerText.gameObject.SetActive(true);

        RoundRunning = true;
    }

    private void Update()
    {
        if (!RoundRunning)
            return;

        // ---------- CURRENT LEVEL ----------
        // Count UP timer
        currentTime += Time.deltaTime;

        // ---------- FUTURE LEVELS ----------
        // Uncomment these lines and comment the line above
        //
        // currentTime -= Time.deltaTime;
        //
        // if(currentTime <= 0f)
        // {
        //     currentTime = 0f;
        //     EndRound();
        // }

        if (timerText != null)
        {
            int minutes =
                Mathf.FloorToInt(currentTime / 60f);

            int seconds =
                Mathf.FloorToInt(currentTime % 60f);

            timerText.text =
                string.Format(
                    "{0:00}:{1:00}",
                    minutes,
                    seconds);
        }
    }

    public void EndRound()
    {
        RoundRunning = false;

        Debug.Log("ROUND OVER");

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        // Future:
        // Spin wheels again
        // Show score
        // Load next level
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
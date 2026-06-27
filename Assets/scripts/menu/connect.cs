using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject levelCompletePanel;

    private void Start()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    private void Update()
    {
        if (!RoundManager.RoundRunning) return;

        // Check if all enemies are dead
        if (FindObjectsOfType<Combat_enemy>().Length == 0)
        {
            RoundManager.Instance.EndRound();
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);
    }

    public void NextLevelButton()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            // No next scene = last level, go to win screen
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
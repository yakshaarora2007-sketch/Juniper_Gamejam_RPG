using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private TMP_Text nextButtonText;  // drag the button text here

    private void Start()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    public void ShowLevelComplete()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        // Change button text if last level
        if (nextButtonText != null)
            nextButtonText.text = string.IsNullOrEmpty(nextSceneName) 
                ? "Finish" 
                : "Next Level";
    }

    public void NextLevelButton()
    {
        Time.timeScale = 1f;
        if (string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(3);
        else
            SceneManager.LoadScene(nextSceneName);
    }
}
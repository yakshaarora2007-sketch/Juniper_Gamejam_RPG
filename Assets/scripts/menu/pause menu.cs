using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private bool isPaused = false;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        isPaused = true;
        container.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        Resume();
    }

    private void Resume()
    {
        isPaused = false;
        container.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f; // always reset before scene load
        SceneManager.LoadScene("MainMenu");
    }
}
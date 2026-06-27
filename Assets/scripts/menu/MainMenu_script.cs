using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("map_1");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // only visible in editor since Quit() doesn't work in editor
    }
}
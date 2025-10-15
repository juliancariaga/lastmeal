using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Day");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

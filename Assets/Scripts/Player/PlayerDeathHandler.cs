using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    public string daySceneName = "DayScene";   // Set this in the Inspector

    public void OnPlayerDeath()
    {
        // Tell GameManager the player died (optional for showing a death menu)
        if (GameManager.Instance != null)
            GameManager.Instance.playerDiedLastNight = true;

        // Load the Day Scene
        SceneManager.LoadScene(daySceneName);
    }
}

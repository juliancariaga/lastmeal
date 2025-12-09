using UnityEngine;
using UnityEngine.SceneManagement;

public class OvenMinigamePortal : MonoBehaviour
{
    public string cookingSceneName = "cooking";

    private bool playerInside;

    void Update()
    {
        // Right mouse button while player is inside the trigger
        if (playerInside && Input.GetMouseButtonDown(1))
        {
            Debug.Log("Opening cooking minigame...");
            SceneManager.LoadScene(cookingSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered oven trigger.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player left oven trigger.");
        }
    }
}

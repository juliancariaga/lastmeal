using UnityEngine;
using UnityEngine.SceneManagement;

public class OvenMinigameClick : MonoBehaviour
{
    [SerializeField] private string cookingSceneName = "cooking";

    private void OnMouseDown()
    {
        // This is called when you click on this object (has to have a Collider2D)
        Debug.Log("Oven clicked – loading minigame: " + cookingSceneName);
        SceneManager.LoadScene(cookingSceneName);
    }
}

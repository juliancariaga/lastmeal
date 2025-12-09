using UnityEngine;
using UnityEngine.SceneManagement;

public class OvenClickMinigame : MonoBehaviour
{
    public string cookingSceneName = "cooking";

    void Update()
    {
        // Right mouse button - change to 0 if you prefer left click
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("Oven clicked, loading minigame...");
                Time.timeScale = 1f; // make sure we're not paused
                SceneManager.LoadScene(cookingSceneName);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class OvenClickManager : MonoBehaviour
{
    [SerializeField] private string cookingSceneName = "cooking";
    [SerializeField] private LayerMask ovenLayer;   // which layer counts as oven

    void Update()
    {
        // Change 0 to 1 if you want RIGHT click instead
        if (Input.GetMouseButtonDown(0))   // 0 = left, 1 = right
        {
            // Mouse position in world space
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos2D = new Vector2(worldPos.x, worldPos.y);

            // Raycast at that point, only against the oven layer
            RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, 0f, ovenLayer);

            if (hit.collider != null)
            {
                Debug.Log("Clicked oven: " + hit.collider.name);
                SceneManager.LoadScene(cookingSceneName);
            }
        }
    }
}

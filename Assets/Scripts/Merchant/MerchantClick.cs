using UnityEngine;

public class MerchantClick : MonoBehaviour
{
    public GameObject merchantPanel;   // drag Merchantpanel here in Inspector

    void Start()
    {
        // Make sure the panel starts hidden
        if (merchantPanel != null)
            merchantPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        // Left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point = new Vector2(mouseWorld.x, mouseWorld.y);

            // Check what we clicked on
            Collider2D hit = Physics2D.OverlapPoint(point);

            if (hit != null && hit.gameObject == this.gameObject)
            {
                Debug.Log("Merchant clicked! Opening shop...");
                if (merchantPanel != null)
                    merchantPanel.SetActive(true);

                // Time.timeScale = 0f; // optional pause
            }
        }
    }
}

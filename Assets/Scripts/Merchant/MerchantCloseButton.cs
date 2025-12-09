using UnityEngine;

public class MerchantCloseButton : MonoBehaviour
{
    public GameObject panel;   // The merchant panel to hide

    void Awake()
    {
        // If you forget to drag it in, use the parent object by default
        if (panel == null && transform.parent != null)
            panel = transform.parent.gameObject;
    }

    public void Close()
    {
        if (panel != null)
            panel.SetActive(false);

        Time.timeScale = 1f;
        Debug.Log("Merchant panel closed");
    }
}

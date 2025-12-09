using TMPro;
using UnityEngine;

public class GoldCounterUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI goldText;
    public string prefix = "Gold: ";

    void Start()
    {
        // If not set in Inspector, grab TextMeshProUGUI on this GameObject
        if (goldText == null)
            goldText = GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
            if (goldText == null) return;

            int amount = 0;
            if (GameManager.Instance != null)
                amount = GameManager.Instance.gold;

            goldText.text = prefix + amount.ToString();
    }
}

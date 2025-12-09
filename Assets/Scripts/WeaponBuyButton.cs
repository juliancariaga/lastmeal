using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBuyButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public int cost = 5;   // set per weapon in Inspector

    private bool purchased = false;

    public void OnBuy()
    {
        // Prevent double-buying the same weapon
        if (purchased) return;

        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // 🔹 Real gold logic (CAN go negative)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gold -= cost;   // no check, can go below 0
            Debug.Log($"Bought weapon for {cost}. New gold: {GameManager.Instance.gold}");
        }
        else
        {
            Debug.LogWarning("WeaponBuyButton: GameManager.Instance is null, gold not changed.");
        }

        // 🔹 Mark as sold
        buttonText.text = "SOLD!";
        purchased = true;

        // Optional: disable the button so player can't click again
        var btn = GetComponent<Button>();
        if (btn != null)
            btn.interactable = false;
    }
}

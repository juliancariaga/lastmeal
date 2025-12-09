using TMPro;
using UnityEngine;

public class WeaponBuyButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    
    public int cost = 0;

    public void OnBuy()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // If you want to ignore gold completely, just do this:
        buttonText.text = "SOLD!";

        // If later you want to use gold, you could do:
        // if (GameManager.Instance != null && GameManager.Instance.gold >= cost)
        // {
        //     GameManager.Instance.gold -= cost;
        //     buttonText.text = "SOLD!";
        // }
    }
}

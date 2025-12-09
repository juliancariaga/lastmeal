using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("References")]
    public Health targetHealth;   // Player's Health component
    public Slider slider;         // The UI slider

    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        // Safety check
        if (targetHealth == null)
        {
            Debug.LogWarning("HealthBarUI: targetHealth not assigned.");
            return;
        }

        // Initialize slider range
        slider.minValue = 0f;
        slider.maxValue = targetHealth.maxHealth;
        slider.value = targetHealth.currentHealth;
    }

    void Update()
    {
        if (targetHealth == null || slider == null) return;

        slider.value = targetHealth.currentHealth;
    }
}

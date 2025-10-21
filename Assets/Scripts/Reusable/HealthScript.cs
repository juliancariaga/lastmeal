using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("Events")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Call this to apply damage to this object.
    /// </summary>
    public void TakeDamage(float amount, GameObject source = null)
    {
        if (amount <= 0f || currentHealth <= 0f) return;

        currentHealth = Mathf.Max(0f, currentHealth - amount);
        onDamaged?.Invoke();

        if (currentHealth <= 0f)
        {
            onDeath?.Invoke();
            // Example default behavior. Replace with death animation/disable/etc.
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        if (amount <= 0f || currentHealth <= 0f) return;
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }
}

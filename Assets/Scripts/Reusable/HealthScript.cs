using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public enum MonsterRarity { Common, Uncommon, Rare, Epic }

    [Header("Events")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    [Header("Scale feedback settings")]
    public float scaleAmount = 0.8f;    // how small to shrink 
    public float scaleDuration = 0.1f;  // how long the squish lasts before returning

    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    void Awake()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;
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
    public void ShrinkOnHit()
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(ScaleFlash());
    }

    private IEnumerator ScaleFlash()
    {
        // scale down
        transform.localScale = originalScale * scaleAmount;
        yield return new WaitForSeconds(scaleDuration);
        // return to normal
        transform.localScale = originalScale;
        scaleRoutine = null;
    }
}

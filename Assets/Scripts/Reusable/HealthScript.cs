using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("Events")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    [Header("Scale feedback settings")]
    public float scaleAmount = 0.8f;    // how small to shrink 
    public float scaleDuration = 0.1f;  // how long the squish lasts before returning

    [Header("Blocked-hit feedback")]
    public GameObject blockedTextPrefab;   // optional "Too high rarity" text
    public float blockedTextYOffset = 0.8f;
    public bool alsoShrinkOnBlocked = false;

    [Header("Blocked-hit cooldown")]
    public float blockedHitCooldown = 0.3f; // cooldown between blocked messages
    private float lastBlockedTime = -999f;


    [Header("Flash feedback settings")]
    public float flashDuration = 0.1f;
    public Color flashColor = Color.white;

    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    // NEW: sprite reference + flash coroutine
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;

    void Awake()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;

        // NEW: auto-cache sprite
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
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
        transform.localScale = originalScale * scaleAmount;
        yield return new WaitForSeconds(scaleDuration);
        transform.localScale = originalScale;
        scaleRoutine = null;
    }

    /// <summary>
    /// Called when an attack is blocked due to rarity being too low.
    /// </summary>
    public void ShowBlockedHit()
    {
        // cooldown check
        if (Time.time - lastBlockedTime < blockedHitCooldown)
            return;

        lastBlockedTime = Time.time;

        // spawn floating text, if assigned
        if (blockedTextPrefab != null)
        {
            Vector3 worldPos = transform.position + Vector3.up * blockedTextYOffset;

            GameObject go = Instantiate(
                blockedTextPrefab,
                worldPos,
                Quaternion.identity
            );

            // Make text face the camera (optional)
            if (Camera.main != null)
                go.transform.forward = Camera.main.transform.forward;
        }

        // flash white
        PlayBlockedFlash();

        // shrink if enabled
        if (alsoShrinkOnBlocked)
            ShrinkOnHit();
    }


    // NEW: method to trigger shimmer/flash
    private void PlayBlockedFlash()
    {
        if (spriteRenderer == null) return;

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(BlockedFlash());
    }

    // NEW: flash coroutine
    private IEnumerator BlockedFlash()
    {
        // set to white (or chosen flash color)
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // restore original color
        spriteRenderer.color = originalColor;

        flashRoutine = null;
    }
}

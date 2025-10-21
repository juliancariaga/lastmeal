using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnContact : MonoBehaviour
{
    [Header("Damage")]
    public float damage = 10f;

    [Tooltip("Which layers are allowed to receive damage from this object.")]
    public LayerMask targetLayers;

    [Tooltip("Optional: prevent multi-hits if objects stay overlapping.")]
    public float rehitCooldown = 0.2f;

    private float _lastHitTime = -999f;

    void Reset()
    {
        // Make sure the collider is a trigger for OnTrigger* to fire.
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    // Optional: if you want damage on stays too (e.g., hazards)
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - _lastHitTime >= rehitCooldown)
            TryDamage(other);
    }

    private void TryDamage(Collider2D other)
    {
        // Layer filter first (faster than GetComponent).
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) return;

        // Don’t hit your own root (e.g., child trigger touching parent).
        if (other.transform.root == transform.root) return;

        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damage, gameObject);
            _lastHitTime = Time.time;
        }
    }
}

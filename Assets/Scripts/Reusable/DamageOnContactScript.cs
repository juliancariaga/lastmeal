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
        // 1) Layer filter first
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) return;

        // 2) Don’t hit your own root
        if (other.transform.root == transform.root) return;

        // 3) Get Health once (if no Health, nothing to do)
        if (!other.TryGetComponent<Health>(out var health)) return;

        // 4) Rarity gating (only when this object acts like a weapon)
        WeaponRarity weaponRarity = GetComponent<WeaponRarity>() ?? GetComponentInParent<WeaponRarity>();

        if (weaponRarity != null && other.TryGetComponent<EnemyRarity>(out var enemyRarity))
        {
            // Weapon is weaker than the enemy → blocked
            if (weaponRarity.rarity < enemyRarity.rarity)
            {
                health.ShowBlockedHit();   // show text / shrink if enabled
                _lastHitTime = Time.time;  // so it doesn't spam every frame
                return;                    // no damage
            }
        }

        // 5) Apply damage normally
        health.TakeDamage(damage, gameObject);
        _lastHitTime = Time.time;
    }

}

using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    [Header("Data (assign in Inspector)")]
    public DropProfile dropProfile;
    public GameObject pickupPrefab;

    [Header("Spawn Tuning")]
    [Range(0f, 1.5f)] public float spawnSpread = 0.5f;
    public bool addPopOutImpulse = true;
    public float popOutForce = 1.5f;

    public void OnDeath()
    {
        if (dropProfile == null || pickupPrefab == null)
        {
            Debug.LogWarning($"{name}: DropOnDeath missing references.");
            return;
        }

        // 1) Chance gate
        if (Random.value > Mathf.Clamp01(dropProfile.dropChance))
            return;

        // 2) Mode selection
        switch (dropProfile.selectionMode)
        {
            case DropSelectionMode.RandomSingle:
                SpawnOneByWeight(dropProfile.entries);
                break;
            case DropSelectionMode.MultipleChances:
                SpawnIndependent(dropProfile.entries);
                break;
            case DropSelectionMode.AlwaysAll:
                SpawnAllGuaranteed(dropProfile.entries);
                break;
        }
    }

    void SpawnOneByWeight(List<DropEntry> list)
    {
        if (list == null || list.Count == 0) return;

        float total = 0f;
        foreach (var e in list) total += Mathf.Max(0f, e.weight);
        if (total <= 0f) return;

        float r = Random.value * total;
        foreach (var e in list)
        {
            r -= Mathf.Max(0f, e.weight);
            if (r <= 0f) { SpawnEntry(e); break; }
        }
    }

    void SpawnIndependent(List<DropEntry> list)
    {
        if (list == null || list.Count == 0) return;
        foreach (var e in list)
            if (Random.value <= Mathf.Clamp01(e.chance))
                SpawnEntry(e);
    }

    void SpawnAllGuaranteed(List<DropEntry> list)
    {
        if (list == null || list.Count == 0) return;
        foreach (var e in list)
            if (e.guaranteed)
                SpawnEntry(e);
    }

    void SpawnEntry(DropEntry e)
    {
        if (e == null || e.ingredient == null) return;

        int min = (e.minOverride > 0) ? e.minOverride : e.ingredient.minAmount;
        int max = (e.maxOverride > 0) ? e.maxOverride : e.ingredient.maxAmount;
        if (max < min) max = min;
        int amount = Random.Range(min, max + 1);

        Vector3 pos = transform.position + (Vector3)(Random.insideUnitCircle * spawnSpread);
        GameObject go = Instantiate(pickupPrefab, pos, Quaternion.identity);

        var pickup = go.GetComponent<PickupIngredient>();
        if (pickup != null)
        {
            pickup.ingredient = e.ingredient;
            pickup.amount = amount;
        }

        if (addPopOutImpulse)
        {
            var rb = go.GetComponent<Rigidbody>();
            if (rb != null) rb.AddForce(Random.onUnitSphere * popOutForce, ForceMode.Impulse);

            var rb2d = go.GetComponent<Rigidbody2D>();
            if (rb2d != null) rb2d.AddForce(Random.insideUnitCircle.normalized * popOutForce, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.8f, 1f, 0.35f);
        Gizmos.DrawWireSphere(transform.position, spawnSpread);
    }
}

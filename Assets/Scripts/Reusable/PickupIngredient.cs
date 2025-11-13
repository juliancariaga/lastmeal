using UnityEngine;

public class PickupIngredient : MonoBehaviour
{
    public Ingredient ingredient; // what item this is (Tomato, Dog Meat, etc.)
    public int amount = 1;        // how many

    void Start()
    {
    var sr = GetComponent<SpriteRenderer>();
    if (sr && ingredient && ingredient.icon) sr.sprite = ingredient.icon;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponent<PlayerInventory>();   // <- get the player's inventory
        
        if (inv != null && ingredient != null && amount > 0)
        {
        inv.Add(ingredient, amount);                   // <- add to inventory
        Debug.Log($"Picked up {amount}x {ingredient.displayName}");
        Destroy(gameObject);
        }
    }

}

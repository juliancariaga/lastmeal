// Defines the item data 
//Example: Carrot, Dog meat, Tomato 

using UnityEngine;
using UnityEngine.UIElements;

// Enums is a fixed list of options below i defined once. Keeps it organize 
// and avoids different spellings for category and rarity in this case
public enum IngredientCategory { Vegetable, Fruit, Grain, Dairy, Meat, Herb }
public enum Rarity {Common, Uncommon, Rare}

//[CreateAssetMenu(...)] tells Unity to add a menu option in the Editor, so Ingredient assets can be created easily
// Assets -> Create -> Game -> Ingredient
[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients Data/Ingredient")]
public class Ingredient : ScriptableObject // ScriptableObject is used for data-only, ScriptableObject = blueprint / recipe / data sheet
{
    [Header("Identity")] // is just an Inspector label, to make it organized
    public string id; // Id is like a unique barcode on a barcode. For the computer
    public string displayName; // what shows in UI ("Dog meat"). What the player will see
    public Sprite icon; // the sprite shown in the HUD or Inventory

    [Header("Design")]
    public IngredientCategory category; // groups the ingredient type like Vegetable, Grain, etc
    public Rarity rarity; // how rare it is
    // this will be used by the enum as small dropdown lists 

    [Header("Balancing")]
    [Min(1)] public int minAmount = 1;
    [Min(1)] public int maxAmount = 1; // min and max is how much of be drop at once
    [Min(0)] public int baseValue = 1; // how much it's "worth" (for scoring or selling)


    // Editor niceties

    private void OnValidate() // Special Unity editor method that runs only inside the Unity editor
    {
        if (string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(displayName))
            id = displayName.Trim().ToLower().Replace(" ", "_"); // if id is empty it will use the displayName
                                                                 // converted to lowercase and with spaces by underscore
        if (maxAmount < minAmount) maxAmount = minAmount; // if max is lower than the min, 
        // //it will make the max equal to min
    }




}

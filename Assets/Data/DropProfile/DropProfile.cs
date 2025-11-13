// Defines how a npc drop item(s)
// Example Dog's drop list

using System.Collections.Generic; // able to use List<>
using UnityEngine;

public enum DropSelectionMode // Creates a dropdown choice in the Inspector for the Drop Modes
{
    [InspectorName("Random Single Drop")] RandomSingle, // Pick one random itm
    [InspectorName("Multiple Drops (Independent)")] MultipleChances, // Each item rolls its own chance and can get both
    [InspectorName("Always Drop All")] AlwaysAll // Everything in the list will drop
}


[CreateAssetMenu(fileName = "DropProfile", menuName = "DropProfile/DropProfile")]
public class DropProfile : ScriptableObject
{ 
    // Should this creature Drop Anything chose a number between 0.0 - 1.0
    [Header("Should this creature Drop Anything: 0%-100% or 0.0 - 1.0")]
    [Range(0f, 1f)] public float dropChance = 1f;


    // what type of drop mode 
    // look at public enum DropSelectionMode
    [Header("How should the items drop?")]
    public DropSelectionMode selectionMode = DropSelectionMode.RandomSingle;

    // List of possible items that the enemy can drop
    // Each element in the list is a DropEntry
    [Header("Which items can this Enemy drop")] 
    public List<DropEntry> entries = new List<DropEntry>();
}

// [System.Serializable] is needed so we can use the Entries in the inspector and add our drops
// Entries are the Ingredient asset 
[System.Serializable]
public class DropEntry
{
    [Header("Item(s) to drop")]
    public Ingredient ingredient;// Reference to the Ingredient asset like Dog Meat

    [Header("Optional amount overrides (0 = use Ingredient defaults)")]
    [Min(0)] public int minOverride = 0; // if you want to change the min drop for THIS enemy
    [Min(0)] public int maxOverride = 0; // same as minOverride  
                                         // (leave them 0 if you want to use Ingredient default)

    [Header("Use ONE of these drop modes")]
    [Min(0f)] public float weight = 1f;     // used ONLY in RandomSingle mode
    [Range(0f,1f)] public float chance = 0; // used ONLY in MultipleChances mode
    public bool guaranteed = false;         // used ONLY in AlwaysAll mode

}
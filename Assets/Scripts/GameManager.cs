using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Cooking minigame session data
    [HideInInspector] public Ingredient rawIngredientToCook;
    [HideInInspector] public Ingredient cookedIngredientResult;
    [HideInInspector] public int amountToCook;

    public static GameManager Instance;

    // Persistent inventory storage
    public List<StackEntry> persistentInventory = new List<StackEntry>();

    // For gold / currency (optional)
    public int gold = 0;

    // Used so DayScene knows to display death menu
    public bool playerDiedLastNight = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

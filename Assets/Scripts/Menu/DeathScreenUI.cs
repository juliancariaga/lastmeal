using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("The whole panel that should pop up on death.")]
    public GameObject panel;

    [Tooltip("TMP text that lists what the player collected.")]
    public TMP_Text lootText;

    [Header("Scene Settings")]
    [Tooltip("Name of the Day scene to load when the player continues.")]
    public string daySceneName = "DayScene";

    [Header("Inventory Reference")]
    [Tooltip("Player object that has the PlayerInventory component.")]
    public PlayerInventory playerInventory;

    void Awake()
    {
        // Make sure the panel starts hidden
        if (panel != null)
            panel.SetActive(false);
    }

    /// <summary>
    /// Call this when the player dies.
    /// Shows the panel and fills it with loot info.
    /// </summary>
    public void ShowDeathScreen()
    {
        if (panel == null || lootText == null || playerInventory == null)
        {
            Debug.LogWarning("DeathScreenUI not wired correctly in Inspector.");
            return;
        }

        Time.timeScale = 0f;        // pause the game
        panel.SetActive(true);

        RefreshLootText();
    }

    void RefreshLootText()
    {
        var items = playerInventory.items;   // List<StackEntry> from your PlayerInventory:contentReference[oaicite:0]{index=0}

        if (items == null || items.Count == 0)
        {
            lootText.text = "You collected nothing this night.";
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("You collected:");

        foreach (var entry in items)
        {
            if (entry == null || entry.ingredient == null)
                continue;

            // Ingredient has displayName and rarity fields:contentReference[oaicite:1]{index=1}
            string name = entry.ingredient.displayName;
            string rarity = entry.ingredient.rarity.ToString();
            int count = entry.count;

            sb.AppendLine($"{name} ({rarity}) x{count}");
        }

        lootText.text = sb.ToString();
    }

    /// <summary>
    /// Hook this to your 'DayScene Button' OnClick.
    /// </summary>
    public void OnClickContinue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(daySceneName);
    }

    /// <summary>
    /// Optional: hook this to ExitButton if you want to quit the game.
    /// </summary>
    public void OnClickExitGame()
    {
        Time.timeScale = 1f;
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}

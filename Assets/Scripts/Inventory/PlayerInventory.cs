using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // use TMP_Text if you prefer TextMeshPro

[System.Serializable]
public class StackEntry
{
    public Ingredient ingredient;
    public int count;
    public StackEntry(Ingredient ing, int c) { ingredient = ing; count = c; }
}

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Data")]
    public List<StackEntry> items = new List<StackEntry>();
    public UnityEvent onChanged;

    [Header("UI (optional)")]
    public GameObject panel;   // a Panel GameObject you show/hide
    public TMP_Text listText;      // or TMP_Text if using TextMeshPro
    public KeyCode toggleKey = KeyCode.Tab;

    bool isOpen = false;

    void OnEnable()  { if (onChanged != null) onChanged.AddListener(RefreshIfOpen); }
    void OnDisable() { if (onChanged != null) onChanged.RemoveListener(RefreshIfOpen); }

    void Start()
{
    if (GameManager.Instance != null)
        items = GameManager.Instance.persistentInventory; //// Load saved inventory from GameManager

    if (panel) panel.SetActive(false);   // start hidden
}

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            Toggle();
    }

    public void Add(Ingredient ing, int amount)
    {
        if (ing == null || amount <= 0) return;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ingredient == ing)
            {
                items[i].count += amount;
                onChanged?.Invoke();
                return;
            }
        }
        items.Add(new StackEntry(ing, amount));
        onChanged?.Invoke();
    }

    public bool TryConsume(Ingredient ing, int amount)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ingredient == ing && items[i].count >= amount)
            {
                items[i].count -= amount;
                if (items[i].count == 0) items.RemoveAt(i);
                onChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

    // -------- UI helpers --------

    void Toggle()
    {
        isOpen = !isOpen;
        if (panel) panel.SetActive(isOpen);
        if (isOpen) RefreshUI();
    }

    void RefreshIfOpen()
    {
        if (isOpen) RefreshUI();
    }

    void RefreshUI()
    {
        if (!listText) return;

        var sb = new StringBuilder();
        if (items.Count == 0) sb.AppendLine("Empty");
        else
        {
            foreach (var s in items)
                sb.AppendLine($"{s.ingredient.displayName} x{s.count}");
        }
        listText.text = sb.ToString();
    }
}

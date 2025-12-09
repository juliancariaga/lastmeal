using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Setup")]
    public Transform weaponHolder;             // parent transform for weapons
    public List<GameObject> startingWeapons;   // weapon PREFABS to start with

    private readonly List<GameObject> ownedWeapons = new List<GameObject>();
    private int currentIndex = -1;

    void Awake()
    {
        // Spawn starting weapons as children of the weaponHolder
        foreach (var prefab in startingWeapons)
        {
            if (prefab == null) continue;

            var weaponInstance = Instantiate(prefab, weaponHolder);
            weaponInstance.SetActive(false);
            ownedWeapons.Add(weaponInstance);
        }

        // Equip first weapon if any
        if (ownedWeapons.Count > 0)
        {
            Equip(0);
        }
    }

    void Update()
    {
        // Simple controls: 1,2,3 to swap, and Q/E to cycle
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Equip(2);

        if (Input.GetKeyDown(KeyCode.Q)) PreviousWeapon();
        if (Input.GetKeyDown(KeyCode.E)) NextWeapon();
    }

    public void Equip(int index)
    {
        if (index < 0 || index >= ownedWeapons.Count) return;

        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            ownedWeapons[i].SetActive(i == index);
        }

        currentIndex = index;
        Debug.Log($"Equipped weapon: {ownedWeapons[index].name}");
    }

    public void NextWeapon()
    {
        if (ownedWeapons.Count == 0) return;
        int newIndex = (currentIndex + 1) % ownedWeapons.Count;
        Equip(newIndex);
    }

    public void PreviousWeapon()
    {
        if (ownedWeapons.Count == 0) return;
        int newIndex = (currentIndex - 1 + ownedWeapons.Count) % ownedWeapons.Count;
        Equip(newIndex);
    }

    // Called when picking up a new weapon in the world
    public void UnlockWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null || weaponHolder == null) return;

        // Avoid duplicates: if we already have this prefab, do nothing
        foreach (var w in ownedWeapons)
        {
            if (w.name.StartsWith(weaponPrefab.name))  // simple check; you can use an ID later
                return;
        }

        var weaponInstance = Instantiate(weaponPrefab, weaponHolder);
        weaponInstance.SetActive(false);
        ownedWeapons.Add(weaponInstance);

        // If this is the first weapon, auto-equip it
        if (currentIndex == -1)
            Equip(ownedWeapons.Count - 1);

        Debug.Log($"Unlocked new weapon: {weaponPrefab.name}");
    }
}

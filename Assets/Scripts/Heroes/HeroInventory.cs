using System.Collections.Generic;
using UnityEngine;

// Purpose: Contains the functionality for heros to hold onto various items and equipment
// Directions: Attach to each hero
// Other notes: 

public class HeroInventory : MonoBehaviour
{
    [SerializeField] List<BaseItem> inventory = new List<BaseItem>(); // Used as the master inventory for each hero.  It is serialized simply so it is viewable in the inspector for testing.
    public List<BaseItem> GetInventory() { return inventory; }
    public void AddToInventory(BaseItem item) { inventory.Add(item); }
    public void RemoveFromInventory(BaseItem item) { inventory.Remove(item); }
    public void ClearInventory() { inventory.Clear(); }

    HeroManager heroManager;

    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
    }

    /// <summary>
    /// Not in use yet.
    /// </summary>
    /// <returns>Int with how many Training Equipment items the hero has in their inventory</returns>
    public int GetTrainingEquipmentCount()
    {
        int i = 0;

        foreach (BaseItem item in inventory)
        {
            if (item is TrainingEquipment)
            {
                i++;
            }
        }

        return i;
    }
}

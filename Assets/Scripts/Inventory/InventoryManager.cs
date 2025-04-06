using System.Collections.Generic;
using UnityEngine;

// Purpose: Handles player inventory.  Not used yet.
// Directions: Just attach to [System]
// Other notes: 

public class InventoryManager : MonoBehaviour
{
    List<BaseItem> inventory = new List<BaseItem>();
    public List<BaseItem> GetInventory() { return inventory; }
    public void AddToInventory(BaseItem item) { inventory.Add(item); }
    public void RemoveFromInventory(BaseItem item) { inventory.Remove(item); }
    public void ClearInventory() { inventory.Clear(); }
}

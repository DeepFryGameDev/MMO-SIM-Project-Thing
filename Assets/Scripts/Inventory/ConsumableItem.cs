using UnityEngine;

// Purpose: Used as the derived class for all consumable items (such as potions, food, etc).
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Consumables/New Item")]
public class ConsumableItem : HeroItem
{
    [Header("---Consumable Item Values---")]
    public ConsumableEffect effect;
}

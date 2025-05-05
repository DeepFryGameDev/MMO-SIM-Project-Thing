using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Consumables/New Item")]
public class ConsumableItem : HeroItem
{
    [Header("---Consumable Item Values---")]
    public ConsumableEffect effect;
}

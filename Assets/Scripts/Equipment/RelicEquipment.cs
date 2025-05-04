using UnityEngine;

// Purpose: Relics provide bonuses to abilities or other useful passive abilities.  
// Directions: 
// Other notes: Should start at low levels with more useful things for low levels, like carry weight.

[CreateAssetMenu(menuName = "Equipment/New Relic")]
public class RelicEquipment : HeroBaseEquipment
{
    private void Awake()
    {
        rarity = EnumHandler.InventoryRarities.COMMON;
    }
}

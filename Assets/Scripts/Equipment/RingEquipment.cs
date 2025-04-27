using UnityEngine;

// Purpose: Rings provide flat secondary stat bonuses, like crit modifiers and attack speed
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Equipment/New Ring")]
public class RingEquipment : HeroEquipment
{
    private void Awake()
    {
        rarity = EnumHandler.InventoryRarities.COMMON;
    }
}

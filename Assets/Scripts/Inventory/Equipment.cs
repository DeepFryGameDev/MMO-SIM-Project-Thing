using UnityEngine;

// Purpose: The base ScriptableObject class for any equipment to be equipped by heros.
// Directions: This will not be called directly, but will be the base class for any equipment.
// Other notes: 

public class Equipment : BaseItem
{
    [Tooltip("Name of the equipment.")]
    public new string name;

    [Tooltip("Affects the drop rate and color in UI tooltip")]
    public EnumHandler.EquipmentRarities rarity;
}

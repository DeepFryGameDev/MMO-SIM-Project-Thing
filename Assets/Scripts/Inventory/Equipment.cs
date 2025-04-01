using UnityEngine;

public class Equipment : BaseItem
{
    [Tooltip("Name of the equipment.")]
    public new string name;

    [Tooltip("Affects the drop rate and color in UI tooltip")]
    public EnumHandler.EquipmentRarities rarity;
}

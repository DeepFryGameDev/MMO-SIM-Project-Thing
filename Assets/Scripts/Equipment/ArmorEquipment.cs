using UnityEngine;

// Purpose: Used as the derived class for all armor - this is Helm, Chest, Hands (not to be confused with main/offhand), Legs, Feet.
// Directions: Any armor specific details should go into here.
// Other notes: 

[CreateAssetMenu(menuName = "Equipment/New Armor")]
public class ArmorEquipment : HeroBaseEquipment
{
    [Header("---Base Armor Values---")]
    public EnumHandler.ArmorClasses armorClass;
    public EnumHandler.EquipmentArmorSlots equipSlot;

    public int baseArmorValue;
    public int baseMagicResistValue;

    [Header("---Stat Increases---")]
    public int strengthBonus;
    public int enduranceBonus;
    public int agilityBonus;
    public int dexterityBonus;
    public int intelligenceBonus;
    public int faithBonus;

    [Header("---Stat Variance---")]
    public float stat1Variance = 1;
    public int stat2Variance = 0;
    public int stat3Variance = 0;
}

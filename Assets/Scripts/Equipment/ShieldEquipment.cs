using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Equipment/New Shield")]
public class ShieldEquipment : HandEquipment
{
    public EnumHandler.ShieldClasses shieldClass;

    public int damageBlocked;

    public int baseArmorValue;
    public int baseMagicResistValue;

    private void Awake()
    {
        equipSlot = EnumHandler.EquipmentHandSlots.OFFHAND;
    }
}

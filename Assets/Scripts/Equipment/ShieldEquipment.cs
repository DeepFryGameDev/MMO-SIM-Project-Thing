using UnityEngine;

// Purpose: Used as the derived class for all shield equipment.
// Directions: Any shield related values should be housed here.
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

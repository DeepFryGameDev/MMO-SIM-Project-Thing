using UnityEngine;

// Purpose: This may be reworked, but for now this is the derived class that will house any mainhand/offhand related functionality.  Weapons, shields, and offhand items (like a lantern or a gem or something) would go here too.
// Directions: 
// Other notes: 

public class HandEquipment : HeroBaseEquipment
{
    [Header("---Base Hand Equipment Values---")]
    public EnumHandler.EquipmentHandSlots equipSlot;
}

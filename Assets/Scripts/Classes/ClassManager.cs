using UnityEngine;

// Purpose: Facilitates functionality for classes.  This is where the math is run for each stat and parameter to determine differences in how classes operate.
// Directions: Attach to [System]
// Other notes: 

public class ClassManager : MonoBehaviour
{
    public static ClassManager i;

    private void Awake()
    {
        i = this;
    }

    /// <summary>
    /// Checks if the given hero's class is able to equip the armor being checked.
    /// </summary>
    /// <param name="classDetails">Hero's class to compare with</param>
    /// <param name="itemsArmorClass">Armor Class of the item to be checked</param>
    /// <returns></returns>
    public bool CanWearArmorClass(HeroClassDetails classDetails, EnumHandler.ArmorClasses itemsArmorClass)
    {
        switch (itemsArmorClass)
        {
            case EnumHandler.ArmorClasses.CLOTH:
                if (classDetails.clothEquippable) { return true; }
                return false;
            case EnumHandler.ArmorClasses.LEATHER:
                if (classDetails.leatherEquippable) { return true; }
                return false;
            case EnumHandler.ArmorClasses.MAIL:
                if (classDetails.mailEquippable) { return true; }
                return false;
            case EnumHandler.ArmorClasses.PLATE:
                if (classDetails.plateEquippable) { return true; }
                return false;
            default:
                DebugManager.i.ClassDebugOut("ClassManager", "CanWearArmorClass - item's armor class not found.  Defaulting to false.", false, true);
                return false;
        }
    }

    /// <summary>
    /// Checks if the given hero's class is able to equip the weapon being checked.
    /// </summary>
    /// <param name="classDetails">Hero's class to compare with</param>
    /// <param name="itemsWeaponClass">Armor Class of the item to be checked</param>
    /// <returns></returns>
    public bool CanEquipWeaponClass(HeroClassDetails classDetails, EnumHandler.WeaponClasses itemsWeaponClass)
    {
        switch (itemsWeaponClass)
        {
            case EnumHandler.WeaponClasses.SWORD:
                if (classDetails.swordEquippable) { return true; }
                return false;
            case EnumHandler.WeaponClasses.BOW:
                if (classDetails.bowEquippable) { return true; }
                    return false;
            case EnumHandler.WeaponClasses.MACE:
                if (classDetails.maceEquippable) { return true; }
                return false;
            case EnumHandler.WeaponClasses.STAFF:
                if (classDetails.staffEquippable) { return true; }
                return false;
            case EnumHandler.WeaponClasses.LANCE:
                if (classDetails.lanceEquippable) { return true; }
                return false;
            default:
                DebugManager.i.ClassDebugOut("ClassManager", "CanEquipWeaponClass - item's weapon class not found.  Defaulting to false.", false, true);
                return false;
        }
    }
}

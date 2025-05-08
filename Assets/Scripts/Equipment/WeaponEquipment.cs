using UnityEngine;

// Purpose: Used as the derived class for all weapons.  
// Directions: Any weapon related functionality or values should come from here.
// Other notes: 

[CreateAssetMenu(menuName = "Equipment/New Weapon")]
public class WeaponEquipment : HandEquipment
{
    [Header("---Base Weapon Equipment Values---")]
    public EnumHandler.WeaponClasses weaponClass;

    public int attackDamage;

    public float attackSpeed;
}

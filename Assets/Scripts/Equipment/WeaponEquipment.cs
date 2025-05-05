using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Equipment/New Weapon")]
public class WeaponEquipment : HandEquipment
{
    [Header("---Base Weapon Equipment Values---")]
    public EnumHandler.WeaponClasses weaponClass;

    public int attackDamage;

    public float attackSpeed;
}

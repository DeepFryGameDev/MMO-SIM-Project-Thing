using UnityEngine;

// Purpose: Houses each class specific variable.  This is essentially where classes are "configured".
// Directions: Create a new class in Assets/ScriptableObjects/Classes using Right Click > Create > Classes > New Class.  This will be assigned to another script later to be applied to heroes.
// Other notes: 

[CreateAssetMenu(menuName = "Classes/New Class")]
public class HeroClassDetails : ScriptableObject
{
    [Header("Base Class Info")]
    [Tooltip("The name of the class")]
    public new string name;
    [Tooltip("The main class required to be leveled before this class is available.")]
    public EnumHandler.BaseHeroClasses primaryClass;
    [Tooltip("The secondary class required to be leveled before this class is available.")]
    public EnumHandler.BaseHeroClasses subClass;

    [Header("Equippable Armor")]
    [Tooltip("If this class is capable of equipping cloth armor")]
    public bool clothEquippable;
    [Tooltip("If this class is capable of equipping leather armor")]
    public bool leatherEquippable;
    [Tooltip("If this class is capable of equipping mail armor")]
    public bool mailEquippable;
    [Tooltip("If this class is capable of equipping plate armor")]
    public bool plateEquippable;

    [Header("Equippable Weapons")]
    [Tooltip("If this class is capable of equipping swords")]
    public bool swordEquippable;
    [Tooltip("If this class is capable of equipping bows")]
    public bool bowEquippable;
    [Tooltip("If this class is capable of equipping maces")]
    public bool maceEquippable;
    [Tooltip("If this class is capable of equipping staves")]
    public bool staffEquippable;
    [Tooltip("If this class is capable of equipping lances")]
    public bool lanceEquippable;

    [Header("Equippable Offhands")]
    [Tooltip("If this class is capable of equipping shields")]
    public bool shieldEquippable;

    [Header("Required Stat Levels")]
    [Tooltip("Strength level required before this class is available")]
    public int requiredStrengthLevel;
    [Tooltip("Endurance level required before this class is available")]
    public int requiredEnduranceLevel;
    [Tooltip("Agility level required before this class is available")]
    public int requiredAgilityLevel;
    [Tooltip("Dexterity level required before this class is available")]
    public int requiredDexterityLevel;
    [Tooltip("Intelligence level required before this class is available")]
    public int requiredIntelligenceLevel;
    [Tooltip("Faith level required before this class is available")]
    public int requiredFaithLevel;
}

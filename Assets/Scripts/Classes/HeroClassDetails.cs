using UnityEngine;

[CreateAssetMenu(menuName = "Classes/New Class")]
public class HeroClassDetails : ScriptableObject
{
    [Header("Base Class Info")]
    public EnumHandler.HeroClasses heroClass;
    public EnumHandler.HeroClasses branchedFromClass;

    [Header("Equippable Armor")]
    public bool clothEquippable;
    public bool leatherEquippable;
    public bool mailEquippable;
    public bool plateEquippable;

    [Header("Equippable Weapons")]
    public bool swordEquippable;
    public bool bowEquippable;
    public bool maceEquippable;
    public bool staffEquippable;
    public bool lanceEquippable;

    [Header("Equippable Offhands")]
    public bool shieldEquippable;

    [Header("Required Stat Levels")]
    public int requiredStrengthLevel;
    public int requiredEnduranceLevel;
    public int requiredAgilityLevel;
    public int requiredDexterityLevel;
    public int requiredIntelligenceLevel;
    public int requiredFaithLevel;
}

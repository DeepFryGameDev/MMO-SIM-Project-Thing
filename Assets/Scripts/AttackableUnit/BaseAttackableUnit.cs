using UnityEngine;

// Purpose: The parent class used to house all variables and functionality for any unit that can be attacked, both enemies and the player
// Directions: Anything that should be shared between enemies and the player should be set in this class
// Other notes: 

public class BaseAttackableUnit : MonoBehaviour
{
    // -- Name
    [Tooltip("The name of the unit")]
    [SerializeField] new string name;
    public string GetName() { return name; }

    // -- Parameters --
    protected int currentHP;
    public int GetCurrentHP() { return currentHP; }
    public void SetCurrentHP(int HP) { currentHP = HP; }

    // -- Stats --
    [Tooltip("Offensive Physical Attribute - Measure of physical power")]
    [SerializeField] protected int baseStrength;
    public int GetBaseStrength() { return baseStrength; }
    public void SetBaseStrength(int baseStrength) { this.baseStrength = baseStrength; } // This is the base value before equipment, perks, etc

    [Tooltip("Defensive Physical Attribute - Measure of stamina and health")]
    [SerializeField] protected int baseEndurance;
    public int GetBaseEndurance() { return baseEndurance; }
    public void SetBaseEndurance(int baseEndurance) { this.baseEndurance = baseEndurance; } // This is the base value before equipment, perks, etc

    [Tooltip("Offensive Mobility Attribute - Measure of physical awareness and perception")]
    [SerializeField] protected int baseAgility;
    public int GetBaseAgility() { return baseAgility; }
    public void SetBaseAgility(int baseAgility) { this.baseAgility = baseAgility; } // This is the base value before equipment, perks, etc

    [Tooltip("Defensive Mobility Attribute - Measure of reflexes and movement")]
    [SerializeField] protected int baseDexterity;
    public int GetBaseDexterity() { return baseDexterity; }
    public void SetBaseDexterity(int baseDexterity) { this.baseDexterity = baseDexterity; } // This is the base value before equipment, perks, etc

    [Tooltip("Offensive Magical Attribute - Measure of potency with offensive magic")]
    [SerializeField] protected int baseIntelligence;
    public int GetBaseIntelligence() { return baseIntelligence; }
    public void SetBaseIntelligence(int baseIntelligence) { this.baseIntelligence = baseIntelligence; } // This is the base value before equipment, perks, etc       

    [Tooltip("Defensive Magical Attribute - Measure of potency with defensive magic/stats and mana regeneration")]
    [SerializeField] protected int baseFaith;
    public int GetBaseFaith() { return baseFaith; }
    public void SetBaseFaith(int baseFaith) { this.baseFaith = baseFaith; } // This is the base value before equipment, perks, etc    

    // armor
    [Tooltip("Total armor - Decreases physical damage taken")]
    [SerializeField] protected int baseArmor;
    public int GetBaseArmor() { return baseArmor; }

    int armor;
    public virtual int GetArmor() { return armor; }
    public void SetArmor(int armor) { this.armor = armor; }

    // magic resistance
    [Tooltip("Total Magic Resist - Decreases magical damge taken")]
    [SerializeField] protected int baseMagicResist;
    public int GetBaseMagicResist() { return baseMagicResist; }
    int magicResist;
    public virtual int GetMagicResist() { return magicResist; }
    public void SetMagicResist(int magicResist) { this.magicResist = magicResist; }

    // movement speed

    // -- Other stuff --

}

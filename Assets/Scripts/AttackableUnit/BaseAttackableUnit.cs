using UnityEngine;

// Purpose: The parent class used to house all variables and functionality for any unit that can be attacked, both enemies and the player
// Directions: Anything that should be shared between enemies and the player should be set in this class
// Other notes: 

public class BaseAttackableUnit : MonoBehaviour
{
    // -- Name
    [Tooltip("The name of the unit")]
    [SerializeField] new string name;

    // -- Parameters --
    protected int currentHP;
    public int GetCurrentHP() { return currentHP; }
    public void SetCurrentHP(int HP) { currentHP = HP; }

    // -- Stats --
    [Tooltip("Offensive Physical Attribute - Measure of physical power")]
    [SerializeField] protected int baseStrength;
    public int GetBaseStrength() { return baseStrength; }

    int strength; // Current Strength Level
    public virtual int GetStrength() { return strength; }
    public void SetStrength (int strength) { this.strength = strength;}

    [Tooltip("Defensive Physical Attribute - Measure of stamina and health")]
    [SerializeField] protected int baseEndurance;
    public int GetBaseEndurance() { return baseEndurance; }

    int endurance;
    public virtual int GetEndurance() { return endurance; }
    public void SetEndurance(int endurance) { this.endurance = endurance; }

    [Tooltip("Offensive Mobility Attribute - Measure of physical awareness and perception")]
    [SerializeField] protected int baseAgility;
    public int GetBaseAgility() { return baseAgility; }

    int agility;
    public virtual int GetAgility() { return agility; }
    public void SetAgility(int agility) { this.agility = agility; }

    [Tooltip("Defensive Mobility Attribute - Measure of reflexes and movement")]
    [SerializeField] protected int baseDexterity;
    public int GetBaseDexterity() { return baseDexterity; }

    int dexterity;
    public virtual int GetDexterity() { return dexterity; }
    public void SetDexterity(int dexterity) { this.dexterity = dexterity; }

    [Tooltip("Offensive Magical Attribute - Measure of potency with offensive magic")]
    [SerializeField] protected int baseIntelligence;
    public int GetBaseIntelligence() { return baseIntelligence; }

    int intelligence;
    public virtual int GetIntelligence() { return intelligence; }
    public void SetIntelligence(int intelligence) { this.intelligence = intelligence; }

    [Tooltip("Defensive Magical Attribute - Measure of potency with defensive magic/stats and mana regeneration")]
    [SerializeField] protected int baseFaith;
    public int GetBaseFaith() { return baseFaith; }

    int faith;
    public virtual int GetFaith() { return faith; }
    public void SetFaith(int faith) { this.faith = faith; }

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

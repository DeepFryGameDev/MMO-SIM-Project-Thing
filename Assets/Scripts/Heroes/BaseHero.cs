using UnityEngine;

// Purpose: Controls all hero stats and equipment functionality
// Directions: Attach to Player GameObject
// Other notes: Distribute stat points up to 18 for balance.

public class BaseHero : BaseAttackableUnit
{
    [SerializeField] string heroName;

    [SerializeField] int ID;
    public int GetID() { return ID; }

    // -- HP --
    int baseHP = 100; // Base health before modifying with Endurance
    float enduranceToHPMod = .375f; // Used to calculate maximum HP based on the player's Endurance

    // -- Stamina --
    float baseStamina = 100; // Base Max Stamina for the player - taken into account before class stats - used to determine sprinting duration
    float enduranceToStaminaMod = .25f; // Used to calculate maximum stamina based on the player's Endurance

    // -- Energy -- 
    int energy;
    public int GetEnergy() { return energy; }
    public void SetEnergy(int energy) { this.energy = energy; }


    // -- Primary Stats --
    [SerializeField] int defaultStrength;

    int strength; // Current Strength Level
    public void SetStrength(int strength) { this.strength = strength; } // This takes into account equipment, perks, etc
    public int GetStrength() { return strength; }

    [SerializeField] int defaultEndurance;
    int endurance;
    public void SetEndurance(int endurance) { this.endurance = endurance; } // This takes into account equipment, perks, etc
    public int GetEndurance() { return endurance; }

    [SerializeField] int defaultAgility;
    int agility;
    public void SetAgility(int agility) { this.agility = agility; } // This takes into account equipment, perks, etc
    public int GetAgility() { return agility; }

    [SerializeField] int defaultDexterity;
    int dexterity;
    public void SetDexterity(int dexterity) { this.dexterity = dexterity; } // This takes into account equipment, perks, etc
    public int GetDexterity() { return dexterity; }

    [SerializeField] int defaultIntelligence;
    int intelligence;
    public void SetIntelligence(int intelligence) { this.intelligence = intelligence; } // This takes into account equipment, perks, etc
    public int GetIntelligence() { return intelligence; }

    [SerializeField] int defaultFaith;
    int faith;
    public void SetFaith(int faith) { this.faith = faith; } // This takes into account equipment, perks, etc
    public int GetFaith() { return faith; }

    float resistToArmorMod = .5f; // Makes armor more effective based on player's resistance

    float resistToMagicResistMod = .5f; // Makes magic resist more effective based on player's resistance

    // -- Secondary stats

    #region Equipment

    // Houses all of the currently equipped items on the player

    #endregion

    // Attacks

    /// <summary>
    /// All inherited classes should call base.Awake() in 'protected override void Awake()'
    /// </summary>
    protected virtual void Awake()
    {
        SetUp();
    }

    /// <summary>
    /// Sets base stats and attributes for the player
    /// </summary>
    protected void SetUp()
    {
        // set hero manager

        SetBaseStats();

        SetStrength(GetBaseStrength());
        SetEndurance(GetBaseEndurance());
        SetAgility(GetBaseAgility());
        SetDexterity(GetBaseDexterity());
        SetIntelligence(GetBaseIntelligence());
        SetFaith(GetBaseFaith());

        SetArmor(GetArmor());
        SetMagicResist(GetMagicResist());

        SetCurrentHP(GetMaxHP());

        energy = HeroSettings.maxEnergy;

        // ReportStats();
    }

    private void SetBaseStats()
    {
        base.SetName(heroName);
        base.baseStrength = defaultStrength;
        base.baseEndurance = defaultEndurance;
        base.baseAgility = defaultAgility;
        base.baseDexterity = defaultDexterity;
        base.baseIntelligence = defaultIntelligence;
        base.baseFaith = defaultFaith;
    }

    /// <summary>
    /// Used to calculate the player's maximum health points
    /// </summary>
    /// <returns>Value by rounding the result of baseHP * the product of player's endurance and enduranceToHP modifier</returns>
    public int GetMaxHP()
    {
        return Mathf.RoundToInt(baseHP * (GetEndurance() * enduranceToHPMod));
    }

    /// <summary>
    /// Sets current HP to player's max HP
    /// </summary>
    public void UpdateHPForMax()
    {
        currentHP = GetMaxHP();
    }

    /// <summary>
    /// Used to calculate the player's maximum stamina
    /// </summary>
    /// <returns>Value calculated by baseStamina * (player's endurance * enduranceToStamina modifier)</returns>
    public float GetMaxStamina()
    {
        return (baseStamina * (GetEndurance() * enduranceToStaminaMod));
    }

    /// <summary>
    /// Lower's players health points by the provided value
    /// </summary>
    /// <param name="damage">Amount of health points to be decreased</param>
    public void TakeDamage(int damage)
    {
        Debug.Log("Player HP before damage: " + GetCurrentHP());
        currentHP -= damage;

        Debug.Log("Player HP after damage: " + GetCurrentHP());

        if (GetCurrentHP() <= 0)
        {
            // die
            Die();
        }
    }

    /// <summary>
    /// Raises players health points by the provided value
    /// </summary>
    /// <param name="healthToHeal">Amount of health points to be replenished</param>
    public void Heal(int healthToHeal)
    {
        currentHP += healthToHeal;

        if (currentHP >= GetMaxHP())
        {
            currentHP = GetMaxHP();
        }
    }

    /// <summary>
    /// Should result in asking the player if they want to quit or load 
    /// </summary>
    void Die()
    {
        // Show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// A debug method to output the player's current attributes and stats
    /// </summary>
    public void ReportStats()
    {
        Debug.Log("--Stats Report--");
        Debug.Log("MaxHP: " + GetMaxHP());
        Debug.Log("-----");
        Debug.Log("Armor: " + GetArmor());
        Debug.Log("Magic Resist: " + GetMagicResist());
        Debug.Log("-----");
        Debug.Log("STR: " + GetStrength());
        Debug.Log("END: " + GetEndurance());
        Debug.Log("AGI: " + GetAgility());
        Debug.Log("DEX: " + GetDexterity());
        Debug.Log("INT: " + GetIntelligence());
        Debug.Log("FTH: " + GetFaith());
        Debug.Log("--End Stats--");
    }

    #region Get attribute/stat values from Equipment functions
    // These functions return the player's attributes by adding the base value together with all equipped equipment values

    public override int GetArmor()
    {
        int totalArmor = base.GetArmor();

        /*
        if (GetEquippedMainHand() != null)
        {
            totalArmor += GetEquippedMainHand().armor;
        }
        if (GetEquippedOffHand() != null)
        {
            totalArmor += GetEquippedOffHand().armor;
        }

        if (GetEquippedHelm() != null)
        {
            totalArmor += GetEquippedHelm().armor;
        }

        if (GetEquippedChest() != null)
        {
            totalArmor += GetEquippedChest().armor;
        }

        if (GetEquippedHands() != null)
        {
            totalArmor += GetEquippedHands().armor;
        }

        if (GetEquippedLegs() != null)
        {
            totalArmor += GetEquippedLegs().armor;
        }

        if (GetEquippedFeet() != null)
        {
            totalArmor += GetEquippedFeet().armor;
        }

        if (GetEquippedAmulet() != null)
        {
            totalArmor += GetEquippedAmulet().armor;
        }

        if (GetEquippedRingOne() != null)
        {
            totalArmor += GetEquippedRingOne().armor;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalArmor += GetEquippedRingTwo().armor;
        }
        */

        totalArmor = Mathf.RoundToInt(totalArmor * (GetFaith() * resistToArmorMod)); // Improves Armor based on player's resist

        return totalArmor; // calculate strength including equipped gear here
    }

    public override int GetMagicResist()
    {
        int totalMagicResist = base.GetMagicResist();

        /*
        if (GetEquippedMainHand() != null)
        {
            totalMagicResist += GetEquippedMainHand().magicResist;
        }
        if (GetEquippedOffHand() != null)
        {
            totalMagicResist += GetEquippedOffHand().magicResist;
        }

        if (GetEquippedHelm() != null)
        {
            totalMagicResist += GetEquippedHelm().magicResist;
        }

        if (GetEquippedChest() != null)
        {
            totalMagicResist += GetEquippedChest().magicResist;
        }

        if (GetEquippedHands() != null)
        {
            totalMagicResist += GetEquippedHands().magicResist;
        }

        if (GetEquippedLegs() != null)
        {
            totalMagicResist += GetEquippedLegs().magicResist;
        }

        if (GetEquippedFeet() != null)
        {
            totalMagicResist += GetEquippedFeet().magicResist;
        }

        if (GetEquippedAmulet() != null)
        {
            totalMagicResist += GetEquippedAmulet().magicResist;
        }

        if (GetEquippedRingOne() != null)
        {
            totalMagicResist += GetEquippedRingOne().magicResist;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalMagicResist += GetEquippedRingTwo().magicResist;
        }
        */

        totalMagicResist = Mathf.RoundToInt(totalMagicResist * (GetFaith() * resistToMagicResistMod)); // Improves Magic Resist based on player's resist

        return totalMagicResist; // calculate strength including equipped gear here
    }

    #endregion
}

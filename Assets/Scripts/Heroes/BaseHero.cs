using UnityEngine;

// Purpose: Controls all hero stats and equipment functionality
// Directions: Attach to Player GameObject
// Other notes: Distribute stat points up to 18 for balance.

public class BaseHero : BaseAttackableUnit
{
    // -- HP --
    int baseHP = 100; // Base health before modifying with Endurance
    float enduranceToHPMod = .375f; // Used to calculate maximum HP based on the player's Endurance

    // -- Stamina --
    float baseStamina = 100; // Base Max Stamina for the player - taken into account before class stats - used to determine sprinting duration
    float enduranceToStaminaMod = .25f; // Used to calculate maximum stamina based on the player's Endurance

    // -- Energy -- 
    float energy;
    public float GetEnergy() { return energy; }
    public void SetEnergy(float energy) { this.energy = energy; }

    float currentStamina; // Used to track what the player's current stamina at any given point is
    public float GetCurrentStamina() { return currentStamina; }
    public void SetCurrentStamina(float stamina) { currentStamina = stamina;}

    float resistToArmorMod = .5f; // Makes armor more effective based on player's resistance

    float resistToMagicResistMod = .5f; // Makes magic resist more effective based on player's resistance

    // -- Secondary stats

    #region Equipment

    // Houses all of the currently equipped items on the player

    BaseMainHandEquipment equippedMainHand;
    public void SetEquippedMainHand(BaseMainHandEquipment mainHand) { this.equippedMainHand = mainHand; }
    public BaseMainHandEquipment GetEquippedMainHand() { return equippedMainHand; }

    BaseOffHandEquipment equippedOffHand;
    public void SetEquippedOffHand(BaseOffHandEquipment offHand) { this.equippedOffHand = offHand; }
    public BaseOffHandEquipment GetEquippedOffHand() { return equippedOffHand; }

    BaseAmuletEquipment equippedAmulet;
    public void SetEquippedAmulet(BaseAmuletEquipment amulet) { this.equippedAmulet = amulet; }
    public BaseAmuletEquipment GetEquippedAmulet() { return equippedAmulet; }

    BaseRingEquipment equippedRingOne;
    public void SetEquippedRingOne(BaseRingEquipment ringOne) { this.equippedRingOne = ringOne; }
    public BaseRingEquipment GetEquippedRingOne() { return equippedRingOne; }

    BaseRingEquipment equippedRingTwo;
    public void SetEquippedRingTwo(BaseRingEquipment ringTwo) { this.equippedRingTwo = ringTwo; }
    public BaseRingEquipment GetEquippedRingTwo() { return equippedRingTwo; }

    BaseHelmEquipment equippedHelm;
    public void SetEquippedHelm(BaseHelmEquipment helm) { this.equippedHelm = helm; }
    public BaseHelmEquipment GetEquippedHelm() { return equippedHelm; }

    BaseChestEquipment equippedChest;
    public void SetEquippedChest(BaseChestEquipment chest) { this.equippedChest = chest; }
    public BaseChestEquipment GetEquippedChest() { return equippedChest; }

    BaseHandEquipment equippedHands;
    public void SetEquippedHands(BaseHandEquipment hands) { this.equippedHands = hands; }
    public BaseHandEquipment GetEquippedHands() { return equippedHands; }

    BaseLegsEquipment equippedLegs;
    public void SetEquippedLegs(BaseLegsEquipment legs) { this.equippedLegs = legs; }
    public BaseLegsEquipment GetEquippedLegs() { return equippedLegs; }

    BaseFeetEquipment equippedFeet;
    public void SetEquippedFeet(BaseFeetEquipment feet) { this.equippedFeet = feet; }
    public BaseFeetEquipment GetEquippedFeet() { return equippedFeet; }

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

        SetStrength(GetBaseStrength());
        SetEndurance(GetBaseEndurance());
        SetAgility(GetBaseAgility());
        SetDexterity(GetBaseDexterity());
        SetIntelligence(GetBaseIntelligence());
        SetFaith(GetBaseFaith());

        SetArmor(GetBaseArmor());
        SetMagicResist(GetBaseMagicResist());

        SetCurrentHP(GetMaxHP());

        UpdateStaminaForMax();

        energy = HeroSettings.maxEnergy;

        // ReportStats();
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
    /// Simply sets current stamina to player's max stamina
    /// </summary>
    public void UpdateStaminaForMax()
    {
        currentStamina = GetMaxStamina();
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
        Debug.Log("MaxStamina: " + GetMaxStamina());
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

    public override int GetStrength()
    {
        int totalStrength = base.GetStrength();

        if (GetEquippedMainHand() != null)
        {
            totalStrength += GetEquippedMainHand().strength;
        }

        if (GetEquippedOffHand() != null)
        {
            totalStrength += GetEquippedOffHand().strength;
        }

        if (GetEquippedHelm() != null)
        {
            totalStrength += GetEquippedHelm().strength;
        }

        if (GetEquippedChest() != null)
        {
            totalStrength += GetEquippedChest().strength;
        }

        if (GetEquippedHands() != null)
        {
            totalStrength += GetEquippedHands().strength;
        }

        if (GetEquippedLegs() != null)
        {
            totalStrength += GetEquippedLegs().strength;
        }

        if (GetEquippedFeet() != null)
        {
            totalStrength += GetEquippedFeet().strength;
        }

        if (GetEquippedAmulet() != null)
        {
            totalStrength += GetEquippedAmulet().strength;
        }

        if (GetEquippedRingOne() != null)
        {
            totalStrength += GetEquippedRingOne().strength;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalStrength += GetEquippedRingTwo().strength;
        }

        return totalStrength; // calculate strength including equipped gear here
    }

    public override int GetEndurance()
    {
        int totalEndurance = base.GetEndurance();

        if (GetEquippedMainHand() != null)
        {
            totalEndurance += GetEquippedMainHand().endurance;
        }

        if (GetEquippedOffHand() != null)
        {
            totalEndurance += GetEquippedOffHand().endurance;
        }

        if (GetEquippedHelm() != null)
        {
            totalEndurance += GetEquippedHelm().endurance;
        }

        if (GetEquippedChest() != null)
        {
            totalEndurance += GetEquippedChest().endurance;
        }

        if (GetEquippedHands() != null)
        {
            totalEndurance += GetEquippedHands().endurance;
        }

        if (GetEquippedLegs() != null)
        {
            totalEndurance += GetEquippedLegs().endurance;
        }

        if (GetEquippedFeet() != null)
        {
            totalEndurance += GetEquippedFeet().endurance;
        }

        if (GetEquippedAmulet() != null)
        {
            totalEndurance += GetEquippedAmulet().endurance;
        }

        if (GetEquippedRingOne() != null)
        {
            totalEndurance += GetEquippedRingOne().endurance;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalEndurance += GetEquippedRingTwo().endurance;
        }

        return totalEndurance; // calculate endurance including equipped gear here
    }

    public override int GetAgility()
    {
        int totalAgility = base.GetAgility();

        if (GetEquippedMainHand() != null)
        {
            totalAgility += GetEquippedMainHand().agility;
        }

        if (GetEquippedOffHand() != null)
        {
            totalAgility += GetEquippedOffHand().agility;
        }

        if (GetEquippedHelm() != null)
        {
            totalAgility += GetEquippedHelm().agility;
        }

        if (GetEquippedChest() != null)
        {
            totalAgility += GetEquippedChest().agility;
        }

        if (GetEquippedHands() != null)
        {
            totalAgility += GetEquippedHands().agility;
        }

        if (GetEquippedLegs() != null)
        {
            totalAgility += GetEquippedLegs().agility;
        }

        if (GetEquippedFeet() != null)
        {
            totalAgility += GetEquippedFeet().agility;
        }

        if (GetEquippedAmulet() != null)
        {
            totalAgility += GetEquippedAmulet().agility;
        }

        if (GetEquippedRingOne() != null)
        {
            totalAgility += GetEquippedRingOne().agility;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalAgility += GetEquippedRingTwo().agility;
        }

        return totalAgility; // calculate strength including equipped gear here
    }

    public override int GetDexterity()
    {
        int totalDexterity = base.GetDexterity();

        if (GetEquippedMainHand() != null)
        {
            totalDexterity += GetEquippedMainHand().dexterity;
        }
        if (GetEquippedOffHand() != null)
        {
            totalDexterity += GetEquippedOffHand().dexterity;
        }

        if (GetEquippedHelm() != null)
        {
            totalDexterity += GetEquippedHelm().dexterity;
        }

        if (GetEquippedChest() != null)
        {
            totalDexterity += GetEquippedChest().dexterity;
        }

        if (GetEquippedHands() != null)
        {
            totalDexterity += GetEquippedHands().dexterity;
        }

        if (GetEquippedLegs() != null)
        {
            totalDexterity += GetEquippedLegs().dexterity;
        }

        if (GetEquippedFeet() != null)
        {
            totalDexterity += GetEquippedFeet().dexterity;
        }

        if (GetEquippedAmulet() != null)
        {
            totalDexterity += GetEquippedAmulet().dexterity;
        }

        if (GetEquippedRingOne() != null)
        {
            totalDexterity += GetEquippedRingOne().dexterity;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalDexterity += GetEquippedRingTwo().dexterity;
        }

        return totalDexterity; // calculate strength including equipped gear here
    }

    public override int GetIntelligence()
    {
        int totalIntelligence = base.GetIntelligence();

        if (GetEquippedMainHand() != null)
        {
            totalIntelligence += GetEquippedMainHand().intelligence;
        }
        if (GetEquippedOffHand() != null)
        {
            totalIntelligence += GetEquippedOffHand().intelligence;
        }

        if (GetEquippedHelm() != null)
        {
            totalIntelligence += GetEquippedHelm().intelligence;
        }

        if (GetEquippedChest() != null)
        {
            totalIntelligence += GetEquippedChest().intelligence;
        }

        if (GetEquippedHands() != null)
        {
            totalIntelligence += GetEquippedHands().intelligence;
        }

        if (GetEquippedLegs() != null)
        {
            totalIntelligence += GetEquippedLegs().intelligence;
        }

        if (GetEquippedFeet() != null)
        {
            totalIntelligence += GetEquippedFeet().intelligence;
        }

        if (GetEquippedAmulet() != null)
        {
            totalIntelligence += GetEquippedAmulet().intelligence;
        }

        if (GetEquippedRingOne() != null)
        {
            totalIntelligence += GetEquippedRingOne().intelligence;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalIntelligence += GetEquippedRingTwo().intelligence;
        }

        return totalIntelligence; // calculate strength including equipped gear here
    }

    public override int GetFaith()
    {
        int totalFaith = base.GetFaith();

        if (GetEquippedMainHand() != null)
        {
            totalFaith += GetEquippedMainHand().faith;
        }
        if (GetEquippedOffHand() != null)
        {
            totalFaith += GetEquippedOffHand().faith;
        }

        if (GetEquippedHelm() != null)
        {
            totalFaith += GetEquippedHelm().faith;
        }

        if (GetEquippedChest() != null)
        {
            totalFaith += GetEquippedChest().faith;
        }

        if (GetEquippedHands() != null)
        {
            totalFaith += GetEquippedHands().faith;
        }

        if (GetEquippedLegs() != null)
        {
            totalFaith += GetEquippedLegs().faith;
        }

        if (GetEquippedFeet() != null)
        {
            totalFaith += GetEquippedFeet().faith;
        }

        if (GetEquippedAmulet() != null)
        {
            totalFaith += GetEquippedAmulet().faith;
        }

        if (GetEquippedRingOne() != null)
        {
            totalFaith += GetEquippedRingOne().faith;
        }

        if (GetEquippedRingTwo() != null)
        {
            totalFaith += GetEquippedRingTwo().faith;
        }

        return totalFaith; // calculate strength including equipped gear here
    }

    public override int GetArmor()
    {
        int totalArmor = base.GetArmor();

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

        totalArmor = Mathf.RoundToInt(totalArmor * (GetFaith() * resistToArmorMod)); // Improves Armor based on player's resist

        return totalArmor; // calculate strength including equipped gear here
    }

    public override int GetMagicResist()
    {
        int totalMagicResist = base.GetMagicResist();

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

        totalMagicResist = Mathf.RoundToInt(totalMagicResist * (GetFaith() * resistToMagicResistMod)); // Improves Magic Resist based on player's resist

        return totalMagicResist; // calculate strength including equipped gear here
    }

    #endregion
}

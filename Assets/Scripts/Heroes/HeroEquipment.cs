using UnityEngine;

// Purpose: Facilitates functionality with setting a piece of equipment as 'equipped' to a hero (and vice versa) and other equipment functionalities.  Here is also where each piece of 'equipped' equipment is tracked for each hero.
// Directions: Attach to each hero object.
// Other notes: 

public class HeroEquipment : MonoBehaviour
{
    [Header("---Equipped Armor---")]
    [SerializeField] ArmorEquipment equippedHead;
    public void SetEquippedHead(ArmorEquipment equipment) { equippedHead = equipment; }
    public ArmorEquipment GetEquippedHead() { return equippedHead; }

    [SerializeField] ArmorEquipment equippedChest;
    public void SetEquippedChest(ArmorEquipment equipment) { equippedChest = equipment; }
    public ArmorEquipment GetEquippedChest() { return equippedChest; }

    [SerializeField] ArmorEquipment equippedHands;
    public void SetEquippedHands(ArmorEquipment equipment) { equippedHands = equipment; }
    public ArmorEquipment GetEquippedHands() { return equippedHands; }

    [SerializeField] ArmorEquipment equippedLegs;
    public void SetEquippedLegs(ArmorEquipment equipment) { equippedLegs = equipment; }
    public ArmorEquipment GetEquippedLegs() { return equippedLegs; }

    [SerializeField] ArmorEquipment equippedFeet;
    public void SetEquippedFeet(ArmorEquipment equipment) { equippedFeet = equipment; }
    public ArmorEquipment GetEquippedFeet() { return equippedFeet; }

    [Header("---Equipped Rings---")]
    [SerializeField] RingEquipment equippedRing1;
    public void SetEquippedRing1(RingEquipment equippedRing) { this.equippedRing1 = equippedRing; }
    public RingEquipment GetEquippedRing1() { return equippedRing1; }

    [SerializeField] RingEquipment equippedRing2;
    public void SetEquippedRing2(RingEquipment equippedRing) { this.equippedRing2 = equippedRing; }
    public RingEquipment GetEquippedRing2() { return equippedRing2; }

    [Header("---Equipped Relics---")]
    [SerializeField] RelicEquipment equippedRelic1;
    public void SetEquippedRelic1(RelicEquipment equippedRelic) { this.equippedRelic1 = equippedRelic; }
    public RelicEquipment GetEquippedRelic1() { return equippedRelic1; }

    [SerializeField] RelicEquipment equippedRelic2;
    public void SetEquippedRelic2(RelicEquipment equippedRelic) { this.equippedRelic2 = equippedRelic; }
    public RelicEquipment GetEquippedRelic2() { return equippedRelic2; }

    [Header("---Equipped Trinket---")]
    [SerializeField] TrinketEquipment equippedTrinket;
    public void SetEquippedTrinket(TrinketEquipment equippedTrinket) { this.equippedTrinket = equippedTrinket; }
    public TrinketEquipment GetEquippedTrinket() { return equippedTrinket; }

    [Header("---Equipped Weapon/Offhand---")]
    [SerializeField] WeaponEquipment equippedMainHand;
    public void SetEquippedMainHand(WeaponEquipment equipment) { equippedMainHand = equipment; }
    public WeaponEquipment GetEquippedMainHand() { return equippedMainHand; }

    [SerializeField] ShieldEquipment equippedShield; // will need to be reworked to allow for different types of equipment. Right now, doesn't do anything.

    HeroManager heroManager; // The Hero Manager assigned to this hero

    void Awake()
    {
        heroManager = GetComponent<HeroManager>();
    }

    /// <summary>
    /// Sets the hero's current stats to their base stat value + any bonuses they receive from equipment and perks (not yet implemented).
    /// This should be ran any time a piece of equipment is changed or perk is changed
    /// </summary>
    void UpdateStats()
    {
        int tempStrength = heroManager.Hero().GetBaseStrength();
        int tempEndurance = heroManager.Hero().GetBaseEndurance();
        int tempAgility = heroManager.Hero().GetBaseAgility();
        int tempDexterity = heroManager.Hero().GetBaseDexterity();
        int tempIntelligence = heroManager.Hero().GetBaseIntelligence();
        int tempFaith = heroManager.Hero().GetBaseFaith();

        if (equippedHead != null) 
        { 
            tempStrength += equippedHead.strengthBonus;
            tempEndurance += equippedHead.enduranceBonus;
            tempAgility += equippedHead.agilityBonus;
            tempDexterity += equippedHead.dexterityBonus;
            tempIntelligence += equippedHead.intelligenceBonus;
            tempFaith += equippedHead.faithBonus;
        }

        if (equippedChest != null)
        {
            tempStrength += equippedChest.strengthBonus;
            tempEndurance += equippedChest.enduranceBonus;
            tempAgility += equippedChest.agilityBonus;
            tempDexterity += equippedChest.dexterityBonus;
            tempIntelligence += equippedChest.intelligenceBonus;
            tempFaith += equippedChest.faithBonus;
        }

        if (equippedHands != null)
        {
            tempStrength += equippedHands.strengthBonus;
            tempEndurance += equippedHands.enduranceBonus;
            tempAgility += equippedHands.agilityBonus;
            tempDexterity += equippedHands.dexterityBonus;
            tempIntelligence += equippedHands.intelligenceBonus;
            tempFaith += equippedHands.faithBonus;
        }

        if (equippedLegs != null)
        {
            tempStrength += equippedLegs.strengthBonus;
            tempEndurance += equippedLegs.enduranceBonus;
            tempAgility += equippedLegs.agilityBonus;
            tempDexterity += equippedLegs.dexterityBonus;
            tempIntelligence += equippedLegs.intelligenceBonus;
            tempFaith += equippedLegs.faithBonus;
        }

        if (equippedFeet != null)
        {
            tempStrength += equippedFeet.strengthBonus;
            tempEndurance += equippedFeet.enduranceBonus;
            tempAgility += equippedFeet.agilityBonus;
            tempDexterity += equippedFeet.dexterityBonus;
            tempIntelligence += equippedFeet.intelligenceBonus;
            tempFaith += equippedFeet.faithBonus;
        }

        heroManager.Hero().SetStrength(tempStrength);
        heroManager.Hero().SetEndurance(tempEndurance);
        heroManager.Hero().SetAgility(tempAgility);
        heroManager.Hero().SetDexterity(tempDexterity);
        heroManager.Hero().SetIntelligence(tempIntelligence);
        heroManager.Hero().SetFaith(tempFaith);
    }

    #region Armor

    /// <summary>
    /// Simply sets the given armor slot on the hero to null and adds the item that was equipped in that slot back to the player's inventory.
    /// </summary>
    /// <param name="armorSlot">The armor slot to be unequipped</param>
    public void UnequipArmor(EnumHandler.EquipmentArmorSlots armorSlot)
    {
            switch (armorSlot)
            {
                case EnumHandler.EquipmentArmorSlots.HEAD:
                    if (GetEquippedHead() != null)
                    {
                        heroManager.HeroInventory().AddToInventory(GetEquippedHead());
                        DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedHead().name + " from " + heroManager.Hero().GetName(), false, false);
                        SetEquippedHead(null);                        
                    }
                    break;
            case EnumHandler.EquipmentArmorSlots.CHEST:
                if (GetEquippedChest() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedChest());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedChest().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedChest(null);
                }
                break;
            case EnumHandler.EquipmentArmorSlots.HANDS:
                if (GetEquippedHands() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedHands());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedHands().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedHands(null);
                }
                break;
            case EnumHandler.EquipmentArmorSlots.LEGS:
                if (GetEquippedLegs() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedLegs());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedLegs().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedLegs(null);
                }
                break;
            case EnumHandler.EquipmentArmorSlots.FEET:
                if (GetEquippedFeet() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedFeet());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedFeet().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedFeet(null);
                }
                break;
        }

        UpdateStats();
    }

    /// <summary>
    /// Sets the given armor item to the appropriate slot on the hero
    /// </summary>
    /// <param name="armorToEquip"></param>
    public void EquipArmor(ArmorEquipment armorToEquip)
    {
        switch (armorToEquip.equipSlot)
        {
            case EnumHandler.EquipmentArmorSlots.HEAD:
                SetEquippedHead(armorToEquip);
                break;
            case EnumHandler.EquipmentArmorSlots.CHEST:
                SetEquippedChest(armorToEquip);
                break;
            case EnumHandler.EquipmentArmorSlots.HANDS:
                SetEquippedHands(armorToEquip);
                break;
            case EnumHandler.EquipmentArmorSlots.LEGS:
                SetEquippedLegs(armorToEquip);
                break;
            case EnumHandler.EquipmentArmorSlots.FEET:
                SetEquippedFeet(armorToEquip);
                break;
        }

        UpdateStats();
    }

    #endregion

    #region Rings

    public void UnequipRing(int slot)
    {
        switch (slot)
        {
            case 1:
                if (GetEquippedRing1() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedRing1());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedRing1().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedRing1(null);
                }
                break;
            case 2:
                if (GetEquippedRing2() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedRing2());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedRing2().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedRing2(null);
                }                
                break;
        }

        UpdateStats();
    }

    public void EquipRing(RingEquipment ringToEquip, int slot)
    {
        switch (slot)
        {
            case 1:
                SetEquippedRing1(ringToEquip);
                break;
            case 2:
                SetEquippedRing2(ringToEquip);
                break;
        }

        UpdateStats();
    }

    #endregion

    #region Relics

    public void UnequipRelic(int slot)
    {
        switch (slot)
        {
            case 1:
                if (GetEquippedRelic1() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedRelic1());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedRelic1().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedRelic1(null);
                }
                break;
            case 2:
                if (GetEquippedRelic2() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedRelic2());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedRelic2().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedRelic2(null);
                }
                break;
        }

        UpdateStats();
    }

    public void EquipRelic(RelicEquipment relicToEquip, int slot)
    {
        switch (slot)
        {
            case 1:
                SetEquippedRelic1(relicToEquip);
                break;
            case 2:
                SetEquippedRelic2(relicToEquip);
                break;
        }

        UpdateStats();
    }

    #endregion

    #region Trinkets

    public void UnequipTrinket()
    {
        if (GetEquippedTrinket() != null)
        {
            heroManager.HeroInventory().AddToInventory(GetEquippedTrinket());
            DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedTrinket().name + " from " + heroManager.Hero().GetName(), false, false);
            SetEquippedTrinket(null);
        }

        UpdateStats();
    }

    public void EquipTrinket(TrinketEquipment trinketToEquip)
    {
        SetEquippedTrinket(trinketToEquip);

        UpdateStats();
    }

    #endregion

    #region Weapons

    public void UnequipWeapon(EnumHandler.EquipmentHandSlots handSlot)
    {
        switch (handSlot)
        {
            case EnumHandler.EquipmentHandSlots.MAINHAND:
                if (GetEquippedMainHand() != null)
                {
                    heroManager.HeroInventory().AddToInventory(GetEquippedMainHand());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedMainHand().name + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedMainHand(null);
                }
                break;
            case EnumHandler.EquipmentHandSlots.OFFHAND:

                break;
        }

        UpdateStats();
    }

    public void EquipWeapon(WeaponEquipment weaponToEquip)
    {
        switch (weaponToEquip.equipSlot)
        {
            case EnumHandler.EquipmentHandSlots.MAINHAND:
                SetEquippedMainHand(weaponToEquip);
                break;
            case EnumHandler.EquipmentHandSlots.OFFHAND:
                // Will be updated to allow for different types of equipment here
                break;
        }

        UpdateStats();
    }

    #endregion
}

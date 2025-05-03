using UnityEngine;

public class HeroEquipment : MonoBehaviour
{
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


    [SerializeField] RingEquipment equippedRing1;
    [SerializeField] RingEquipment equippedRing2;
    [SerializeField] RelicEquipment equippedRelic1;
    [SerializeField] RelicEquipment equippedRelic2;
    [SerializeField] TrinketEquipment equippedTrinket;

    [SerializeField] WeaponEquipment equippedMainHand;
    [SerializeField] ShieldEquipment equippedShield; // will need to be reworked to allow dual wielding.  But for right now, shield is ok.

    HeroManager heroManager;

    void Awake()
    {
        heroManager = GetComponent<HeroManager>();
    }

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

    public void UnequipArmorFromHero(EnumHandler.EquipmentArmorSlots armorSlot)
    {
            switch (armorSlot)
            {
                case EnumHandler.EquipmentArmorSlots.HEAD:
                    if (heroManager.HeroEquipment().GetEquippedHead() != null)
                    {
                        heroManager.HeroInventory().AddToInventory(heroManager.HeroEquipment().GetEquippedHead());
                        DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedHead() + " from " + heroManager.Hero().GetName(), false, false);
                        SetEquippedHead(null);                        
                    }
                    break;
            case EnumHandler.EquipmentArmorSlots.CHEST:
                if (heroManager.HeroEquipment().GetEquippedChest() != null)
                {
                    heroManager.HeroInventory().AddToInventory(heroManager.HeroEquipment().GetEquippedChest());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedChest() + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedChest(null);
                }
                break;
            case EnumHandler.EquipmentArmorSlots.HANDS:
                if (heroManager.HeroEquipment().GetEquippedHands() != null)
                {
                    heroManager.HeroInventory().AddToInventory(heroManager.HeroEquipment().GetEquippedHands());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedHands() + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedHands(null);
                }
                break;
            case EnumHandler.EquipmentArmorSlots.LEGS:
                if (heroManager.HeroEquipment().GetEquippedLegs() != null)
                {
                    heroManager.HeroInventory().AddToInventory(heroManager.HeroEquipment().GetEquippedLegs());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedLegs() + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedLegs(null);
                }
                break;
            case EnumHandler.EquipmentArmorSlots.FEET:
                if (heroManager.HeroEquipment().GetEquippedFeet() != null)
                {
                    heroManager.HeroInventory().AddToInventory(heroManager.HeroEquipment().GetEquippedFeet());
                    DebugManager.i.InventoryDebugOut("HeroEquipment", "Unequipping " + GetEquippedFeet() + " from " + heroManager.Hero().GetName(), false, false);
                    SetEquippedFeet(null);
                }
                break;
        }
        return;
    }

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
}

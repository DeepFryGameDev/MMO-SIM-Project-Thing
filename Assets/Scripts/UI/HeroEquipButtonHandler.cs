using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroEquipButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image iconImage;

    Sprite defaultSprite;

    HeroManager heroManager;

    HeroEquipMenuHandler heroEquipMenuHandler;

    HeroBaseEquipment assignedEquipment;
    public void SetAssignedEquipment(HeroBaseEquipment equipment) { assignedEquipment = equipment; }
    public HeroBaseEquipment GetAssignedEquipment() { return assignedEquipment; }

    private void Awake()
    {
        iconImage = transform.Find("EquipSlotIcon").GetComponent<Image>();

        defaultSprite = iconImage.sprite;

        heroEquipMenuHandler = FindFirstObjectByType<HeroEquipMenuHandler>();
    }

    public void OnClick(int slotIndex)
    {
        heroManager = HomeZoneManager.i.GetHeroManager();
        heroEquipMenuHandler.SetEquipmentClickedInMenu(assignedEquipment);

        List<ArmorEquipment> armorEquipmentInInventory;
        List<WeaponEquipment> weaponEquipmentInInventory;
        List<RingEquipment> ringEquipmentInInventory;
        List<RelicEquipment> relicEquipmentInInventory;
        List<TrinketEquipment> trinketEquipmentInInventory;

        /* 0 - helm
         * 1 - chest
         * 2 - hands
         * 3 - pants
         * 4 - feet
         * 5 - ring 1
         * 6 - ring 2
         * 7 - relic 1
         * 8 - relic 2
         * 9 - trinket
         * 10 - mainhand
         * 11 - offhand
         */

        switch (slotIndex)
        {
            case 0: // helm
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the helm equip slot");

                // get all helmets in inventory that both match the hero's wearable armor class, and helmet slot
                armorEquipmentInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.HEAD);
                if (armorEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip helmet
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(armorEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedHead() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable helmets in inventory and not wearing any helmet", true, false);
                    } else
                    {
                        // Display a blank icon to allow player to unequip helmet
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                    break;
            case 1: // chest
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the chest equip slot");

                // get all chests in inventory that both match the hero's wearable armor class, and chest slot
                armorEquipmentInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.CHEST);
                if (armorEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip chest
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(armorEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedChest() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable chests in inventory and not wearing any chest", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip chest
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 2: // hands
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the hands equip slot");

                // get all hands in inventory that both match the hero's wearable armor class, and hands slot
                armorEquipmentInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.HANDS);
                if (armorEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip hands
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(armorEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedHands() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable hands in inventory and not wearing any hands", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip hands
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 3: // pants
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the legs equip slot");

                // get all legs in inventory that both match the hero's wearable armor class, and legs slot
                armorEquipmentInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.LEGS);
                if (armorEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip legs
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(armorEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedLegs() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable legs in inventory and not wearing any legs", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip legs
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 4: // feet
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the feet equip slot");

                // get all feet in inventory that both match the hero's wearable armor class, and feet slot
                armorEquipmentInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.FEET);
                if (armorEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip feet
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(armorEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedFeet() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable feet in inventory and not wearing any feet", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip feet
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 5: // ring 1
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the ring 1 equip slot");
                // get all rings in inventory
                ringEquipmentInInventory = RingEquipmentInInventory();
                if (ringEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip ring
                    InstantiateUnequipIcon();

                    // Display equippable rings
                    InstantiateEquippableRings(ringEquipmentInInventory, 1);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedRing1() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable rings in inventory and not wearing any rings", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip ring
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 6: // ring 2
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the ring 2 equip slot");
                // get all rings in inventory
                ringEquipmentInInventory = RingEquipmentInInventory();
                if (ringEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip ring
                    InstantiateUnequipIcon();

                    // Display equippable rings
                    InstantiateEquippableRings(ringEquipmentInInventory, 2);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedRing2() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable rings in inventory and not wearing any rings", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip ring
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                    break;
            case 7: // relic 1
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the relic 1 equip slot");

                // get all relics in inventory
                relicEquipmentInInventory = RelicEquipmentInInventory();
                if (relicEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip relic
                    InstantiateUnequipIcon();

                    // Display equippable relics
                    InstantiateEquippableRelics(relicEquipmentInInventory, 1);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedRelic1() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable relics in inventory and not wearing any relics", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip relic
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 8: // relic 2
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the relic 2 equip slot");

                // get all relics in inventory
                relicEquipmentInInventory = RelicEquipmentInInventory();
                if (relicEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip relic
                    InstantiateUnequipIcon();

                    // Display equippable relics
                    InstantiateEquippableRelics(relicEquipmentInInventory, 2);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedRelic2() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable relics in inventory and not wearing any relics", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip relic
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 9: // trinket
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the trinket equip slot");

                // get all relics in inventory
                trinketEquipmentInInventory = TrinketEquipmentInInventory();
                if (trinketEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip relic
                    InstantiateUnequipIcon();

                    // Display equippable trinkets
                    InstantiateEquippableTrinkets(trinketEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedTrinket() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable trinket in inventory and not wearing any trinkets", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip relic
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }

                break;
            case 10: // mainhand
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the main hand equip slot");

                // get all equipment in inventory that both match the hero's equippable weapons and main hand slot
                weaponEquipmentInInventory = WeaponEquipmentInInventoryBySlot(EnumHandler.EquipmentHandSlots.MAINHAND);
                if (weaponEquipmentInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip weapon
                    InstantiateUnequipIcon();

                    // Display equippable weapons
                    InstantiateEquippableWeapons(weaponEquipmentInInventory);

                    // Display the EquipScroll list
                    heroEquipMenuHandler.ToggleEquipScroll(true);
                }
                else
                {
                    if (heroManager.HeroEquipment().GetEquippedMainHand() == null)
                    {
                        DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "No wearable MainHand in inventory and not wearing any MainHand", true, false);
                    }
                    else
                    {
                        // Display a blank icon to allow player to unequip weapon
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 11: // offhand
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the off hand equip slot - NOT YET IMPLEMENTED.", true, false);
                break;
        }
    }

    void InstantiateEquippableArmor(List<ArmorEquipment> wearableEquipmentList)
    {
        foreach (ArmorEquipment armor in wearableEquipmentList)
        {
            // DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "List out equipment button for " + armor.name + " / " + armor.ID.ToString());
            // instantiate EquipToHeroButton to transform "EquipScroll/Viewport/LayoutGroup"
            GameObject newEquipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());

            // Set the EquipToHeroButtonHandler details about the item 
            EquipToHeroButtonHandler handler = newEquipButton.GetComponent<EquipToHeroButtonHandler>();

            handler.SetAssignedEquipment(armor);
            handler.SetIcon();
            handler.SetHeroManager(heroManager);
        }
    }

    void InstantiateEquippableWeapons(List<WeaponEquipment> wearableEquipmentList)
    {
        foreach (WeaponEquipment weapon in wearableEquipmentList)
        {
            // DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "List out equipment button for " + weapon.name + " / " + weapon.ID.ToString());
            // instantiate EquipToHeroButton to transform "EquipScroll/Viewport/LayoutGroup"
            GameObject newEquipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());

            // Set the EquipToHeroButtonHandler details about the item 
            EquipToHeroButtonHandler handler = newEquipButton.GetComponent<EquipToHeroButtonHandler>();

            handler.SetAssignedEquipment(weapon);
            handler.SetIcon();
            handler.SetHeroManager(heroManager);
        }
    }

    void InstantiateEquippableRings(List<RingEquipment> wearableEquipmentList, int buttonSlotClicked)
    {
        foreach (RingEquipment ring in wearableEquipmentList)
        {
            // DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "List out equipment button for " + ring.name + " / " + ring.ID.ToString());
            // instantiate EquipToHeroButton to transform "EquipScroll/Viewport/LayoutGroup"
            GameObject newEquipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());

            // Set the EquipToHeroButtonHandler details about the item 
            EquipToHeroButtonHandler handler = newEquipButton.GetComponent<EquipToHeroButtonHandler>();

            handler.SetAssignedEquipment(ring);
            handler.SetIcon();
            handler.SetHeroManager(heroManager);

            handler.SetRingSlotClicked(buttonSlotClicked);
        }
    }

    void InstantiateEquippableRelics(List<RelicEquipment> wearableEquipmentList, int buttonSlotClicked)
    {
        foreach (RelicEquipment relic in wearableEquipmentList)
        {
            // DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "List out equipment button for " + ring.name + " / " + ring.ID.ToString());
            // instantiate EquipToHeroButton to transform "EquipScroll/Viewport/LayoutGroup"
            GameObject newEquipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());

            // Set the EquipToHeroButtonHandler details about the item 
            EquipToHeroButtonHandler handler = newEquipButton.GetComponent<EquipToHeroButtonHandler>();

            handler.SetAssignedEquipment(relic);
            handler.SetIcon();
            handler.SetHeroManager(heroManager);

            handler.SetRelicSlotClicked(buttonSlotClicked);
        }
    }

    void InstantiateEquippableTrinkets(List<TrinketEquipment> wearableEquipmentList)
    {
        foreach (TrinketEquipment trinket in wearableEquipmentList)
        {
            // DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "List out equipment button for " + ring.name + " / " + ring.ID.ToString());
            // instantiate EquipToHeroButton to transform "EquipScroll/Viewport/LayoutGroup"
            GameObject newEquipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());

            // Set the EquipToHeroButtonHandler details about the item 
            EquipToHeroButtonHandler handler = newEquipButton.GetComponent<EquipToHeroButtonHandler>();

            handler.SetAssignedEquipment(trinket);
            handler.SetIcon();
            handler.SetHeroManager(heroManager);
        }
    }

    void InstantiateUnequipIcon()
    {
        GameObject unequipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());
        EquipToHeroButtonHandler unequipHandler = unequipButton.GetComponent<EquipToHeroButtonHandler>();
        unequipHandler.ToggleIsUnequip();
        unequipHandler.SetHeroManager(heroManager);

        
    }

    List<ArmorEquipment> ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots armorSlot)
    {
        List<HeroItem> inventory = heroManager.HeroInventory().GetInventory();

        List<ArmorEquipment> listToDisplay = new List<ArmorEquipment>();

        foreach (HeroItem item in inventory)
        {
            if (item is ArmorEquipment)
            {
                ArmorEquipment armorItem = item as ArmorEquipment;
                if (armorItem.equipSlot == armorSlot && ClassManager.i.CanWearArmorClass(heroManager.HeroClass().GetCurrentClass(), armorItem.armorClass))
                {
                    listToDisplay.Add(armorItem);
                }
            }
        }

        return listToDisplay;
    }

    List<WeaponEquipment> WeaponEquipmentInInventoryBySlot(EnumHandler.EquipmentHandSlots handSlot)
    {
        List<HeroItem> inventory = heroManager.HeroInventory().GetInventory();

        List<WeaponEquipment> listToDisplay = new List<WeaponEquipment>();

        foreach (HeroItem item in inventory)
        {
            if (item is WeaponEquipment)
            {
                WeaponEquipment weaponItem = item as WeaponEquipment;
                if (weaponItem.equipSlot == handSlot && ClassManager.i.CanEquipWeaponClass(heroManager.HeroClass().GetCurrentClass(), weaponItem.weaponClass))
                {
                    listToDisplay.Add(weaponItem);
                }
            }
        }

        return listToDisplay;
    }

    List <RingEquipment> RingEquipmentInInventory()
    {
        List<HeroItem> inventory = heroManager.HeroInventory().GetInventory();

        List<RingEquipment> listToDisplay = new List<RingEquipment>();

        foreach (HeroItem item in inventory)
        {
            if (item is RingEquipment)
            {
                RingEquipment ringItem = item as RingEquipment;
                listToDisplay.Add(ringItem);
            }
        }

        return listToDisplay;
    }

    List<RelicEquipment> RelicEquipmentInInventory()
    {
        List<HeroItem> inventory = heroManager.HeroInventory().GetInventory();

        List<RelicEquipment> listToDisplay = new List<RelicEquipment>();

        foreach (HeroItem item in inventory)
        {
            if (item is RelicEquipment)
            {
                RelicEquipment relicItem = item as RelicEquipment;
                listToDisplay.Add(relicItem);
            }
        }

        return listToDisplay;
    }

    List<TrinketEquipment> TrinketEquipmentInInventory()
    {
        List<HeroItem> inventory = heroManager.HeroInventory().GetInventory();

        List<TrinketEquipment> listToDisplay = new List<TrinketEquipment>();

        foreach (HeroItem item in inventory)
        {
            if (item is TrinketEquipment)
            {
                TrinketEquipment trinketItem = item as TrinketEquipment;
                listToDisplay.Add(trinketItem);
            }
        }

        return listToDisplay;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (assignedEquipment != null)
        {
            ArmorEquipment equipAsArmor = assignedEquipment as ArmorEquipment;
            if (equipAsArmor != null)
            {
                heroEquipMenuHandler.DrawArmorEquipmentDetails(equipAsArmor);
                return;
            }

            WeaponEquipment equipAsWeapon = assignedEquipment as WeaponEquipment;
            if (equipAsWeapon != null)
            {
                heroEquipMenuHandler.DrawWeaponEquipmentDetails(equipAsWeapon);
                return;
            }

            RingEquipment equipAsRing = assignedEquipment as RingEquipment;
            if (equipAsRing != null)
            {
                heroEquipMenuHandler.DrawRingEquipmentDetails(equipAsRing);
                return;
            }

            RelicEquipment equipAsRelic = assignedEquipment as RelicEquipment;
            if (equipAsRelic != null)
            {
                heroEquipMenuHandler.DrawRelicEquipmentDetails(equipAsRelic);
                return;
            }

            TrinketEquipment equipAsTrinket = assignedEquipment as TrinketEquipment;
            if (equipAsTrinket != null)
            {
                heroEquipMenuHandler.DrawTrinketEquipmentDetails(equipAsTrinket);
                return;
            }

            ShieldEquipment equipAsShield = assignedEquipment as ShieldEquipment;
            if (equipAsShield != null)
            {
                heroEquipMenuHandler.DrawShieldEquipmentDetails(equipAsShield);
                return;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        heroEquipMenuHandler.ClearEquipmentDetails();
    }
}

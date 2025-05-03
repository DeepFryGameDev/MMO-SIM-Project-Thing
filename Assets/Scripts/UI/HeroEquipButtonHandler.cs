using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                List<ArmorEquipment> wearableHelmsInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.HEAD);
                if (wearableHelmsInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip helmet
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(wearableHelmsInInventory);

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

                // get all chests in inventory that both match the hero's wearable armor class, and helmet slot
                List<ArmorEquipment> wearableChestsInInventory = ArmorEquipmentInInventoryBySlot(EnumHandler.EquipmentArmorSlots.CHEST);
                if (wearableChestsInInventory.Count > 0)
                {
                    // Display a blank icon to allow player to unequip helmet
                    InstantiateUnequipIcon();

                    // Display equippable armor
                    InstantiateEquippableArmor(wearableChestsInInventory);

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
                        // Display a blank icon to allow player to unequip helmet
                        InstantiateUnequipIcon();

                        // Display the EquipScroll list
                        heroEquipMenuHandler.ToggleEquipScroll(true);
                    }
                }
                break;
            case 2: // hands
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the hands equip slot");
                break;
            case 3: // pants
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the pants equip slot");
                break;
            case 4: // feet
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the feet equip slot");
                break;
            case 5: // ring 1
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the ring 1 equip slot");
                break;
            case 6: // ring 2
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the ring 2 equip slot");
                break;
            case 7: // relic 1
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the relic 1 equip slot");
                break;
            case 8: // relic 2
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the relic 2 equip slot");
                break;
            case 9: // trinket
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the trinket equip slot");
                break;
            case 10: // mainhand
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the main hand equip slot");
                break;
            case 11: // offhand
                DebugManager.i.HeroDebugOut("HeroEquipButtonHandler", "Clicked the off hand equip slot");
                break;
        }
    }

    void InstantiateEquippableArmor(List<ArmorEquipment> wearableEquipmentList)
    {
        foreach (ArmorEquipment armor in wearableEquipmentList)
        {
            DebugManager.i.UIDebugOut("HeroEquipButtonHandler", "List out equipment button for " + armor.name + " / " + armor.ID.ToString());
            // instantiate EquipToHeroButton to transform "EquipScroll/Viewport/LayoutGroup"
            GameObject newEquipButton = Instantiate(PrefabManager.i.EquipToHeroButton, heroEquipMenuHandler.GetEquipInventoryTransform());

            // Set the EquipToHeroButtonHandler details about the item 
            EquipToHeroButtonHandler handler = newEquipButton.GetComponent<EquipToHeroButtonHandler>();

            handler.SetEquipment(armor);
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
                if (armorItem.equipSlot == armorSlot && CanWearArmorClass(heroManager.HeroClass().GetArmorClass(), armorItem.armorClass))
                {
                    listToDisplay.Add(armorItem);
                }
            }
        }

        return listToDisplay;
    }

    /// <summary>
    /// Checks if the given hero's class armor class is able to equip the armor being checked.  Plate wearers can equip everything, cloth can only equip cloth, and in between.
    /// </summary>
    /// <param name="herosArmorClass">Hero's armor class to compare with</param>
    /// <param name="itemsArmorClass">Armor Class of the item to be checked</param>
    /// <returns></returns>
    bool CanWearArmorClass(EnumHandler.ArmorClasses herosArmorClass, EnumHandler.ArmorClasses itemsArmorClass)
    {
        switch (herosArmorClass)
        {
            case EnumHandler.ArmorClasses.CLOTH:
                if (itemsArmorClass == EnumHandler.ArmorClasses.PLATE || itemsArmorClass == EnumHandler.ArmorClasses.MAIL || itemsArmorClass == EnumHandler.ArmorClasses.LEATHER) { return false; }
                return true;
            case EnumHandler.ArmorClasses.LEATHER:
                if (itemsArmorClass == EnumHandler.ArmorClasses.PLATE || itemsArmorClass == EnumHandler.ArmorClasses.MAIL) { return false; }
                return true;
            case EnumHandler.ArmorClasses.MAIL:
                if (itemsArmorClass == EnumHandler.ArmorClasses.PLATE) { return false; }
                return true;
            case EnumHandler.ArmorClasses.PLATE:
                return true;
            default:
                DebugManager.i.ClassDebugOut("ClassManager", "CanWearArmorClass - heroArmorClass not found.  Defaulting to false.", true, false);
                return false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // show armor details
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // hide armor details
    }
}

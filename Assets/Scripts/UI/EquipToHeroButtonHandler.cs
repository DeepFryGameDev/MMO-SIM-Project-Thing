using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Purpose: Used to facilitate interaction between the user and the instantiated buttons that appear when clicking on an inventory button
// Directions: Attach to the 'EquipToHeroButton' prefab in Prefabs/UI folder
// Other notes: 

public class EquipToHeroButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    HeroBaseEquipment assignedEquipment;
    public void SetAssignedEquipment(HeroBaseEquipment equipment) { this.assignedEquipment = equipment; }
    public HeroBaseEquipment GetAssignedEquipment() { return assignedEquipment; }

    bool isUnequip = false;
    public void ToggleIsUnequip() { isUnequip = !isUnequip; }

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }

    public void SetIcon()
    {
        transform.Find("Icon").GetComponent<Image>().sprite = assignedEquipment.icon;
    }

    HeroEquipMenuHandler heroEquipMenuHandler;

    int ringSlotClicked = 0;
    public void SetRingSlotClicked(int slotClicked) { ringSlotClicked = slotClicked; }

    int relicSlotClicked = 0;
    public void SetRelicSlotClicked(int slotClicked) { relicSlotClicked = slotClicked; }

    void Awake()
    {
        heroEquipMenuHandler = FindFirstObjectByType<HeroEquipMenuHandler>();
    }

    /// <summary>
    /// Called when the user clicks the button attached to this object
    /// </summary>
    public void OnClick()
    {
        SetEquipment();

        RefreshUI();
    }

    /// <summary>
    /// This is only called from OnClick.  
    /// If this button is set as the 'unEquip' button, the behavior of this button should be to Unequip the current equipment.
    /// Otherwise it should equip the assignedEquipment.
    /// </summary>
    void SetEquipment()
    {
        if (isUnequip) RunUnequip();
        else RunEquip();
    }

    /// <summary>
    /// Equips the assignedEquipment to the appropriate equipment slot
    /// </summary>
    void RunEquip()
    {
        ArmorEquipment equipAsArmor = assignedEquipment as ArmorEquipment;
        if (equipAsArmor != null) // we are trying to unequip armor
        {
            // if hero has anything equipped in this equipment slot, unequip it.
            heroManager.HeroEquipment().UnequipArmor(equipAsArmor.equipSlot);

            // then, set the equipment here to the hero's equipment in that slot.
            heroManager.HeroEquipment().EquipArmor(equipAsArmor);

            return;
        }

        WeaponEquipment equipAsWeapon = assignedEquipment as WeaponEquipment;
        if (equipAsWeapon != null) // we are trying to unequip weapon
        {
            // if hero has anything equipped in this equipment slot, unequip it.
            heroManager.HeroEquipment().UnequipWeapon(equipAsWeapon.equipSlot);

            // then, set the equipment here to the hero's equipment in that slot.
            heroManager.HeroEquipment().EquipWeapon(equipAsWeapon);

            return;
        }

        RingEquipment equipAsRing = assignedEquipment as RingEquipment;
        if (equipAsRing != null) // we are trying to unequip armor
        {
            // if hero has anything equipped in this equipment slot, unequip it.
            heroManager.HeroEquipment().UnequipRing(ringSlotClicked);

            // then, set the equipment here to the hero's equipment in that slot.
            heroManager.HeroEquipment().EquipRing(equipAsRing, ringSlotClicked);

            return;
        }


        RelicEquipment equipAsRelic = assignedEquipment as RelicEquipment;
        if (equipAsRelic != null) // we are trying to unequip armor
        {
            // if hero has anything equipped in this equipment slot, unequip it.
            heroManager.HeroEquipment().UnequipRelic(relicSlotClicked);

            // then, set the equipment here to the hero's equipment in that slot.
            heroManager.HeroEquipment().EquipRelic(equipAsRelic, relicSlotClicked);

            return;
        }

        TrinketEquipment equipAsTrinket = assignedEquipment as TrinketEquipment;
        if (equipAsTrinket != null) // we are trying to unequip armor
        {
            // if hero has anything equipped in this equipment slot, unequip it.
            heroManager.HeroEquipment().UnequipTrinket();

            // then, set the equipment here to the hero's equipment in that slot.
            heroManager.HeroEquipment().EquipTrinket(equipAsTrinket);
        }
    }

    /// <summary>
    /// Unequips the currently equipped equipment in the appropriate slot
    /// </summary>
    void RunUnequip()
    {
        DebugManager.i.UIDebugOut("EquipToHeroButtonHandler", "Unequip button was clicked.", false, false);

        ArmorEquipment equipAsArmor = heroEquipMenuHandler.GetEquipmentClickedInMenu() as ArmorEquipment;
        if (equipAsArmor != null) // user clicked valid equipment to replace in menu
        {
            heroManager.HeroEquipment().UnequipArmor(equipAsArmor.equipSlot);
            return;
        }

        WeaponEquipment equipAsWeapon = heroEquipMenuHandler.GetEquipmentClickedInMenu() as WeaponEquipment;
        if (equipAsWeapon != null) // user clicked valid equipment to replace in menu
        {
            heroManager.HeroEquipment().UnequipWeapon(equipAsWeapon.equipSlot);
            return;
        }

        RingEquipment equipAsRing = heroEquipMenuHandler.GetEquipmentClickedInMenu() as RingEquipment;
        if (equipAsRing != null) // user clicked valid equipment to replace in menu
        {
            heroManager.HeroEquipment().UnequipRing(ringSlotClicked);
            return;
        }

        RelicEquipment equipAsRelic = heroEquipMenuHandler.GetEquipmentClickedInMenu() as RelicEquipment;
        if (equipAsRelic != null) // user clicked valid equipment to replace in menu
        {
            heroManager.HeroEquipment().UnequipRing(relicSlotClicked);
            return;
        }

        TrinketEquipment equipAsTrinket = heroEquipMenuHandler.GetEquipmentClickedInMenu() as TrinketEquipment;
        if (equipAsTrinket != null) // user clicked valid equipment to replace in menu
        {
            heroManager.HeroEquipment().UnequipTrinket();
        }
    }

    /// <summary>
    /// Clears all the UI objects and sets them as appropriate again
    /// </summary>
    void RefreshUI()
    {
        DebugManager.i.UIDebugOut("EquipToHeroButtonHandler", "Refreshing UI");

        heroEquipMenuHandler.GenerateEquippedEquipmentButtons(heroManager);

        heroEquipMenuHandler.ToggleEquipScroll(false);

        heroEquipMenuHandler.ClearEquipmentDetails();

        // clear inventory list
        heroEquipMenuHandler.ClearInventoryList();

        StatusMenuHandler.i.SetHeroManager(heroManager);
        StatusMenuHandler.i.SetStatusValues();
    }

   /// <summary>
   /// Shows the equipment's details when the user puts the mouse cursor over the object
   /// </summary>
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

    /// <summary>
    /// Hides the equipment's details when the user's mouse cursor exits the object's boundary
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        heroEquipMenuHandler.ClearEquipmentDetails();
    }
}

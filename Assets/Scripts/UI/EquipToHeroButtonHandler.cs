using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipToHeroButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    HeroBaseEquipment equipment;
    public void SetEquipment(HeroBaseEquipment equipment) { this.equipment = equipment; }
    public HeroBaseEquipment GetEquipment() { return equipment; }

    bool isUnequip = false;
    public void ToggleIsUnequip() { isUnequip = !isUnequip; }

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }

    public void SetIcon()
    {
        transform.Find("Icon").GetComponent<Image>().sprite = equipment.icon;
    }

    HeroEquipMenuHandler heroEquipMenuHandler;

    void Awake()
    {
        heroEquipMenuHandler = FindFirstObjectByType<HeroEquipMenuHandler>();
    }

    public void OnClick()
    {
        if (isUnequip)
        {
            DebugManager.i.UIDebugOut("EquipToHeroButtonHandler", "Unequip button was clicked.", false, false);

            ArmorEquipment equipAsArmor = heroEquipMenuHandler.GetEquipmentClickedInMenu() as ArmorEquipment;
            if (equipAsArmor != null) // user clicked valid equipment to replace in menu
            {
                heroManager.HeroEquipment().UnequipArmorFromHero(equipAsArmor.equipSlot);
            }
        } else
        {
            ArmorEquipment equipAsArmor = equipment as ArmorEquipment;
            if (equipAsArmor != null) // we are trying to unequip armor
            {
                // if hero has anything equipped in this equipment slot, unequip it.
                heroManager.HeroEquipment().UnequipArmorFromHero(equipAsArmor.equipSlot);

                // then, set the equipment here to the hero's equipment in that slot.
                heroManager.HeroEquipment().EquipArmor(equipAsArmor);

                // then, remove it from the hero's inventory.
                heroManager.HeroInventory().RemoveFromInventory(equipAsArmor);
            }
        }

        heroEquipMenuHandler.GenerateEquippedEquipmentButtons(heroManager);

        heroEquipMenuHandler.ToggleEquipScroll(false);

        heroEquipMenuHandler.ClearEquipmentDetails();

        // clear inventory list
        heroEquipMenuHandler.ClearInventoryList();
    }



    // show equip details
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equipment != null)
        {
            ArmorEquipment equipAsArmor = equipment as ArmorEquipment;
            if (equipAsArmor != null)
            {
                heroEquipMenuHandler.ShowArmorEquipmentDetails(equipAsArmor);
            }
        }       
    }

    // hide equip details
    public void OnPointerExit(PointerEventData eventData)
    {
        heroEquipMenuHandler.ClearEquipmentDetails();
    }
}

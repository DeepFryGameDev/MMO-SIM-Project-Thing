using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroInventoryButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemIcon;
    public void SetItemIcon(Sprite sprite) { itemIcon.sprite = sprite; }

    [SerializeField] TextMeshProUGUI countText;
    public void SetCountText(string text) { countText.text = text; }

    [SerializeField] CanvasGroup countBG;
    public void HideCountBG () { countBG.alpha = 0; }

    ContextMenuHandler contextMenuHandler;

    HeroItem item;
    public void SetItem(HeroItem item) { this.item = item; }

    bool rightClickAvailable;

    HeroInventoryUIHandler inventoryHandler;
    public void SetHandler(HeroInventoryUIHandler inventoryHandler) { this.inventoryHandler = inventoryHandler; }

    void Awake()
    {
        contextMenuHandler = FindFirstObjectByType<ContextMenuHandler>();
    }

    void Update()
    {
        if (rightClickAvailable && Input.GetMouseButtonDown(1)) 
        {
            ProcessContextMenu();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rightClickAvailable = true;

        if (item is TrainingEquipment)
        {
            inventoryHandler.DrawTrainingEquipUI(item as TrainingEquipment);
            return;
        }

        ArmorEquipment equipAsArmor = item as ArmorEquipment;
        if (equipAsArmor != null)
        {
            inventoryHandler.DrawArmorEquipmentDetails(equipAsArmor);
            return;
        }

        WeaponEquipment equipAsWeapon = item as WeaponEquipment;
        if (equipAsWeapon != null)
        {
            inventoryHandler.DrawWeaponEquipmentDetails(equipAsWeapon);
            return;
        }

        RingEquipment equipAsRing = item as RingEquipment;
        if (equipAsRing != null)
        {
            inventoryHandler.DrawRingEquipmentDetails(equipAsRing);
            return;
        }

        RelicEquipment equipAsRelic = item as RelicEquipment;
        if (equipAsRelic != null)
        {
            inventoryHandler.DrawRelicEquipmentDetails(equipAsRelic);
            return;
        }

        TrinketEquipment equipAsTrinket = item as TrinketEquipment;
        if (equipAsTrinket != null)
        {
            inventoryHandler.DrawTrinketEquipmentDetails(equipAsTrinket);
            return;
        }

        ShieldEquipment equipAsShield = item as ShieldEquipment;
        if (equipAsShield != null)
        {
            inventoryHandler.DrawShieldEquipmentDetails(equipAsShield);
            return;
        }

        ConsumableItem itemAsConsumable = item as ConsumableItem;
        if (itemAsConsumable != null)
        {
            DebugManager.i.UIDebugOut("HeroInventoryButtonHandler.OnPointerEnter()", "Consumable Items not yet implemented.  Just showing base stuff");
            inventoryHandler.DrawBaseItemDetails(item);
            return;
        }

        if (item is JunkItem || item is CommonItem)
        {
            inventoryHandler.DrawBaseItemDetails(item);
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryHandler.ResetUI();

        rightClickAvailable = false;
    }

    public void OnClick()
    {
        DebugManager.i.UIDebugOut("HeroInventoryButtonHandler.OnClick()", "Left click not yet implemented.  Will eventually just use the item if it's a consumable.");
    }

    void ProcessContextMenu()
    {
        // this will be the dropdown list of options the user can click

        DebugManager.i.UIDebugOut("HeroInventoryButtonHandler.ProcessContextMenu()", "Context menu presented for " + item.name);

        // Close any other existing context menus
        contextMenuHandler.CloseGiveToHeroList();

        contextMenuHandler.OpenContextMenu(item);
    }
}

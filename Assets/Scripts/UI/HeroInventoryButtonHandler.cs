using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroInventoryButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemIcon;
    public void SetItemIcon(Sprite sprite) { itemIcon.sprite = sprite; }

    [SerializeField] TextMeshProUGUI countText;
    public void SetCountText(string text) { countText.text = text; }

    [SerializeField] CanvasGroup countBG;
    public void HideCountBG () { countBG.alpha = 0; }

    HeroItem item;
    public void SetItem(HeroItem item) { this.item = item; }

    HeroInventoryUIHandler inventoryHandler;
    public void SetHandler(HeroInventoryUIHandler inventoryHandler) { this.inventoryHandler = inventoryHandler; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryHandler.DrawBaseItemUI(item);

        if (item is TrainingEquipment)
        {
            inventoryHandler.DrawTrainingEquipUI(item as TrainingEquipment);
        }
        else if (item is WeaponEquipment)
        {
            inventoryHandler.DrawEquipmentWeaponUI(item as WeaponEquipment);
        } else if (item is ShieldEquipment)
        {
            inventoryHandler.DrawEquipmentShieldUI(item as ShieldEquipment);
        }
        else if (item is ArmorEquipment)
        {
            inventoryHandler.DrawEquipmentArmorUI(item as ArmorEquipment);
        } // jewelry still needs to be added
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryHandler.ResetUI();
    }
}

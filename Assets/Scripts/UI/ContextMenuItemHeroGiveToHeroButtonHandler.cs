using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Purpose: 
// Directions: 
// Other notes: 

public class ContextMenuItemHeroGiveToHeroButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI nameText;
    public void SetNameText(string name) { nameText.SetText(name); }

    HeroManager giveToHeroManager;
    public void SetGiveToHeroManager (HeroManager heroManager) { this.giveToHeroManager = heroManager; }

    HeroManager giveFromHeroManager;
    public void SetGiveFromHeroManager (HeroManager heroManager) { this.giveFromHeroManager = heroManager; }

    HeroItem itemToSwap;
    public void SetItemToSwap(HeroItem itemToSwap) { this.itemToSwap = itemToSwap; }

    ContextMenuHandler contextMenuHandler;

    HeroInventoryUIHandler inventoryUIHandler;

    private void Awake()
    {
        nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        contextMenuHandler = FindFirstObjectByType<ContextMenuHandler>();

        inventoryUIHandler = FindFirstObjectByType<HeroInventoryUIHandler>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(true);
        contextMenuHandler.SetBlockCloseOnHeroList(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(false);
        contextMenuHandler.SetBlockCloseOnHeroList(false);
    }

    public void OnClick()
    {
        DebugManager.i.InventoryDebugOut("Inventory - ContextMenu", "Removing " + itemToSwap.name + " from " + giveFromHeroManager.Hero().GetName() + " and giving it to " + giveToHeroManager.Hero().GetName());
        giveFromHeroManager.HeroInventory().RemoveFromInventory(itemToSwap);
        giveToHeroManager.HeroInventory().AddToInventory(itemToSwap);

        // close context menu
        contextMenuHandler.CloseContextMenu();
        contextMenuHandler.CloseGiveToHeroList();

        // refresh ui
        inventoryUIHandler.ResetUI();
        inventoryUIHandler.ClearUI();

        inventoryUIHandler.SetHeroDetailsPanel(giveFromHeroManager);
        inventoryUIHandler.GenerateInventory(giveFromHeroManager);
    }
}

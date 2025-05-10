using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Purpose: Facilitates action taken when the user clicks the designated Hero in the 'GiveToHeroList' transform in the Inventory menu.
// Directions: Attach to the prefab used for instantiating the hero buttons - Prefabs/UI/ContextMenuItemHeroGiveToHeroButton
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

    /// <summary>
    /// Called when the user's mouse cursor enters the menu button's bounds.  Just disables closing the context menu when left clicking it.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(true);
        contextMenuHandler.SetBlockCloseOnHeroList(true);
    }

    /// <summary>
    /// Called when the user's mouse cursor exits the menu button's bounds.  It allows the user to close the context menu when left clicking outside of it
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(false);
        contextMenuHandler.SetBlockCloseOnHeroList(false);
    }

    /// <summary>
    /// Called when the user clicks the button this script is attached to.  It will remove the item from the original hero's inventory and add it to the clicked hero's inventory.
    /// </summary>
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

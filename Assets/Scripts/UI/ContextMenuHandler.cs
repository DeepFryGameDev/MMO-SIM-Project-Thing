using UnityEngine;

// Purpose: Used to facilitate action with interacting with the context menu in the UI. (This is the menu that appears when right clicking on a UI object)
// Directions: Attach to [UI]/HeroInventoryCanvas/InventoryContextMenu
// Other notes: Should eventually house all the functionality for context menus?

public class ContextMenuHandler : MonoBehaviour
{
    Canvas parentCanvas;
    public Canvas GetParentCanvas() { return parentCanvas; }

    CanvasGroup canvasGroup;

    bool blockClose;
    public bool GetBlockClose() { return blockClose; }
    public void SetBlockClose(bool set) { blockClose = set; }

    bool blockCloseOnHeroList;
    public bool GetBlockCloseOnHeroList() { return blockCloseOnHeroList; }
    public void SetBlockCloseOnHeroList(bool set) { blockCloseOnHeroList = set; }

    HeroItem clickedItem;

    bool contextMenuOpen;
    public bool GetContextMenuOpen() { return contextMenuOpen; }

    [SerializeField] Transform giveToHeroListTransform;
    public Transform GetGiveToHeroListTransform() { return giveToHeroListTransform; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        parentCanvas = transform.parent.GetComponent<Canvas>();
    }

    void Update()
    {
        if (contextMenuOpen) CloseContextMenuOnCommand();

        if (contextMenuOpen) KeepFullyOnScreen();
    }

    /// <summary>
    /// If the user clicks somewhere off of the context menu, it should be closed.  This function is ran in Update() to check every frame for the user clicking off the context menu.
    /// </summary>
    void CloseContextMenuOnCommand()
    {
        if (!blockClose && Input.GetMouseButtonDown(0))
        {
            CloseContextMenu();
        }

        if (!blockCloseOnHeroList && Input.GetMouseButton(0))
        {
            CloseGiveToHeroList();
        }
    }

    /// <summary>
    /// Opens up the context menu in the UI using the given item as the item that was right clicked
    /// </summary>
    /// <param name="item">Item that the context menu should be opened for</param>
    public void OpenContextMenu(HeroItem item)
    {
        clickedItem = item;

        Vector3 tryPos = Input.mousePosition / parentCanvas.transform.localScale.x;
        //Debug.Log("Should open menu at " + tryPos);
        transform.position = tryPos;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        contextMenuOpen = true;
    }

    /// <summary>
    /// Closes the context menu in the UI
    /// </summary>
    public void CloseContextMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        contextMenuOpen = false;
    }

    /// <summary>
    /// When the user clicks "GiveToHeroButton", this is called to generate the list of available heroes to give to
    /// </summary>
    public void PrepGiveToHeroList()
    {
        foreach (HeroManager heroManager in PartyManager.i.GetInactiveHeroes()) // This should only be used at home. this will need to be different for being out in the field.
        {
            // make sure the heroManager you're checking isn't the one you're giving from (you dont want to show the current hero from heroManager instantiated)
            if (heroManager != HomeZoneManager.i.GetHeroManager())
            {
                // generate ContextMenuItemHeroGiveItemToHeroButtons under the GiveToHeroList transform
                GameObject newButton = Instantiate(PrefabManager.i.ContextMenuItemHeroGiveToHeroButton, giveToHeroListTransform);

                // set the nameText, giveToHero, giveFromHero, and item for each
                ContextMenuItemHeroGiveToHeroButtonHandler handler = newButton.GetComponent<ContextMenuItemHeroGiveToHeroButtonHandler>();

                handler.SetNameText(heroManager.Hero().GetName());
                handler.SetGiveFromHeroManager(HomeZoneManager.i.GetHeroManager());
                handler.SetGiveToHeroManager(heroManager);

                handler.SetItemToSwap(clickedItem);
            }
        }
    }

    /// <summary>
    /// Opens the 'GiveToHeroList' in the UI for the user to interact with
    /// </summary>
    public void OpenGiveToHeroList()
    {
        giveToHeroListTransform.GetComponent<CanvasGroup>().alpha = 1;
        giveToHeroListTransform.GetComponent<CanvasGroup>().interactable = true;
        giveToHeroListTransform.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// Closes the 'GiveToHeroList' in the UI
    /// </summary>
    public void CloseGiveToHeroList()
    {
        ClearGiveToHeroList();

        giveToHeroListTransform.GetComponent<CanvasGroup>().alpha = 0;
        giveToHeroListTransform.GetComponent<CanvasGroup>().interactable = false;
        giveToHeroListTransform.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// Destroys all objects instantiated in the giveToHeroList
    /// </summary>
    void ClearGiveToHeroList()
    {
        foreach (Transform transform in giveToHeroListTransform)
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Keeps the context menu available on the screen so that it is always fully viewable within the user's viewable bounds
    /// </summary>
    void KeepFullyOnScreen()
    {
        RectTransform canvas = transform.parent.GetComponent<RectTransform>();
        RectTransform rect = GetComponent<RectTransform>();

        Vector2 anchorOffset = canvas.sizeDelta * (rect.anchorMin - Vector2.one / 2);

        Vector2 maxPivotOffset = rect.sizeDelta * (rect.pivot - (Vector2.one / 2) * 2);
        Vector2 minPivotOffset = rect.sizeDelta * ((Vector2.one / 2) * 2 - rect.pivot);

        Vector2 position = rect.anchoredPosition;

        float minX = (canvas.sizeDelta.x) * -0.5f - anchorOffset.x - minPivotOffset.x + rect.sizeDelta.x;
        float maxX = (canvas.sizeDelta.x) * 0.5f - anchorOffset.x + maxPivotOffset.x;
        float minY = (canvas.sizeDelta.y) * -0.5f - anchorOffset.y - minPivotOffset.y + rect.sizeDelta.y;
        float maxY = (canvas.sizeDelta.y) * 0.5f - anchorOffset.y + maxPivotOffset.y;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        rect.anchoredPosition = position;
    }
}

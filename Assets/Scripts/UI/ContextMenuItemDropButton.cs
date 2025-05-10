using UnityEngine;
using UnityEngine.EventSystems;

// Purpose: Facilitates action taken when the user clicks the "Drop" button when right clicking on an item in the inventory UI.
// Directions: Attach to the button "[UI]/HeroInventoryCanvas/InventoryContextMenu/DropButton" Button Component
// Other notes: Not yet implemented.

public class ContextMenuItemDropButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ContextMenuHandler contextMenuHandler;

    void Awake()
    {
        contextMenuHandler = transform.parent.GetComponent<ContextMenuHandler>();
    }

    /// <summary>
    /// Called when the user's mouse cursor enters the menu button's bounds.  Just disables closing the context menu when left clicking it.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(true);
    }

    /// <summary>
    /// Called when the user's mouse cursor exits the menu button's bounds.  It allows the user to close the context menu when left clicking outside of it
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(false);
    }

    /// <summary>
    /// Not yet implemented
    /// </summary>
    public void OnClick()
    {
        
    }
}

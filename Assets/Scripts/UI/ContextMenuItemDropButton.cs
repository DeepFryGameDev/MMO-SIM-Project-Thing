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

    public void OnPointerEnter(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        contextMenuHandler.SetBlockClose(false);
    }

    public void OnClick()
    {
        
    }
}

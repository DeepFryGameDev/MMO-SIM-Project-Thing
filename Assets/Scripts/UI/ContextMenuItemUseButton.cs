using UnityEngine;
using UnityEngine.EventSystems;

// Purpose: Facilitates action taken when the user clicks the "Use" button when right clicking on an item in the inventory UI.
// Directions: Attach to the button "[UI]/HeroInventoryCanvas/InventoryContextMenu/UseButton" Button Component
// Other notes: Not yet implemented.

public class ContextMenuItemUseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

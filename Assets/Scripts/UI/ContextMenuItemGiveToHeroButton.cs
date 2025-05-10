using UnityEngine;
using UnityEngine.EventSystems;

// Purpose: Facilitates action taken when the user clicks the "Give To" button when right clicking on an item in the inventory UI.
// Directions: Attach to the button "[UI]/HeroInventoryCanvas/InventoryContextMenu/GiveToButton" Button Component
// Other notes: Somewhat working.  The hero list needs work since it will run off the screen.  But you can swap items between heroes.

public class ContextMenuItemGiveToHeroButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    /// When the user clicks the 'Give To' button, the Hero List should be set up and displayed with the available heros to transfer the item to.
    /// </summary>
    public void OnClick()
    {
        Vector3 tryPos = Input.mousePosition / contextMenuHandler.GetParentCanvas().transform.localScale.x;
        // Debug.Log("Should open menu at " + tryPos);
        contextMenuHandler.GetGiveToHeroListTransform().position = tryPos;

        contextMenuHandler.PrepGiveToHeroList();

        // then display the panel
        contextMenuHandler.OpenGiveToHeroList();
    }
}

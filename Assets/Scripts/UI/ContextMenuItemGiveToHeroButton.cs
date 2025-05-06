using UnityEngine;
using UnityEngine.EventSystems;

// Purpose: Facilitates action taken when the user clicks the "Drop" button when right clicking on an item in the inventory UI.
// Directions: Attach to the button "[UI]/HeroInventoryCanvas/InventoryContextMenu/DropButton" Button Component
// Other notes: Somewhat working.  The hero list needs work since it will run off the screen.  But you can swap items between heroes.

public class ContextMenuItemGiveToHeroButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        Vector3 tryPos = Input.mousePosition / contextMenuHandler.GetParentCanvas().transform.localScale.x;
        // Debug.Log("Should open menu at " + tryPos);
        contextMenuHandler.GetGiveToHeroListTransform().position = tryPos;

        contextMenuHandler.PrepGiveToHeroList();

        // then display the panel
        contextMenuHandler.OpenGiveToHeroList();
    }
}

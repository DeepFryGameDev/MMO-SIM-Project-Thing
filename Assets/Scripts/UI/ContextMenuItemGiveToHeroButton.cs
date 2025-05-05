using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

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

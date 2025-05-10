using UnityEngine;

// Purpose: Is primarily used to keep the menu on the screen but it doesn't really work.
// Directions: Attach to "[UI]/HeroInventoryCanvas/InventoryContextMenu/GiveToHeroList" object
// Other notes:

public class HeroInventoryContextMenuGiveToHeroListHandler : MonoBehaviour
{
    [SerializeField] ContextMenuHandler contextMenuHandler;

    // Update is called once per frame
    void Update()
    {
        if (contextMenuHandler.GetContextMenuOpen())
        {
            KeepFullyOnScreen();
        }       
    }

    /// <summary>
    /// Is supposed to keep the object from appearing outside of the screen's bounds, but it doesn't work right now.
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

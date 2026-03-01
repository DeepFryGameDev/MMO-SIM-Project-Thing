using UnityEngine;
using UnityEngine.EventSystems;

public class BattleHeroActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    BattleUIHandler handler;

    private void Awake()
    {
        handler = FindFirstObjectByType<BattleUIHandler>();
    }
    public void OnAttackClick()
    {
        handler.ToggleEnemyListPanel(true);
    }

    // Called when the mouse pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Nothing yet
    }

    // Called when the mouse pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Nothing yet
    }

}

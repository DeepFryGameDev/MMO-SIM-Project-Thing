using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyListButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // should just store which enemy belongs to this button
    BaseEnemy enemy;
    public void SetEnemy(BaseEnemy enemy) { this.enemy = enemy; }
    public BaseEnemy GetEnemy() { return enemy; }

    public void OnClick()
    {
        BaseAction basicAttack = new BaseAction(BattleManager.i.GetHeroTurnQueue().Peek().Hero(), enemy, EnumHandler.battleActionTypes.BASICATTACK);
        BattleManager.i.AddToActionQueue(basicAttack);

        // Hide both the heroActionPanel and enemyListPanel with handler
        BattleUIHandler.i.ToggleHeroActionPanel(false);
        BattleUIHandler.i.ToggleEnemyListPanel(false);

        // Dequeue the turn
        BattleManager.i.HeroTurnDeQueue();

        // Turn hero turn off
        BattleManager.i.SetHeroTurnActive(false);
    }



    // Called when the mouse pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        enemy.GetArrowBehavior().ToggleVisibility(true);
    }

    // Called when the mouse pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        enemy.GetArrowBehavior().ToggleVisibility(false);
    }
}

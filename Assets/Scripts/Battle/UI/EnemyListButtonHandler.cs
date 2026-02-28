using UnityEngine;

public class EnemyListButtonHandler : MonoBehaviour
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
}

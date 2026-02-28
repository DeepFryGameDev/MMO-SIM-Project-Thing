using UnityEngine;

public class BattleHeroActionButton : MonoBehaviour
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
}

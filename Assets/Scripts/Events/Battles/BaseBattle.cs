using System.Collections.Generic;
using UnityEngine;

public class BaseBattle : BaseInteractable
{
    [SerializeField] protected List<BaseEnemy> enemies;
    public List<BaseEnemy> GetEnemies() { return enemies; }

    [SerializeField] int battleScene;

    public override void OnInteract()
    {
        base.OnInteract();

        BattleData.i.SetBaseBattle(this);

        DebugManager.i.BattleDebugOut("Test Battle", "Test battle initated!");
        StartCoroutine(bse.BattleTransition(battleScene));
    }
}

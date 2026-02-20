using System.Collections.Generic;
using UnityEngine;

public class BaseBattle : BaseInteractable
{
    [SerializeField] protected List<EnemyScriptableObject> enemies;
    public List<EnemyScriptableObject> GetEnemies() { return enemies; }

    [SerializeField] int battleScene;

    public override void OnInteract()
    {
        base.OnInteract();

        BattleData.i.SetBaseBattle(this);

        DebugManager.i.BattleDebugOut("Test Battle", "Test battle initated!");
        StartCoroutine(bse.BattleTransition(battleScene));
    }
}

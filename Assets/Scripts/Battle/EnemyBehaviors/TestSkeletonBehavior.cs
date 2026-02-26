using UnityEngine;

public class TestSkeletonBehavior : BattleEnemyProcessing
{
    // Here is where we will give the AI to the test skeleton.

    protected override void Update()
    {
        base.Update();

        if (turnReady)
        {
            EnqueueBasicAttack();
        }
    }
}

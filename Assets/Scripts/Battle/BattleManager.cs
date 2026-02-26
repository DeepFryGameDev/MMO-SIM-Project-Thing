using System;
using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public static BattleManager i;

    Queue<BaseAttackableUnit> unitTurnQueue = new Queue<BaseAttackableUnit>();
    public void AddToUnitTurnQueue(BaseAttackableUnit unit) { unitTurnQueue.Enqueue(unit); }
    void UnitTurnDeQueue() { unitTurnQueue.Dequeue(); }
    public Queue<BaseAttackableUnit> GetUnitTurnQueue() { return unitTurnQueue; }

    Queue<BaseAction> actionQueue = new Queue<BaseAction>();
    public void AddToActionQueue(BaseAction action) { actionQueue.Enqueue(action); }
    void ActionTurnDeQueue() { actionQueue.Dequeue(); }
    public Queue<BaseAction> GetActionQueue() { return actionQueue; }

    private void Awake()
    {
        i = this;
    }

    private void Update()
    {
        if ( actionQueue.Count > 0)
        {
            if (actionQueue.Peek().GetSourceUnit() is BaseEnemy) // Enemy action is executed immediately
            {
                DebugManager.i.BattleDebugOut("BattleManager", "Processing action " + actionQueue.Peek().GetActionType().ToString() +
                " / Source: " + actionQueue.Peek().GetSourceUnit().GetName() + " / Target: " + actionQueue.Peek().GetTargetUnit().GetName());
                // run enemy action here

                ActionTurnDeQueue();
                UnitTurnDeQueue();
            } else // Hero action is executed after player input
            {

            }            
        }
    }
}

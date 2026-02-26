using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

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

    bool actionStarted, actionFinished = false;

    private void Awake()
    {
        i = this;
    }

    private void Update()
    {
        if ( actionQueue.Count > 0 && !actionStarted)
        {
            if (actionQueue.Peek().GetSourceUnit() is BaseEnemy) // Enemy action is executed immediately
            {
                DebugManager.i.BattleDebugOut("BattleManager", "Processing action " + actionQueue.Peek().GetActionType().ToString() +
                " / Source: " + actionQueue.Peek().GetSourceUnit().GetName() + " / Target: " + actionQueue.Peek().GetTargetUnit().GetName());
                // run enemy action here
                StartCoroutine(ProcessAction());
                
            } else // Hero action is executed after player input
            {

            }            
        }
    }

    IEnumerator ProcessAction()
    {
        actionStarted = true;
        actionFinished = false;

        // halt any other actions from taking place until this action is fully processed
        while (!actionFinished)
        {
            // source unit should run to target unit
            // play attack animation, etc. here. for now just simulating with a wait
            // run back to their original position after the attack is done, etc.

            Debug.Log("~~Simulating action process~~");
            yield return new WaitForSeconds(3.0f); // placeholder for action processing time
            Debug.Log("Just setting actionFinished to true for now");
            actionFinished = true;
        }

        // start unit's ATB over again
        if (actionQueue.Peek().GetSourceUnit() is BaseEnemy)
        {
            BattleEnemyProcessing enemyProc = actionQueue.Peek().GetSourceUnit().GetComponent<BattleEnemyProcessing>();
            if (enemyProc != null) enemyProc.ResetATB();
        }

        ActionTurnDeQueue();   

        actionStarted = false;        
    }
}

using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class BattleManager : MonoBehaviour
{
    public static BattleManager i;

    Queue<HeroManager> heroTurnQueue = new Queue<HeroManager>();
    public void AddToHeroTurnQueue(HeroManager hero) { heroTurnQueue.Enqueue(hero); }
    public void HeroTurnDeQueue() { heroTurnQueue.Dequeue(); }
    public Queue<HeroManager> GetHeroTurnQueue() { return heroTurnQueue; }

    Queue<BaseAction> actionQueue = new Queue<BaseAction>();
    public void AddToActionQueue(BaseAction action) { actionQueue.Enqueue(action); }
    void ActionTurnDeQueue() { actionQueue.Dequeue(); }
    public Queue<BaseAction> GetActionQueue() { return actionQueue; }

    bool actionStarted, actionFinished = false;
    bool heroTurnActive = false;
    public void SetHeroTurnActive(bool active) { heroTurnActive = active; }

    List<BaseEnemy> enemyList = new List<BaseEnemy>();
    public void AddToEnemyList(BaseEnemy enemy) { enemyList.Add(enemy); }
    public List<BaseEnemy> GetEnemyList() { return enemyList; }

    private void Awake()
    {
        i = this;
    }

    private void Update()
    {
        if ( actionQueue.Count > 0 && !actionStarted)
        {
            DebugManager.i.BattleDebugOut("BattleManager", "Processing action " + actionQueue.Peek().GetActionType().ToString() +
                " / Source: " + actionQueue.Peek().GetSourceUnit().GetName() + " / Target: " + actionQueue.Peek().GetTargetUnit().GetName());
            // run action here
            StartCoroutine(ProcessAction());
        }

        if (heroTurnQueue.Count > 0 && !heroTurnActive)
        {
            // Display BattleActionPanel for the hero at heroTurnQueue.Peek()

            // while this is being displayed, we should wait to check the next in the queue
            heroTurnActive = true;
            
            if (BattleUIHandler.i == null) BattleUIHandler.i = FindFirstObjectByType<BattleUIHandler>();

            BattleUIHandler.i.SetHeroFacePanel(heroTurnQueue.Peek().GetFaceImage());
            BattleUIHandler.i.ToggleHeroActionPanel(true);
        }
    }

    IEnumerator ProcessAction()
    {
        actionStarted = true;
        actionFinished = false;

        // halt any other actions from taking place until this action is fully processed
        while (!actionFinished)
        {
            // switch for type of action
            switch (actionQueue.Peek().GetActionType())
            {
                case EnumHandler.battleActionTypes.BASICATTACK:
                    //Debug.Log("Processing basic attack action");

                    StartCoroutine(ProcessBasicAttack());

                    while (!actionFinished)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    break;
                case EnumHandler.battleActionTypes.SPECIALATTACK:
                    Debug.Log("Processing special attack action");
                    break;
                case EnumHandler.battleActionTypes.MAGIC:
                    Debug.Log("Processing magic action");
                    break;
                case EnumHandler.battleActionTypes.ITEM:
                    Debug.Log("Processing item action");
                    break;
            }          
        }

        // start unit's ATB over again
        if (actionQueue.Peek().GetSourceUnit() is BaseEnemy)
        {
            BattleEnemyProcessing enemyProc = actionQueue.Peek().GetSourceUnit().GetComponent<BattleEnemyProcessing>();
            if (enemyProc != null) enemyProc.ResetATB();
        } else
        {
            BaseHero hero = actionQueue.Peek().GetSourceUnit() as BaseHero;

            hero.GetComponent<HeroManager>().BattleHeroProcessing().ResetATB();
        }

            ActionTurnDeQueue();   

        actionStarted = false;        
    }

    IEnumerator ProcessBasicAttack()
    {
        BaseAction action = actionQueue.Peek();

        BattleUnitMovement unitMovement = action.GetSourceUnit().GetComponent<BattleUnitMovement>();
        Animator animator = action.GetSourceUnit().GetComponent<Animator>();

        bool isEnemy = action.GetSourceUnit() is BaseEnemy;

        // PART 1 - SOURCE UNIT RUNS TO TARGET UNIT
        unitMovement.RunToTarget(action.GetTargetUnit());

        if (isEnemy) // Enemy anim handler
        {
            action.GetSourceUnit().GetComponent<EnemyAnimHandler>().SetAnimationState(EnumHandler.enemyBattleAnimationStates.RUNTOPOINT);
        } else // Hero anim handler
        {
            action.GetSourceUnit().GetComponent<HeroAnimHandler>().SetBattleAnimationState(EnumHandler.heroBattleAnimationStates.RUNTOPOINT);
        }

            yield return new WaitUntil(() => unitMovement.GetIsRunningToTarget() == false);

        // -----------------------------------------

        // PART 2 - DO THE ATTACK

        //Debug.Log("Pre attack");
        // Small pause
        if (isEnemy)  // Enemy anim handler
        {
            action.GetSourceUnit().GetComponent<EnemyAnimHandler>().SetAnimationState(EnumHandler.enemyBattleAnimationStates.IDLE);
        } else // Hero anim handler
        {
            action.GetSourceUnit().GetComponent<HeroAnimHandler>().SetBattleAnimationState(EnumHandler.heroBattleAnimationStates.IDLE);
        }
            yield return new WaitForSeconds(BattleSettings.preAttackAnimWaitTime);

        //Debug.Log("Playing attack animation!");

        if (isEnemy)  // Enemy anim handler
        {
            action.GetSourceUnit().GetComponent<EnemyAnimHandler>().AttackAnim();
        } else
        {
            action.GetSourceUnit().GetComponent<HeroAnimHandler>().AttackAnim();
        }

        yield return null;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length); // Wait until animation is finished


        // PART 3 - DISPLAY DAMAGE 
        //calculate damage here and put it in the damagepopup below.
        DamagePopupHandler.Create(action.GetTargetUnit().transform.position, 000);

        // update hero/enemy values

        // Small pause
        if (isEnemy)  // Enemy anim handler
        {
            action.GetSourceUnit().GetComponent<EnemyAnimHandler>().SetAnimationState(EnumHandler.enemyBattleAnimationStates.IDLE);
        }
        else
        {
            action.GetSourceUnit().GetComponent<HeroAnimHandler>().SetBattleAnimationState(EnumHandler.heroBattleAnimationStates.IDLE);
        }
        yield return new WaitForSeconds(BattleSettings.postAttackAnimWaitTime);

        // -----------------------------------------

        // PART 4 - RUN BACK TO ORIGINAL POSITION AND FACE OPPONENT AGAIN
        if (isEnemy)   // Enemy anim handler
        {
            action.GetSourceUnit().GetComponent<EnemyAnimHandler>().SetAnimationState(EnumHandler.enemyBattleAnimationStates.RUNTOPOINT);
        }
        else // Hero anim handler
        {
            action.GetSourceUnit().GetComponent<HeroAnimHandler>().SetBattleAnimationState(EnumHandler.heroBattleAnimationStates.RUNTOPOINT);
        }

        unitMovement.RunToOrigin();
        yield return new WaitUntil(() => unitMovement.GetIsRunningToOrigin() == false);

        // ------------------------------------------

        // -- PART 5 - POST ATTACK STUFF

        //Debug.Log("In post attack");

        // Set back to idle animation
        if (isEnemy)  // Enemy anim handler
        {
            action.GetSourceUnit().GetComponent<EnemyAnimHandler>().SetAnimationState(EnumHandler.enemyBattleAnimationStates.IDLE);
        }
        else
        {
            action.GetSourceUnit().GetComponent<HeroAnimHandler>().SetBattleAnimationState(EnumHandler.heroBattleAnimationStates.IDLE);
        }

        // Update UI

        // -----------------------------------------

        // when all done, set actionFinished to true so that the BattleManager can move on to the next action in the queue
        actionFinished = true;
    }
}

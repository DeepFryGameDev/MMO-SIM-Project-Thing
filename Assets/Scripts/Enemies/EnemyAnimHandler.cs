using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    EnumHandler.enemyBattleAnimationStates currentAnimState;
    public void SetAnimationState(EnumHandler.enemyBattleAnimationStates animState) { currentAnimState = animState; }
    EnumHandler.enemyBattleStates currentBattleState;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAnimState)
        {
             case EnumHandler.enemyBattleAnimationStates.IDLE:
                anim.SetBool("battleIdle", true);
                anim.SetBool("battleRun", false);
                break;
            case EnumHandler.enemyBattleAnimationStates.RUNTOPOINT:
                anim.SetBool("battleIdle", false);
                anim.SetBool("battleRun", true);
                break;
            case EnumHandler.enemyBattleAnimationStates.GETHIT:
                // Play hurt animation
                break;
            case EnumHandler.enemyBattleAnimationStates.DEATH:
                // Play death animation
                break;
        }
    }

    public void AttackAnim()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("BattleIdle"))
        {
            anim.SetTrigger("battleAttack");
        }
    }
}

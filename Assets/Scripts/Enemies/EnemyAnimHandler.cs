using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    public EnumHandler.enemyBattleAnimationStates currentAnimState;
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
                break;
            case EnumHandler.enemyBattleAnimationStates.ATTACK:
                // Play attack animation
                break;
            case EnumHandler.enemyBattleAnimationStates.GETHIT:
                // Play hurt animation
                break;
            case EnumHandler.enemyBattleAnimationStates.DEATH:
                // Play death animation
                break;
        }
    }
}

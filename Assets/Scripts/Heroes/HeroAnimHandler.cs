using UnityEngine;

// Purpose: Handles all animation processing for heroes
// Directions: Attach to the hero object
// Other notes: 

public class HeroAnimHandler : MonoBehaviour
{
    HeroManager heroManager; // HeroManager attached to the hero

    Animator animator; // Animator to manipulate for animations

    EnumHandler.heroAnimationStates currentAnimationState;
    public EnumHandler.heroAnimationStates GetAnimationState() { return currentAnimationState; }
    public void SetAnimationState(EnumHandler.heroAnimationStates newState) { currentAnimationState = newState; }

    EnumHandler.heroBattleAnimationStates currentBattleAnimationState;
    public EnumHandler.heroBattleAnimationStates GetBattleAnimationState() { return currentBattleAnimationState; }
    public void SetBattleAnimationState(EnumHandler.heroBattleAnimationStates newState) { currentBattleAnimationState = newState; }

    void Start() // For scripts attached to heroes, set vars in Start so that HeroManager vars are set in Awake first.
    {
        heroManager = transform.GetComponent<HeroManager>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentAnimationState)
        {
            case EnumHandler.heroAnimationStates.FREEMOVE:
                MovementAnims();
                break;
            case EnumHandler.heroAnimationStates.BATTLE:
                BattleAnims();
                break;
        }        
    }

    /// <summary>
    /// Simply handles how movement animations should work with the Animator
    /// This should be reworked eventually - right now this is gathering the distance var every frame for every hero.  Too much overhead.
    /// </summary>
    void MovementAnims()
    {
        float distance = Vector3.Distance(heroManager.HeroPathing().GetAgent().destination, transform.position);

        switch (heroManager.HeroPathing().GetRunMode())
        {
            case EnumHandler.pathRunMode.WALK:
                if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Walking / Walking Anim");

                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);

                break;
            case EnumHandler.pathRunMode.CANRUN:
                if (distance > HeroSettings.stoppingDistance) // Hero is en route to target
                {
                    if (distance > HeroSettings.walkToTargetDistance) // should be running
                    {
                        if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Running / Running Anim");

                        animator.SetBool("isWalking", false);
                        animator.SetBool("isRunning", true);
                    }
                    else // should be walking 
                    {
                        if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Walking / Walking Anim");

                        animator.SetBool("isWalking", true);
                        animator.SetBool("isRunning", false);
                    }
                }
                break;
            case EnumHandler.pathRunMode.CATCHUP:
                if (distance > HeroSettings.runToCatchupDistance) // should be running
                {
                    if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Running / Running Anim");

                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", true);
                }
                else // should be walking 
                {
                    if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Walking / Walking Anim");

                    animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false);
                }
                break;
        }
        

        

        if (distance <= HeroSettings.stoppingDistance && animator.GetBool("isWalking")) // Hero has stopped moving
        {
            if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Stopped / Idle Anim");
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    public void SetMovementToIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    void BattleAnims()
    {
        // need to set a default state to idle battle stance
        switch (currentBattleAnimationState)
        {
            case EnumHandler.heroBattleAnimationStates.NOTINCOMBAT:
                // do nothing
                break;
            case EnumHandler.heroBattleAnimationStates.IDLE:
                // show battle idle stance
                animator.SetBool("battleIdle", true);
                animator.SetBool("battleRun", false);
                break;
            case EnumHandler.heroBattleAnimationStates.RUNTOPOINT:
                animator.SetBool("battleIdle", false);
                animator.SetBool("battleRun", true);
                break;
            case EnumHandler.heroBattleAnimationStates.ATTACK:

                break;
            case EnumHandler.heroBattleAnimationStates.BLOCK:

                break;
            case EnumHandler.heroBattleAnimationStates.MAGIC:

                break;
        }
    }

    public void AttackAnim()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("BattleIdle"))
        {
            //Debug.Log("Triggering battleAttack");
            animator.SetTrigger("battleAttack");
        }
    }

    public void GetHitAnim()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("BattleIdle"))
        {
            //Debug.Log("Triggering battleGetHit");
            animator.SetTrigger("battleGetHit");
        }
    }
}

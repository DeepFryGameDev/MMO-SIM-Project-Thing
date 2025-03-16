using UnityEngine;

// Purpose: Handles all animation processing for heroes
// Directions: Attach to the hero object
// Other notes: 

public class HeroAnimHandler : MonoBehaviour
{
    HeroManager heroManager; // HeroManager attached to the hero

    Animator animator; // Animator to manipulate for animations
    
    void Start() // For scripts attached to heroes, set vars in Start so that HeroManager vars are set in Awake first.
    {
        heroManager = transform.GetComponent<HeroManager>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovementAnims();
    }

    /// <summary>
    /// Simply handles how movement animations should work with the Animator
    /// This should be reworked eventually - right now this is gathering the distance var every frame for every hero.  Too much overhead.
    /// </summary>
    void MovementAnims()
    {
        float distance = Vector3.Distance(heroManager.HeroPathing().GetAgent().destination, transform.position);

        if (distance > HeroSettings.stoppingDistance) // Hero is en route to target
        {
            if (distance > HeroSettings.walkToTargetDistance && heroManager.HeroPathing().GetShouldRun()) // should be running
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

        if (distance <= HeroSettings.stoppingDistance && animator.GetBool("isWalking")) // Hero has stopped moving
        {
            if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Stopped / Idle Anim");
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}

using UnityEngine;

// Purpose: Used to transition back to home for easy combat testing
// Directions: Make a copy of the original script (TransitionToDebugField/Home) and attach it to the GameObject trigger this on touch.
// ----------- Set the SceneIndex and intended destination position via inspector.
// ----------- Ensure the GameObject that contains the trigger for this has a collider with 'isTrigger' marked true.  Best used with a box collider on the floor.
// Other notes: 

public class TransitionToDebugHomeFromCombatRange : SceneTransitionEvent
{
    [SerializeField] TransitionToDebugCombatRange transitionToCombatRange;

    public bool isHome = true;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTransition && !isHome && transitionToCombatRange.isTesting)
        {
            isHome = true;
            transitionToCombatRange.isTesting = false;

            base.OnTriggerEnter(other);

            // anything that should change to be read as HOME should be here.
            DebugManager.i.SystemDebugOut("DebugTransition", "Starting home preparation.");

            SceneInfo.i.SetSceneMode(EnumHandler.SceneMode.HOME);
        }         
    }

    /*void MoveToDebugField()
    {
        //StartCoroutine(scriptedEvent.TransitionToScene(1, new Vector3(32, 99.61497f, 200)));        
    }*/
}

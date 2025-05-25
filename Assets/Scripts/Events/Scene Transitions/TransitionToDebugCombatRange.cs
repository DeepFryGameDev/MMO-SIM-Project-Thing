using UnityEngine;

// Purpose: Used to transition to the combat range for easy combat testing
// Directions: Make a copy of the original script (TransitionToDebugField/Home) and attach it to the GameObject trigger this on touch.
// ----------- Set the SceneIndex and intended destination position via inspector.
// ----------- Ensure the GameObject that contains the trigger for this has a collider with 'isTrigger' marked true.  Best used with a box collider on the floor.
// Other notes: 

public class TransitionToDebugCombatRange : SceneTransitionEvent
{
    [SerializeField] TransitionToDebugHomeFromCombatRange transitionToHome;

    public bool isTesting = false;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTransition && !isTesting && transitionToHome.isHome)
        {
            isTesting = true;
            transitionToHome.isHome = false;

            base.OnTriggerEnter(other);

            // anything that should change to be read as FIELD should be here.
            DebugManager.i.SystemDebugOut("DebugTransition", "Starting combat test preparation.");

            SceneInfo.i.SetSceneMode(EnumHandler.SceneMode.FIELD);
        }         
    }

    /*void MoveToDebugField()
    {
        //StartCoroutine(scriptedEvent.TransitionToScene(1, new Vector3(32, 99.61497f, 200)));        
    }*/
}

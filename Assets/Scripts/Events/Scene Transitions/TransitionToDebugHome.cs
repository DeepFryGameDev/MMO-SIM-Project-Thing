using UnityEngine;

// Purpose: Use this to transition to another scene in the game
// Directions: Make a copy of the original script (TransitionToDebugField/Home) and attach it to the GameObject trigger this on touch.
// ----------- Set the SceneIndex and intended destination position via inspector.
// ----------- Ensure the GameObject that contains the trigger for this has a collider with 'isTrigger' marked true.  Best used with a box collider on the floor.
// Other notes: 

public class TransitionToDebugHome : SceneTransitionEvent
{
    bool running;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!running)
            {
                running = true;
                base.OnTriggerEnter(other);

                StartCoroutine(scriptedEvent.TransitionToScene(sceneIndex, spawnPosition));
            }
        }
    }
}

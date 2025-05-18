using UnityEngine;

// Purpose: Used as the derived class for Scene Transitions.  All scene transition events should contain these values.
// Directions: Simply make a copy of TransitionToDebugField or TransitionToDebugHome and set the values.
// Other notes: 

public class SceneTransitionEvent : BaseInteractOnTouch
{
    [Tooltip("The scene index that should be loaded.")]
    [SerializeField] protected int sceneIndex;

    [Tooltip("The position in the new scene that the player should spawn at.")]
    [SerializeField] protected Vector3 spawnPosition;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (SceneInfo.i.GetSceneMode() == EnumHandler.SceneMode.HOME) DateManager.i.StopNewWeekToast();
    }
}

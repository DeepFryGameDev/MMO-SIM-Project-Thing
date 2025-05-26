using System;
using UnityEngine;

// Purpose: Used as the derived class for Scene Transitions.  All scene transition events should contain these values.
// Directions: Simply make a copy of TransitionToDebugField or TransitionToDebugHome and set the values.
// Other notes: 

public class SceneTransitionEvent : BaseInteractOnTouch
{
    BoxCollider transitionCollider;

    Vector3 transitionBlockedColliderCenter = new Vector3(0, 17.5f, 0); // When the transition is blocked, the center of the collider is moved to this position
    Vector3 transitionBlockedColliderSize = new Vector3(1f, 35f, 1f); // When the transition is blocked, the size of the collider is set to this position

    Vector3 transitionUnblockedColliderCenter;
    Vector3 transitionUnblockedColliderSize;

    [Tooltip("The scene index that should be loaded.")]
    [SerializeField] protected int sceneIndex;

    [Tooltip("The position in the new scene that the player should spawn at.")]
    [SerializeField] protected Vector3 spawnPosition;

    [Tooltip("If this transition point requires a party to be formed")]
    [SerializeField] protected bool requiresParty;

    protected bool canTransition;

    private void Awake()
    {
        transitionCollider = GetComponent<BoxCollider>();

        scriptedEvent = gameObject.AddComponent<BaseScriptedEvent>();

        transitionUnblockedColliderCenter = transitionCollider.center;
        transitionUnblockedColliderSize = transitionCollider.size;
    }

    private void Update()
    {
        if (requiresParty)
        {
            if (GameSettings.GetHeroesInParty().Count > 0)
            {
                canTransition = true;
            } else
            {
                canTransition = false;
            }
        } else
        {
            canTransition = true;
        }

        SetColliderSettings();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (canTransition)
        {
            TransitionFunctions();

            base.OnTriggerEnter(other);
        }
    }

    /// <summary>
    /// Used to set the collider depending if the player is able to transition.
    /// If the player is able to transition, the collider is set to 'Trigger' and the default values to match the object it is on.
    /// If the player is unable to transition, the trigger is turned off, and the collider is set to a large size as to block any movement from occurring on it.
    /// </summary>
    void SetColliderSettings()
    {
        if (canTransition)
        {
            // center should be 0.
            transitionCollider.center = transitionUnblockedColliderCenter;

            // size should be 1.
            transitionCollider.size = transitionUnblockedColliderSize;

            // is trigger should be on
            transitionCollider.isTrigger = true;

        } else
        {
            // center should be 17.5.
            transitionCollider.center = transitionBlockedColliderCenter;

            // size should be 35.
            transitionCollider.size = transitionBlockedColliderSize;

            // is trigger off
            transitionCollider.isTrigger = false;
        }
    }

    /// <summary>
    /// Any functions that should occur when the player transitions scenes.
    /// </summary>
    void TransitionFunctions()
    {
        if (SceneInfo.i.GetSceneMode() == EnumHandler.SceneMode.HOME) DateManager.i.StopNewWeekToast();
    }
}

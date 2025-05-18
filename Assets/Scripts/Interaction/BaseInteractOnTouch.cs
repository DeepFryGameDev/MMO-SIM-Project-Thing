using UnityEngine;

// Purpose: Used as the base level script for any events that should be triggered when the player touches an object.
// Directions: Derive any second level event scripts that should interact on touch to this.
// Other notes: 

public class BaseInteractOnTouch : MonoBehaviour
{
    protected static BaseScriptedEvent scriptedEvent;

    private void Awake()
    {
        scriptedEvent = gameObject.AddComponent<BaseScriptedEvent>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has entered the trigger");
    }
}

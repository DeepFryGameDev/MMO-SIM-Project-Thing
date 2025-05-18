using UnityEngine;

// Purpose: 
// Directions: 
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

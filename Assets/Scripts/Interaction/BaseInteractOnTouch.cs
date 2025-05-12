using UnityEngine;

public class BaseInteractOnTouch : MonoBehaviour
{
    protected static BaseScriptedEvent scriptedEvent;

    Collider attachedCollider;

    private void Awake()
    {
        attachedCollider = GetComponent<Collider>();
        scriptedEvent = gameObject.AddComponent<BaseScriptedEvent>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " has entered the trigger");
    }
}

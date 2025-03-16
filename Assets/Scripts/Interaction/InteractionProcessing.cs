using UnityEngine;

// Purpose: Base class for any objects that will be interacted with
// Directions: 
// Other notes: 

public class InteractionProcessing : MonoBehaviour
{

    /// <summary>
    /// Used by all inherited classes to interact with the object within the player's interaction raycast
    /// </summary>
    public virtual void OnInteract()
    {
        Debug.Log("OnInteract in InteractionProcessing");
    }
}

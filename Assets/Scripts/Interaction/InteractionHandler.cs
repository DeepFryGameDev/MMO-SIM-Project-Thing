using UnityEngine;

// Purpose: Used to manage interactions between the player and objects in the world that can utilize scripts to perform events
// Directions: Attach to the [System] GameObject
// Other notes: 

public class InteractionHandler : MonoBehaviour
{
    [Tooltip("Distance from the player to the object before interaction is available")]
    [SerializeField] float interactDistance;
    public float GetInteractDistance() { return  interactDistance; }

    [Tooltip("If true, will display the ray from the player to show where interaction is available")]
    [SerializeField] bool showInteractionRay;
    public bool GetShowInteractionRay() { return showInteractionRay; }

    // Will turn true when an interaction is in range of the player and the player is able to start the interaction process
    bool interactionReady;

    // Object in the world that is being interacted with
    GameObject interactedObject;   
    public GameObject GetInteractedObject() { return interactedObject; }
    public void SetInteractedObject(GameObject gameObject) { interactedObject = gameObject;}

    public CanvasGroup interactKeyCanvasGroup;

    [SerializeField] InputSubscription inputSubscription;


    void Start()
    {
        // SetVars();

        // SetInteractKeyText();
    }

    void Update()
    {
        if (interactionReady && inputSubscription.actionInput)
        {
            Debug.Log("Interacting with " + interactedObject.name);

            BaseInteractable bi = interactedObject.GetComponent<BaseInteractable>();
            if (bi != null)
            {
                bi.OnInteract();
            } else
            {
                switch (interactedObject.tag)
                {
                    case "Hero":
                        interactedObject.GetComponent<HeroInteraction>().OnInteract();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Displays or hides the interaction UI graphic
    /// </summary>
    /// <param name="toggle">True to display interaction UI graphic, False to hide it</param>
    public void ToggleInteraction(bool toggle)
    {
        if (toggle) interactKeyCanvasGroup.alpha = 1; else interactKeyCanvasGroup.alpha = 0;

        interactionReady = toggle;
    }
}

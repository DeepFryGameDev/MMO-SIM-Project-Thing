using UnityEngine;

// Purpose: Contains primary functionality behind connecting GameObjects in the world to interactions with ScriptedEvents
// Directions: Attach to Player -> InteractPoint Transform
// Other notes: 

public class PlayerInteraction : MonoBehaviour
{
    InteractionHandler ih; // Used to display/hide interaction UI graphic, as well as set the interactedObject

    int layerMask = 1 << 8; // Set to layer layerInteractable - this ensures only interactable objects will receive instruction from the raycast

    bool ignoreRay = false; // Used when interactions should not be possible - ie in a menu
    public void SetIgnoreRay(bool ignoreRay) { this.ignoreRay = ignoreRay; }

    void Update()
    {
        if (ih == null)
        {
            //Debug.Log("Setting new IH");
            ih = FindFirstObjectByType<InteractionHandler>();
        }

        CheckForAvailableInteraction();
    }

    /// <summary>
    /// Draws a ray from the player's gameObject in the direction they are facing, with distance being the interactDistance in InteractionHandler
    /// If an interactable gameObject is detected, the interaction UI graphic is displayed and the object hit is set as the interactedObject
    /// If showInteractRay in InteractionHandler is set to true, a ray is drawn in the Unity Editor to show where the interaction ray is
    /// </summary>
    void CheckForAvailableInteraction()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, ih.GetInteractDistance(), layerMask) && !ignoreRay)
        {
            if (ih.GetShowInteractionRay()) Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            // if the target object is a hero, make sure their follow state is IDLE before allowing interaction
            if (hit.transform.gameObject.CompareTag("Hero") && CheckForHeroInteractionAvailable(hit.transform.GetComponent<HeroManager>()))
            {
                ih.ToggleInteraction(true);
                ih.SetInteractedObject(hit.transform.gameObject);
            }            
        }
        else
        {
            if (ih.GetShowInteractionRay()) Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * ih.GetInteractDistance(), Color.white);

            ih.ToggleInteraction(false);

            if (ih.GetInteractedObject() != null)
            {
                ih.SetInteractedObject(null);
            }
        }
    }

    /// <summary>
    /// Checks if the given heroManager is currently outside of their home zone by checking their path mode.  This should be reworked eventually.
    /// </summary>
    /// <param name="heroManager">Hero to check</param>
    /// <returns>True if they are in their home zone and not in party</returns>
    bool CheckForHeroInteractionAvailable(HeroManager heroManager)
    {
        EnumHandler.pathModes pathMode = heroManager.HeroPathing().GetPathMode();

        if (pathMode != EnumHandler.pathModes.PARTYFOLLOW && pathMode != EnumHandler.pathModes.SENDTOSTARTINGPOINT) return true;

        return false;
    }
}

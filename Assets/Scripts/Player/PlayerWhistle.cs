using System.Collections;
using UnityEngine;

// Purpose: Manages procedures relating to the Player Whistle command.
// This command will call the hero of a home zone you are standing on to the player's side and open the command menu.
// Directions: Attach to [Player]
// Other notes:

public class PlayerWhistle : MonoBehaviour
{
    int layerMask = 1 << 10; // Set to layer homeZone - this ensures only objects in the home zone layer will be returned when raycasting
       
    HeroManager heroManagerWhistled; // Is just set to the heroManager that was whistled

    PlayerMovement pm; // Just used to toggle player movement
    HeroCommandProcessing hcp; // Used to allow the player to interface with the hero

    bool canWhistle;
    public void ToggleCanWhistle(bool toggle) { canWhistle = toggle; }

    void Awake()
    {
        pm = FindFirstObjectByType<PlayerMovement>();

        hcp = FindFirstObjectByType<HeroCommandProcessing>();
    }

    void Start()
    {
        canWhistle = true;
    }

    void Update()
    {
        ListenForWhistle();
    }

    /// <summary>
    /// When the player hits the 'whistleKey' keybind, this will fire.
    /// </summary>
    void ListenForWhistle()
    {
        if (Input.GetKeyDown(KeyBindings.whistleKey) && canWhistle && GlobalSettings.GetUIState() == GlobalSettings.UIStates.IDLE)
        {
            // Check if standing on home zone
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector3.down), out hit, 1, layerMask))
            {
                // set heroWhistled
                heroManagerWhistled = hit.transform.gameObject.GetComponent<HeroHomeZone>().heroManager;
                // Debug.Log("Hero manager: " + heroManagerWhistled.Hero().name);

                GlobalSettings.SetUIState(GlobalSettings.UIStates.HEROCOMMAND);

                StartCoroutine(StartCommandFromWhistle());
            }
            else
            {
                DebugManager.i.HeroDebugOut("PlayerWhistle", "Player is not standing on home zone!", true, false);
            }
        }
    }

    /// <summary>
    /// The coroutine in which the hero runs to the player when they are whistled
    /// </summary>
    IEnumerator StartCommandFromWhistle()
    {
        // Reset heroPathing
        heroManagerWhistled.HeroPathing().pathMode = HeroPathing.pathModes.WHISTLE;

        // play some whisle SE eventually here
        DebugManager.i.HeroDebugOut("PlayerWhistle", "Whistling for " + heroManagerWhistled.Hero().GetName(), false, false);

        // stop player movement
        pm.ToggleMovement(false);

        // increase hero pathing run speed
        heroManagerWhistled.HeroPathing().ToggleRun(true);

        // have hero run to player
        heroManagerWhistled.HeroPathing().WhistleMoveToTarget();

        //when hero is within stopping distance of player
        while (!heroManagerWhistled.HeroPathing().GetWhistleTargetWithinRange())
        {
            //Debug.Log("Target not within range");
            yield return new WaitForEndOfFrame();
        }

        heroManagerWhistled.HeroPathing().StopPathing();

        heroManagerWhistled.HeroPathing().ToggleRun(false);

        // open command menu
        hcp.SetHeroManager(heroManagerWhistled);
        hcp.OpenHeroCommand();
    }    
}

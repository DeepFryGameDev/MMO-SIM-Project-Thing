using UnityEngine;

public class HeroCommandProcessing : MonoBehaviour
{
    // Purpose: Handles the procedures in which the command window operates when on a Hero Home Zone
    // Directions: For now, attach to the [UI]/HeroZoneCanvas/HeroCommand object
    // Other notes: 

    CanvasGroup CommandCanvasGroup; // Set to the canvasGroup attached to the HeroCommand UI panel

    HeroPathing heroPathing; // Used to manipulate the pathingState for the given hero
    public void SetHeroPathing(HeroPathing heroPathing) { this.heroPathing = heroPathing; }

    PlayerMovement playerMovement; // Just used to toggle player's movement when command window is opened

    ThirdPersonCam cam; // Used to disable camera rotating when the player's movement is disabled

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        CommandCanvasGroup = GameObject.Find("HeroZoneCanvas/HeroCommand").GetComponent<CanvasGroup>(); //hacky, need to fix this later
        playerMovement = FindFirstObjectByType<PlayerMovement>();

        cam = FindFirstObjectByType<ThirdPersonCam>();
    }

    /// <summary>
    /// Called whenever the player interacts with a hero object using the PlayerInteraction script
    /// </summary>
    public void OnInteract()
    {
        // should eventually make sure player and hero are on a hero zone first.
        OpenHeroCommand();
    }

    /// <summary>
    /// Function called when the hero command window should be opened.  
    /// This includes any methods related to allowing the user to interact with the UI like toggling player movement
    /// </summary>
    public void OpenHeroCommand()
    {
        // Turn off interaction image in UI

        //1. Disable player movement
        playerMovement.ToggleMovement(false);

        //2. Set pathing to idle on heroPathing.
        heroPathing.StopPathing();

        //3. ToggleCommandMenu to open it
        ToggleCommandMenu(true);
    }

    /// <summary>
    /// Function called when the hero command window should be closed.
    /// Like OpenHeroCommand() this should contain any methods related to turning off the UI and allowing player movement again
    /// </summary>
    public void CloseHeroCommand()
    {

    }

    /// <summary>
    /// Simply displays/hides the command menu and allows the player to interact with it
    /// </summary>
    /// <param name="toggle">True to show the command menu, False to hide it</param>
    void ToggleCommandMenu(bool toggle)
    { 
        if (toggle)
        {
            CommandCanvasGroup.alpha = 1;
            CommandCanvasGroup.interactable = true;
            CommandCanvasGroup.blocksRaycasts = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cam.ToggleCameraRotation(false);
        } else
        {
            CommandCanvasGroup.alpha = 0;
            CommandCanvasGroup.interactable = false;
            CommandCanvasGroup.blocksRaycasts = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cam.ToggleCameraRotation(true);
        }

    }
}

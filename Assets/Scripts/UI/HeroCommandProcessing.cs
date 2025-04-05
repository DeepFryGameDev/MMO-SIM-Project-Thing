using UnityEngine;

// Purpose: Handles the procedures in which the command window operates when on a Hero Home Zone
// Directions: For now, attach to the [UI]/HeroZoneCanvas/HeroCommand object
// Other notes: 

public class HeroCommandProcessing : MonoBehaviour
{  
    CanvasGroup CommandCanvasGroup; // Set to the canvasGroup attached to the HeroCommand UI panel

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }

    PlayerMovement playerMovement; // Just used to toggle player's movement when command window is opened

    PlayerInteraction playerInteraction; // Used to hide/show the interaction graphic

    TrainingEquipmentMenu trainingEquipmentMenu;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        CommandCanvasGroup = GameObject.Find("HeroZoneCanvas/HeroCommand").GetComponent<CanvasGroup>(); //hacky, need to fix this later
        playerInteraction = FindFirstObjectByType<PlayerInteraction>(); // and this

        playerMovement = FindFirstObjectByType<PlayerMovement>();

        trainingEquipmentMenu = FindFirstObjectByType<TrainingEquipmentMenu>();
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
        Debug.Log("Opening command window");

        // Turn off interaction image in UI
        playerInteraction.SetIgnoreRay(true);

        //1. Disable player movement
        playerMovement.ToggleMovement(false);

        //2. Set pathing to idle on heroPathing.
        heroManager.HeroPathing().StopPathing();

        //3. ToggleCommandMenu to open it
        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.ROOT);
    }

    /// <summary>
    /// Function called when the hero command window should be closed.  Basically just doing the opposite as OpenHeroCommand() and in reverse order.
    /// Like OpenHeroCommand() this should contain any methods related to turning off the UI and allowing player movement again
    /// </summary>
    public void CloseHeroCommand()
    {
        Debug.Log("Closing command window");

        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.IDLE);

        heroManager.HeroPathing().StartNewRandomPathing(); // should go back to doing whatever they were doing before command menu. like resuming training, etc.

        playerMovement.ToggleMovement(true);

        playerInteraction.SetIgnoreRay(false);

        GlobalSettings.SetUIState(GlobalSettings.UIStates.IDLE);
    }

    public void OpenTrainingEquipmentMenu()
    {
        // draw 'equip' slots for HeroTrainingEquipment.trainingEquipmentSlots

        trainingEquipmentMenu.InstantiateEquipmentSlots(heroManager);

        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.TRAININGEQUIP);
    }

    public void CloseTrainingEquipmentMenu()
    {
        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.ROOT);
    }

    public void OpenTrainingEquipmentList()
    {
        // generate list of training equipment in inventory

        // instantiate them to the list group

        // Show the Training Equipment List menu
    }

    public void CloseTrainingEquipmentList()
    {
        // Just close the training equipment list menu
    }
}

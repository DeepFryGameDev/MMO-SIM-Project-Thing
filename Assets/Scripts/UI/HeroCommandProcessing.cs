using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Handles the procedures in which the command window operates when on a Hero Home Zone or out in the field
// Directions: For now, attach to the [UI]/HeroCommandMenu/HeroCommand object
// Other notes: 

public class HeroCommandProcessing : MonoBehaviour
{  
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; StatusMenuHandler.i.SetHeroManager(heroManager); }

    PlayerMovement playerMovement; // Just used to toggle player's movement when command window is opened

    PlayerInteraction playerInteraction; // Used to hide/show the interaction graphic

    [SerializeField] TrainingEquipmentMenu trainingEquipmentMenu;

    [SerializeField] HeroInventoryUIHandler heroInventoryUIHandler;

    [SerializeField] HeroEquipMenuHandler heroEquipMenuHandler;

    [SerializeField] Transform heroPanelGroupTransform;

    [SerializeField] Animator heroFacePanelAnim;
    [SerializeField] TextMeshProUGUI facePanelNameText;
    [SerializeField] Image facePanelImage;

    public static HeroCommandProcessing i;

    void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        playerInteraction = FindFirstObjectByType<PlayerInteraction>(); // and this

        playerMovement = FindFirstObjectByType<PlayerMovement>();

        i = this;
    }

    /// <summary>
    /// Called whenever the player interacts with a hero object using the PlayerInteraction script
    /// </summary>
    public void OnInteract()
    {
        // should eventually make sure player and hero are on a hero zone first.
        OpenHeroHomeCommand();
    }

    /// <summary>
    /// Function called when the hero command window should be opened.  
    /// This includes any methods related to allowing the user to interact with the UI like toggling player movement
    /// </summary>
    public void OpenHeroHomeCommand()
    {
        // Turn off interaction image in UI
        playerInteraction.SetIgnoreRay(true);

        //1. Disable player movement
        playerMovement.ToggleMovement(false);

        //2. Set pathing to idle on heroPathing.
        heroManager.HeroPathing().StopPathing();

        //3. ToggleCommandMenu to open it
        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.ROOT);

        // Stop show toast
        DateManager.i.StopNewWeekToast();
    }

    /// <summary>
    /// Generates the Hero Field Command Panels needed for the user to interact with the Hero Field Menu.
    /// </summary>
    public void GenerateHeroFieldCommandMenu()
    {
        ClearHeroFieldCommandMenu();

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            if (PrefabManager.i == null) { PrefabManager.i = FindFirstObjectByType<PrefabManager>(); }
            GameObject newObject = Instantiate(PrefabManager.i.HeroCommandFieldMenuPanel, heroPanelGroupTransform);

            HeroCommandFieldMenuHandler handler = newObject.GetComponent<HeroCommandFieldMenuHandler>();

            handler.SetValues(heroManager);
        }
    }

    /// <summary>
    /// Just clears the Hero Panel group so it can be filled in again
    /// </summary>
    void ClearHeroFieldCommandMenu()
    {
        foreach (Transform transform in heroPanelGroupTransform)
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Function called when the hero command window should be closed.  Basically just doing the opposite as OpenHeroCommand() and in reverse order.
    /// Like OpenHeroCommand() this should contain any methods related to turning off the UI and allowing player movement again
    /// Assigned to: 
    /// </summary>
    public void CloseHeroCommand()
    {
        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.IDLE);

        heroManager.HeroPathing().StartNewRandomPathing(); // should go back to doing whatever they were doing before command menu. like resuming training, etc.

        playerMovement.ToggleMovement(true);

        playerInteraction.SetIgnoreRay(false);

        UISettings.SetUIState(EnumHandler.UIStates.IDLE);
    }

    /// <summary>
    /// Used to open the Training Equipment menu for the player to equip to a hero
    /// Assigned to: 
    /// </summary>
    public void OpenTrainingEquipmentMenu()
    {
        // draw 'equip' slots for HeroTrainingEquipment.trainingEquipmentSlots
        trainingEquipmentMenu.InstantiateEquipmentSlots(heroManager);

        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.TRAININGEQUIP);
    }

    /// <summary>
    /// Just goes back to the root Hero Command menu
    /// Assigned to: 
    /// </summary>
    public void CloseTrainingEquipmentMenu()
    {
        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.ROOT);
    }

    /// <summary>
    /// Displays the inventory menu
    /// Assigned to: [UI]/HeroZoneCanvas/HeroCommand/Holder/ButtonGroup/InventoryButton.OnClick()
    /// </summary>
    public void OpenInventoryMenu()
    {
        // use HeroInventoryUIHandler to draw the inventory menu objects
        heroInventoryUIHandler.ResetUI();
        heroInventoryUIHandler.ClearUI();

        heroInventoryUIHandler.SetHeroDetailsPanel(heroManager);
        heroInventoryUIHandler.GenerateInventory(heroManager);

        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.INVENTORY);
    }

    /// <summary>
    /// Closes the inventory menu
    /// Assigned to: [UI]/HeroInventoryCanvas/HeroInventoryHolder/CloseButton.OnClick()
    /// </summary>
    public void CloseInventoryMenu()
    {
        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.ROOT);
    }

    /// <summary>
    /// Opens the equip menu
    /// Assigned to: [UI]/HeroZoneCanvas/HeroCommand/Holder/ButtonGroup/EquipButton.OnClick()
    /// </summary>
    public void OpenEquipMenu()
    {
        // set any already equipped items to the EquipButtonsPanel
        heroEquipMenuHandler.GenerateEquippedEquipmentButtons(heroManager);

        StatusMenuHandler.i = FindFirstObjectByType<StatusMenuHandler>();

        StatusMenuHandler.i.SetHeroManager(heroManager);
        StatusMenuHandler.i.SetStatusValues();

        StatusMenuHandler.i.ToggleMenu(true);

        // display HeroEquipCanvas
        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.EQUIP);
    }

    /// <summary>
    /// When the face panel should be displayed, this will set the values on it.
    /// </summary>
    public void SetFacePanelValues()
    {
        facePanelImage.sprite = heroManager.GetFaceImage();
        facePanelNameText.SetText(heroManager.Hero().GetName());
    }

    /// <summary>
    /// Displays/hides the face panel.  This is used to help the user distinguish who the menu is opened for.
    /// </summary>
    /// <param name="toggle">True to show the panel, False to hide it.</param>
    public void ToggleFacePanel(bool toggle)
    {
        if (i.heroFacePanelAnim == null) { i.heroFacePanelAnim = transform.Find("HeroFacePanel").GetComponent<Animator>(); }
        i.heroFacePanelAnim.SetBool("toggleOn", toggle);
    }
}

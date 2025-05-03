using System.Globalization;
using UnityEngine;

// Purpose: Facilitates control of which menu is available to the user at a given time
// Directions: Attach to the [UI] object
// Other notes: 

public class MenuProcessingHandler : MonoBehaviour
{
    public static MenuProcessingHandler i;

    EnumHandler.HeroCommandMenuStates heroCommandMenuState;
    public void SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates menuState) { heroCommandMenuState = menuState; }
    public EnumHandler.HeroCommandMenuStates GetHeroCommandMenuState() { return heroCommandMenuState; }

    EnumHandler.HeroCommandMenuStates tempHeroCommandMenuState; // Used so UI is only updated once.


    EnumHandler.PlayerCommandMenuStates playerCommandMenuState;
    public void SetPlayerCommandMenuState(EnumHandler.PlayerCommandMenuStates menuState) { playerCommandMenuState = menuState; }
    public EnumHandler.PlayerCommandMenuStates GetPlayerCommandMenuState() { return playerCommandMenuState; }

    EnumHandler.PlayerCommandMenuStates tempPlayerCommandMenuState; // Used so UI is only updated once.

    // Schedule System
    [SerializeField] ScheduleMenuHandler scheduleMenuHandler;

    // will need access to every canvas group
    [SerializeField] CanvasGroup heroCommandCanvasGroup;
    [SerializeField] Animator heroCommandAnimator;
    [SerializeField] CanvasGroup trainingEquipmentMenuCanvasGroup;

    [SerializeField] CanvasGroup playerCommandCanvasGroup;
    [SerializeField] CanvasGroup partyMenuCanvasGroup;
    [SerializeField] Animator playerCommandAnimator;

    [SerializeField] CanvasGroup heroInventoryCanvasGroup;

    [SerializeField] CanvasGroup heroEquipCanvasGroup;

    public CanvasGroup GetTrainingEquipmentMenuCanvasGroup() { return trainingEquipmentMenuCanvasGroup; }
    [SerializeField] CanvasGroup trainingEquipmentListCanvasGroup;
    public CanvasGroup GetTrainingEquipmentListCanvasGroup() { return trainingEquipmentListCanvasGroup; }

    [SerializeField] CanvasGroup scheduleCanvasGroup;
    public CanvasGroup GetScheduleCanvasGroup() { return scheduleCanvasGroup; }
    //----

    ThirdPersonCam cam; // Used to disable camera rotating when the player's movement is disabled

    CanvasGroup tempCanvasGroup; // Used as the last CanvasGroup to be interacted with
    void Awake()
    {
        i = this;

        cam = FindFirstObjectByType<ThirdPersonCam>();
    }

    void Start()
    {
        tempHeroCommandMenuState = EnumHandler.HeroCommandMenuStates.IDLE;
    }

    void Update()
    {
        if (tempHeroCommandMenuState != heroCommandMenuState) ProcessHeroCommandMenu();

        if (tempPlayerCommandMenuState != playerCommandMenuState) ProcessPlayerCommandMenu();
    }

    /// <summary>
    /// Opens/closes menus based on the menu state - this gives easy control to outside scripts to open any given menu
    /// </summary>
    void ProcessHeroCommandMenu()
    { 
        switch (heroCommandMenuState)
        {
            case EnumHandler.HeroCommandMenuStates.IDLE: // Hide the hero command menu
                ToggleMenu(heroCommandCanvasGroup, false);

                break;
            case EnumHandler.HeroCommandMenuStates.ROOT: // Display root contents of hero command menu
                if (tempHeroCommandMenuState == EnumHandler.HeroCommandMenuStates.IDLE) // coming from idle (no menu)
                {
                    ToggleMenu(heroCommandCanvasGroup, true);
                    tempCanvasGroup = heroCommandCanvasGroup;
                } else
                {
                    TransitionToMenu(heroCommandCanvasGroup, tempCanvasGroup);
                }
                    break;
            case EnumHandler.HeroCommandMenuStates.INVENTORY: // Display inventory menu for hero
                TransitionToMenu(heroInventoryCanvasGroup, true);
                break;
            case EnumHandler.HeroCommandMenuStates.EQUIP:
                TransitionToMenu(heroEquipCanvasGroup, true);
                break;
            case EnumHandler.HeroCommandMenuStates.TRAININGEQUIP: // Display training equipment menu
                TransitionToMenu(trainingEquipmentMenuCanvasGroup, true);

                break;
            case EnumHandler.HeroCommandMenuStates.TRAININGEQUIPLIST:
                TransitionToMenu(trainingEquipmentListCanvasGroup, false);

                break;

            case EnumHandler.HeroCommandMenuStates.SCHEDULE:
                TransitionToMenu(scheduleCanvasGroup, true);

                break;
        }

        tempHeroCommandMenuState = heroCommandMenuState;
    }

    /// <summary>
    /// Opens/closes menus based on the menu state - this gives easy control to outside scripts to open any given menu
    /// </summary>
    void ProcessPlayerCommandMenu()
    {
        switch (playerCommandMenuState)
        {
            case EnumHandler.PlayerCommandMenuStates.IDLE: // Hide the player command menu
                ToggleMenu(playerCommandCanvasGroup, false);

                break;
            case EnumHandler.PlayerCommandMenuStates.ROOT: // Display root contents of player command menu
                if (tempPlayerCommandMenuState == EnumHandler.PlayerCommandMenuStates.IDLE) // coming from idle (no menu)
                {
                    ToggleMenu(playerCommandCanvasGroup, true);
                    tempCanvasGroup = playerCommandCanvasGroup;
                }
                else
                {
                    TransitionToMenu(playerCommandCanvasGroup, tempCanvasGroup);
                }
                break;
            case EnumHandler.PlayerCommandMenuStates.PARTY:
                TransitionToMenu(partyMenuCanvasGroup, tempCanvasGroup);
                break;
        }

        tempPlayerCommandMenuState = playerCommandMenuState;
    }

    /// <summary>
    /// Opens the given menu
    /// </summary>
    /// <param name="canvasGroup">CanvasGroup of the menu to be opened</param>
    /// <param name="closePrevious">If the previous menu should be closed, set this to true.</param>
    public void TransitionToMenu(CanvasGroup canvasGroup, bool closePrevious)
    {
        
        if (closePrevious && tempCanvasGroup != null)
        {
            // close stuff from tempCanvasGroup
            tempCanvasGroup.GetComponent<Animator>().SetBool("toggleOn", false);

            tempCanvasGroup.interactable = false;
            tempCanvasGroup.blocksRaycasts = false;
        }

        // open new stuff
        canvasGroup.GetComponent<Animator>().SetBool("toggleOn", true);

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Set new tempCanvasGroup
        tempCanvasGroup = canvasGroup;
    }

    /// <summary>
    /// Simply displays/hides the menu and allows the player to interact with it
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="toggle">True to show the command menu, False to hide it</param>
    public void ToggleMenu(CanvasGroup canvasGroup, bool toggle)
    {
        if (canvasGroup == heroCommandCanvasGroup) heroCommandAnimator.SetBool("toggleOn", toggle);
        else if (canvasGroup == playerCommandCanvasGroup) playerCommandAnimator.SetBool("toggleOn", toggle);

        if (toggle)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cam.ToggleCameraRotation(false);
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cam.ToggleCameraRotation(true);
        }
    }

    // All button onClicks should be listed here

    /// <summary>
    /// Called when the user clicks the Schedule button in the hero command menu, opens the Schedule menu
    /// Assigned to: [UI]/HeroZoneCanvas/HeroCommand/Holder/ButtonGroup/ScheduleButton
    /// </summary>
    public void ScheduleMenuOnClick()
    {
        // generate dropdown lists
        scheduleMenuHandler.GenerateDropdowns();

        // Set ui texts
        scheduleMenuHandler.SetTexts();

        // Display the ScheduleCanvas
        i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.SCHEDULE);
    }

    /// <summary>
    /// Called when the user clicks in the Back button in the schedule menu.  Just goes back to the root menu.
    /// Assigned to: [UI]/ScheduleCanvas/ScheduleHolder/BackButton
    /// </summary>
    public void ScheduleMenuBackOnClick()
    {
        // Go back to root menu
        i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.ROOT);
    }

    /// <summary>
    /// When clicking 'back' in the Training Equipment Menu, this function is called.  It just goes back to the HeroCommand root menu.
    /// Assigned to: [UI]/TrainingEquipmentMenu/MenuButtonGroup/BackButton.OnClick()
    /// </summary>
    public void TrainingEquipmentMenuOnBackClick()
    {
        if (i.GetHeroCommandMenuState() == EnumHandler.HeroCommandMenuStates.TRAININGEQUIPLIST)
        {
            // hide the equip list
            i.TransitionToMenu(i.GetTrainingEquipmentMenuCanvasGroup(), true);
        }

        i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.ROOT);
    }
}

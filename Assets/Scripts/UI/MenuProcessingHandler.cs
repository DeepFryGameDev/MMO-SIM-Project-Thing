using UnityEngine;

// Purpose: Facilitates control of which menu is available to the user at a given time
// Directions: Attach to the [UI] object
// Other notes: 

public class MenuProcessingHandler : MonoBehaviour
{
    public static MenuProcessingHandler i;

    // -- Home
    EnumHandler.HeroCommandHomeMenuStates heroCommandHomeMenuState;
    public void SetHeroCommandMenuState(EnumHandler.HeroCommandHomeMenuStates menuState) { heroCommandHomeMenuState = menuState; }
    public EnumHandler.HeroCommandHomeMenuStates GetHeroCommandMenuState() { return heroCommandHomeMenuState; }

    EnumHandler.HeroCommandHomeMenuStates tempHeroCommandHomeMenuState; // Used so UI is only updated once.


    EnumHandler.PlayerCommandHomeMenuStates playerCommandHomeMenuState;
    public void SetPlayerCommandMenuState(EnumHandler.PlayerCommandHomeMenuStates menuState) { playerCommandHomeMenuState = menuState; }
    public EnumHandler.PlayerCommandHomeMenuStates GetPlayerCommandMenuState() { return playerCommandHomeMenuState; }

    EnumHandler.PlayerCommandHomeMenuStates tempPlayerCommandHomeMenuState; // Used so UI is only updated once.

    // -- Field
    EnumHandler.HeroCommandFieldMenuStates heroCommandFieldMenuState;
    public void SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates menuState) { heroCommandFieldMenuState = menuState; }
    public EnumHandler.HeroCommandFieldMenuStates GetHeroCommandFieldMenuState() { return heroCommandFieldMenuState; }

    EnumHandler.HeroCommandFieldMenuStates tempHeroCommandFieldMenuState; // Used so UI is only updated once.


    EnumHandler.PlayerCommandFieldMenuStates playerCommandFieldMenuState;
    public void SetPlayerCommandFieldMenuState(EnumHandler.PlayerCommandFieldMenuStates menuState) { playerCommandFieldMenuState = menuState; }
    public EnumHandler.PlayerCommandFieldMenuStates GetPlayerCommandFieldMenuState() { return playerCommandFieldMenuState; }

    EnumHandler.PlayerCommandFieldMenuStates tempPlayerCommandFieldMenuState; // Used so UI is only updated once.

    HeroCommandProcessing heroCommandProcessing;

    // Schedule System
    [SerializeField] ScheduleMenuHandler scheduleMenuHandler;

    // will need access to every canvas group
    // -- HOME
    [SerializeField] CanvasGroup heroCommandHomeCanvasGroup;
    [SerializeField] Animator heroCommandHomeAnimator;
    [SerializeField] CanvasGroup trainingEquipmentMenuCanvasGroup;

    [SerializeField] CanvasGroup playerCommandHomeCanvasGroup;
    [SerializeField] CanvasGroup partyMenuCanvasGroup;
    [SerializeField] Animator playerCommandHomeAnimator;

    [SerializeField] CanvasGroup heroInventoryCanvasGroup;

    [SerializeField] CanvasGroup heroEquipCanvasGroup;

    // -- FIELD
    [SerializeField] CanvasGroup playerCommandFieldCanvasGroup;
    [SerializeField] Animator heroCommandFieldAnimator;

    [SerializeField] Animator playerCommandFieldAnimator;

    // ------------------------------

    public CanvasGroup GetTrainingEquipmentMenuCanvasGroup() { return trainingEquipmentMenuCanvasGroup; }
    [SerializeField] CanvasGroup trainingEquipmentListCanvasGroup;
    public CanvasGroup GetTrainingEquipmentListCanvasGroup() { return trainingEquipmentListCanvasGroup; }

    [SerializeField] CanvasGroup scheduleCanvasGroup;
    public CanvasGroup GetScheduleCanvasGroup() { return scheduleCanvasGroup; }

    [SerializeField] StatusMenuHandler statusMenuHandler;

    [SerializeField] CanvasGroup statusMenuCanvasGroup;

    //----

    ThirdPersonCam cam; // Used to disable camera rotating when the player's movement is disabled

    CanvasGroup tempCanvasGroup; // Used as the last CanvasGroup to be interacted with
    void Awake()
    {
        i = this;

        cam = FindFirstObjectByType<ThirdPersonCam>();
        heroCommandProcessing = FindFirstObjectByType<HeroCommandProcessing>();
    }

    void Start()
    {
        tempHeroCommandHomeMenuState = EnumHandler.HeroCommandHomeMenuStates.IDLE;
        tempHeroCommandFieldMenuState = EnumHandler.HeroCommandFieldMenuStates.IDLE;
    }

    void Update()
    {
        //Debug.Log("tempPlayerCommandHomeState: " + tempPlayerCommandHomeMenuState.ToString() + " / playerCommandHomeState: " + playerCommandHomeMenuState.ToString());
        //Debug.Log("tempPlayerCommandFieldState: " + tempPlayerCommandFieldMenuState.ToString() + " / playerCommandFieldState: " + playerCommandFieldMenuState.ToString());

        if (tempHeroCommandHomeMenuState != heroCommandHomeMenuState) ProcessHeroCommandHomeMenu();

        if (tempPlayerCommandHomeMenuState != playerCommandHomeMenuState) ProcessPlayerCommandHomeMenu();

        if (tempHeroCommandFieldMenuState != heroCommandFieldMenuState) ProcessHeroCommandFieldMenu();

        if (tempPlayerCommandFieldMenuState != playerCommandFieldMenuState) ProcessPlayerCommandFieldMenu();
    }

    /// <summary>
    /// Opens/closes menus based on the menu state - this gives easy control to outside scripts to open any given menu
    /// </summary>
    void ProcessHeroCommandHomeMenu()
    { 
        switch (heroCommandHomeMenuState)
        {
            case EnumHandler.HeroCommandHomeMenuStates.IDLE: // Hide the hero command menu
                ToggleMenu(heroCommandHomeCanvasGroup, false);

                break;
            case EnumHandler.HeroCommandHomeMenuStates.ROOT: // Display root contents of hero command menu
                if (tempHeroCommandHomeMenuState == EnumHandler.HeroCommandHomeMenuStates.IDLE) // coming from idle (no menu)
                {
                    ToggleMenu(heroCommandHomeCanvasGroup, true);
                    tempCanvasGroup = heroCommandHomeCanvasGroup;

                }
                else if (tempHeroCommandHomeMenuState == EnumHandler.HeroCommandHomeMenuStates.STATUS)
                {
                    statusMenuHandler.ToggleActiveEffectsStatusMenu(false);
                    TransitionToMenu(heroCommandHomeCanvasGroup, tempCanvasGroup);

                }
                else if (tempHeroCommandHomeMenuState == EnumHandler.HeroCommandHomeMenuStates.EQUIP)
                {
                    TransitionToMenu(heroCommandHomeCanvasGroup, tempCanvasGroup);
                    statusMenuHandler.ToggleMenu(false);

                }
                else
                {
                    TransitionToMenu(heroCommandHomeCanvasGroup, tempCanvasGroup);

                }
                    break;
            case EnumHandler.HeroCommandHomeMenuStates.STATUS:
                TransitionToMenu(statusMenuCanvasGroup, true);
                statusMenuHandler.ToggleActiveEffectsStatusMenu(true);

                break;
            case EnumHandler.HeroCommandHomeMenuStates.INVENTORY: // Display inventory menu for hero
                TransitionToMenu(heroInventoryCanvasGroup, true);

                break;
            case EnumHandler.HeroCommandHomeMenuStates.EQUIP:
                TransitionToMenu(heroEquipCanvasGroup, true);
                statusMenuHandler.ToggleMenu(true);

                break;
            case EnumHandler.HeroCommandHomeMenuStates.TRAININGEQUIP: // Display training equipment menu
                TransitionToMenu(trainingEquipmentMenuCanvasGroup, true);

                break;
            case EnumHandler.HeroCommandHomeMenuStates.TRAININGEQUIPLIST:
                TransitionToMenu(trainingEquipmentListCanvasGroup, false);

                break;

            case EnumHandler.HeroCommandHomeMenuStates.SCHEDULE:
                TransitionToMenu(scheduleCanvasGroup, true);
                
                break;
        }

        tempHeroCommandHomeMenuState = heroCommandHomeMenuState;
    }

    /// <summary>
    /// Opens/closes menus based on the menu state - this gives easy control to outside scripts to open any given menu
    /// </summary>
    void ProcessPlayerCommandHomeMenu()
    {
        switch (playerCommandHomeMenuState)
        {
            case EnumHandler.PlayerCommandHomeMenuStates.IDLE: // Hide the player command menu
                ToggleMenu(playerCommandHomeCanvasGroup, false);

                break;
            case EnumHandler.PlayerCommandHomeMenuStates.ROOT: // Display root contents of player command menu
                if (tempPlayerCommandHomeMenuState == EnumHandler.PlayerCommandHomeMenuStates.IDLE) // coming from idle (no menu)
                {
                    ToggleMenu(playerCommandHomeCanvasGroup, true);
                    tempCanvasGroup = playerCommandHomeCanvasGroup;
                }
                else
                {
                    TransitionToMenu(playerCommandHomeCanvasGroup, tempCanvasGroup);
                }
                break;
            case EnumHandler.PlayerCommandHomeMenuStates.PARTY:
                TransitionToMenu(partyMenuCanvasGroup, tempCanvasGroup);
                break;
        }

        tempPlayerCommandHomeMenuState = playerCommandHomeMenuState;
    }

    /// <summary>
    /// Opens/closes menus based on the menu state - this gives easy control to outside scripts to open any given menu
    /// </summary>
    void ProcessHeroCommandFieldMenu()
    {
        switch (heroCommandFieldMenuState)
        {
            case EnumHandler.HeroCommandFieldMenuStates.IDLE:
                
                break;
            case EnumHandler.HeroCommandFieldMenuStates.ROOT:
                
                break;
        }

        tempHeroCommandFieldMenuState = heroCommandFieldMenuState;
    }

    /// <summary>
    /// Opens/closes menus based on the menu state - this gives easy control to outside scripts to open any given menu
    /// </summary>
    void ProcessPlayerCommandFieldMenu()
    {
        switch (playerCommandFieldMenuState)
        {
            case EnumHandler.PlayerCommandFieldMenuStates.IDLE:
                Debug.Log("ProcessPlayerCommandField is idle");

                ToggleMenu(playerCommandFieldCanvasGroup, false);

                break;
            case EnumHandler.PlayerCommandFieldMenuStates.ROOT: // Display root contents of player command menu
                if (tempPlayerCommandFieldMenuState == EnumHandler.PlayerCommandFieldMenuStates.IDLE) // coming from idle (no menu)
                {
                    Debug.Log("Should be displaying player command menu");
                    // generate hero panels
                    heroCommandProcessing.GenerateHeroFieldCommandMenu();

                    ToggleMenu(playerCommandFieldCanvasGroup, true);
                } else
                {
                    TransitionToMenu(playerCommandFieldCanvasGroup, tempCanvasGroup);
                }
                break;
        }

        tempPlayerCommandFieldMenuState = playerCommandFieldMenuState;
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
        if (canvasGroup == heroCommandHomeCanvasGroup) heroCommandHomeAnimator.SetBool("toggleOn", toggle);
        else if (canvasGroup == playerCommandHomeCanvasGroup) playerCommandHomeAnimator.SetBool("toggleOn", toggle);
        else if (canvasGroup == playerCommandFieldCanvasGroup)
        {
            playerCommandFieldAnimator.SetBool("toggleOn", toggle);
            heroCommandFieldAnimator.SetBool("toggleOn", toggle);
        }        

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
    /// Called when the user clicks the Schedule button in the hero command menu, opens the Schedule menu.  Should eventually be moved to it's own UI handler for schedule.
    /// Assigned to: [UI]/HeroZoneCanvas/HeroCommand/Holder/ButtonGroup/ScheduleButton
    /// </summary>
    public void ScheduleMenuOnClick()
    {
        // generate dropdown lists
        scheduleMenuHandler.GenerateDropdowns();

        // Set ui texts
        scheduleMenuHandler.SetTexts();

        // Display the ScheduleCanvas
        i.SetHeroCommandMenuState(EnumHandler.HeroCommandHomeMenuStates.SCHEDULE);
    }

    /// <summary>
    /// Called when the user clicks in the Back button in the schedule menu.  Just goes back to the root menu.  Should eventually be moved to it's own UI handler for schedule.
    /// Assigned to: [UI]/ScheduleCanvas/ScheduleHolder/BackButton
    /// </summary>
    public void ScheduleMenuBackOnClick()
    {
        // Go back to root menu
        i.SetHeroCommandMenuState(EnumHandler.HeroCommandHomeMenuStates.ROOT);
    }

    /// <summary>
    /// When clicking 'back' in the Training Equipment Menu, this function is called.  It just goes back to the HeroCommand root menu.
    /// Assigned to: [UI]/TrainingEquipmentMenu/MenuButtonGroup/BackButton.OnClick()
    /// </summary>
    public void TrainingEquipmentMenuOnBackClick()
    {
        if (i.GetHeroCommandMenuState() == EnumHandler.HeroCommandHomeMenuStates.TRAININGEQUIPLIST)
        {
            // hide the equip list
            i.TransitionToMenu(i.GetTrainingEquipmentMenuCanvasGroup(), true);
        }

        i.SetHeroCommandMenuState(EnumHandler.HeroCommandHomeMenuStates.ROOT);
    }
}

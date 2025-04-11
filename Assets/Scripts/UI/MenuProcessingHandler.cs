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

    // Schedule System
    [SerializeField] ScheduleMenuHandler scheduleMenuHandler;

    // will need access to every canvas group
    [SerializeField] CanvasGroup heroCommandCanvasGroup;
    [SerializeField] Animator heroCommandAnimator;
    [SerializeField] CanvasGroup trainingEquipmentMenuCanvasGroup;
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

                    // should hide the current week toast here
                } else
                {
                    TransitionToMenu(heroCommandCanvasGroup, tempCanvasGroup);
                }
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
        heroCommandAnimator.SetBool("toggleOn", toggle);

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
    /// 
    /// Assigned to: 
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
    /// 
    /// Assigned to: 
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

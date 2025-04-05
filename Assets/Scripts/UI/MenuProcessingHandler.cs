using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class MenuProcessingHandler : MonoBehaviour
{
    public static MenuProcessingHandler i;

    EnumHandler.HeroCommandMenuStates heroCommandMenuState;
    public void SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates menuState) { heroCommandMenuState = menuState; }
    public EnumHandler.HeroCommandMenuStates GetHeroCommandMenuState() { return heroCommandMenuState; }

    EnumHandler.HeroCommandMenuStates tempHeroCommandMenuState; // Used so UI is only updated once.

    // will need access to every canvas group
    [SerializeField] CanvasGroup HeroCommandCanvasGroup;
    [SerializeField] CanvasGroup TrainingEquipmentMenuCanvasGroup;
    public CanvasGroup GetTrainingEquipmentMenuCanvasGroup() { return TrainingEquipmentMenuCanvasGroup; }
    [SerializeField] CanvasGroup TrainingEquipmentListCanvasGroup;
    public CanvasGroup GetTrainingEquipmentListCanvasGroup() { return TrainingEquipmentListCanvasGroup; }

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

    void ProcessHeroCommandMenu()
    { 
        switch (heroCommandMenuState)
        {
            case EnumHandler.HeroCommandMenuStates.IDLE: // Hide the hero command menu
                ToggleMenu(HeroCommandCanvasGroup, false);

                break;
            case EnumHandler.HeroCommandMenuStates.ROOT: // Display root contents of hero command menu
                if (tempHeroCommandMenuState == EnumHandler.HeroCommandMenuStates.IDLE) // coming from idle (no menu)
                {
                    ToggleMenu(HeroCommandCanvasGroup, true);
                } else
                {
                    TransitionToMenu(HeroCommandCanvasGroup, tempCanvasGroup);
                }
                    break;
            case EnumHandler.HeroCommandMenuStates.TRAININGEQUIP: // Display training equipment menu
                TransitionToMenu(TrainingEquipmentMenuCanvasGroup, true);

                break;
            case EnumHandler.HeroCommandMenuStates.TRAININGEQUIPLIST:
                TransitionToMenu(TrainingEquipmentListCanvasGroup, false);

                break;
        }

        tempHeroCommandMenuState = heroCommandMenuState;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="closePrevious"></param>
    public void TransitionToMenu(CanvasGroup canvasGroup, bool closePrevious)
    {
        if (closePrevious && tempCanvasGroup != null)
        {
            // close stuff from tempCanvasGroup
            tempCanvasGroup.alpha = 0;
            tempCanvasGroup.interactable = false;
            tempCanvasGroup.blocksRaycasts = false;
        }        

        // open new stuff
        canvasGroup.alpha = 1;
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
        if (toggle)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cam.ToggleCameraRotation(false);
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cam.ToggleCameraRotation(true);
        }
    }
}

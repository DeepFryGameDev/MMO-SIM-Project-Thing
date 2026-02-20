using TMPro;
using UnityEngine;

// Purpose: Facilitates the processes for the player command menu
// Directions: Attach to PlayerCommandMenu/PlayerCommand object
// Other notes: 

public class PlayerCommandMenuHandler : MonoBehaviour
{
    PlayerMovement playerMovement;

    ThirdPersonCam cam;

    PlayerWhistle playerWhistle;

    [SerializeField] PartyMenuHandler partyMenuHandler;

    [SerializeField] TextMeshProUGUI homeWeekText;
    [SerializeField] TextMeshProUGUI homeMonthText;
    [SerializeField] TextMeshProUGUI homeYearText;
    [SerializeField] TextMeshProUGUI fieldWeekText;
    [SerializeField] TextMeshProUGUI fieldMonthText;
    [SerializeField] TextMeshProUGUI fieldYearText;

    InputSubscription inputSubscription;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        cam = FindFirstObjectByType<ThirdPersonCam>();
        playerWhistle = FindFirstObjectByType<PlayerWhistle>();

        inputSubscription = GameObject.Find("[System]").GetComponent<InputSubscription>();
    }

    void Update()
    {
            CheckForMenuKey();      
    }

    /// <summary>
    /// Ran in Update() to open the player command menu when the player hits the designated key
    /// </summary>
    void CheckForMenuKey()
    {
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.FIELD:
                if (inputSubscription.commandMenuInput && UISettings.GetUIState() == EnumHandler.UIStates.IDLE)
                {
                    // For now we are disabling player movement.  Eventually I think it would be cool to allow player movement while action is happening, but we need to figure out how to switch to a camera mode that works without needing the mouse.
                    playerMovement.ToggleMovement(false);
                    cam.ToggleCameraRotation(false);

                    MenuProcessingHandler.i.SetPlayerCommandFieldMenuState(EnumHandler.PlayerCommandFieldMenuStates.ROOT);
                    MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.ROOT);

                    SetFieldDateTexts();

                    // should put in a better function later
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    UISettings.SetUIState(EnumHandler.UIStates.PLAYERCOMMAND);

                    MenuProcessingHandler.i.SetPlayerCommandFieldMenuState(EnumHandler.PlayerCommandFieldMenuStates.ROOT);
                }              
                break;
            case EnumHandler.SceneMode.HOME:
                if (inputSubscription.commandMenuInput && UISettings.GetUIState() == EnumHandler.UIStates.IDLE)
                {
                    // disable player movement
                    playerMovement.ToggleMovement(false);

                    // disable player whistle ability
                    playerWhistle.ToggleCanWhistle(false);

                    // Set the date text labels
                    SetHomeDateTexts();

                    // open menu
                    MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandHomeMenuStates.ROOT);

                    // should fix later
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    cam.ToggleCameraRotation(false);

                    UISettings.SetUIState(EnumHandler.UIStates.PLAYERCOMMAND);

                    DateManager.i.StopNewWeekToast();
                }
                break;
        }        
    }

    /// <summary>
    /// Just sets the texts on the player command menu to the current date.
    /// </summary>
    void SetHomeDateTexts()
    {
        homeWeekText.text = "Week " + DateSettings.GetRealCurrentWeek().ToString();
        homeMonthText.text = DateManager.i.GetMonthString(DateSettings.GetCurrentMonth());
        homeYearText.text = DateSettings.GetCurrentYear().ToString();
    }

    /// <summary>
    /// Just sets the texts on the player command menu to the current date.
    /// </summary>
    void SetFieldDateTexts()
    {
        fieldWeekText.text = "Week " + DateSettings.GetRealCurrentWeek().ToString();
        fieldMonthText.text = DateManager.i.GetMonthString(DateSettings.GetCurrentMonth());
        fieldYearText.text = DateSettings.GetCurrentYear().ToString();
    }

    /// <summary>
    /// Set to the 'Close' button in the player command menu
    /// These are the functions that will run when the player command menu is closed.
    /// </summary>
    public void CloseMenuButtonClicked()
    {
        CloseHomeMenu(true);
    }

    /// <summary>
    /// Closes the menu and sets the menu states back to idle
    /// </summary>
    /// <param name="allowMovement">If the player simply closed the menu and can move again, or if the system is moving to the next day and blocks movement.</param>
    void CloseHomeMenu(bool allowMovement)
    {
        // close menu
        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandHomeMenuStates.IDLE);

        if (allowMovement)
        {
            // enable player movement
            playerMovement.ToggleMovement(true);

            // enable player whistle ability
            playerWhistle.ToggleCanWhistle(true);

            cam.ToggleCameraRotation(true);
        }               

        // should fix later
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UISettings.SetUIState(EnumHandler.UIStates.IDLE);
    }

    /// <summary>
    /// These are the functions that will run when the player clicks this button
    /// Assigned to: PlayerCommandMenu/PlayerCommand/Holder/ButtonGroup/NextWeekButton.OnClick()
    /// </summary>
    public void NextWeekButtonClicked()
    {
        StartCoroutine(DateManager.i.ProcessStartWeek());

        CloseHomeMenu(false);
    }

    /// <summary>
    /// Set to the 'Party' button in the player command menu
    /// Assigned to: PlayerCommandMenu/PlayerCommand/Holder/ButtonGroup/PartyButton.OnClick()
    /// </summary>
    public void PartyButtonClicked()
    {
        PartyManager.i.GenerateHeroManagerListsForMenu();

        PartyManager.i.SetPartyMenuUI();

        // show party UI
        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandHomeMenuStates.PARTY);
    }

    /// <summary>
    /// Function that is called when the user closes the field menu - this enables movement again, etc.
    /// </summary>
    /// <param name="allowMovement"></param>
    void CloseFieldMenu(bool allowMovement)
    {
        // close menu
        MenuProcessingHandler.i.SetPlayerCommandFieldMenuState(EnumHandler.PlayerCommandFieldMenuStates.IDLE);
        MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.IDLE);

        if (allowMovement)
        {
            // enable player movement
            playerMovement.ToggleMovement(true);

            // enable player whistle ability
            playerWhistle.ToggleCanWhistle(true);

            cam.ToggleCameraRotation(true);
        }

        // should fix later
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UISettings.SetUIState(EnumHandler.UIStates.IDLE);
    }

    /// <summary>
    /// Called when user clicks the 'close' button on the field menu.
    /// Assigned to: [UI]/PlayerCommandMenu/PlayerCommandFieldMenu/Holder/ButtonGroup/CloseButton.OnClick()
    /// </summary>
    public void CloseFieldMenuButtonClicked()
    {
        CloseFieldMenu(true);
    }
}

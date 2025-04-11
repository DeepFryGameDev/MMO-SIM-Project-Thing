using TMPro;
using UnityEngine;

// Purpose: Facilitates the processes for the player command menu
// Directions: Attach to PlayerCommandMenu/PlayerCommand object
// Other notes: 

public class PlayerCommandMenuHandler : MonoBehaviour
{
    CanvasGroup canvasGroup;

    bool commandMenuOpen;

    PlayerMovement playerMovement;

    ThirdPersonCam cam;

    PlayerWhistle playerWhistle;

    Animator anim;

    [SerializeField] TextMeshProUGUI weekText;
    [SerializeField] TextMeshProUGUI monthText;
    [SerializeField] TextMeshProUGUI yearText;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        cam = FindFirstObjectByType<ThirdPersonCam>();
        playerWhistle = FindFirstObjectByType<PlayerWhistle>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!commandMenuOpen)
        {
            CheckForMenuKey();
        }        
    }

    /// <summary>
    /// Ran in Update() to open the player command menu when the player hits the designated key
    /// </summary>
    void CheckForMenuKey()
    {
        if (Input.GetKeyDown(KeyBindings.playerCommandMenuKey) && GlobalSettings.GetUIState() == GlobalSettings.UIStates.IDLE)
        {
            // disable player movement
            playerMovement.ToggleMovement(false);

            // disable player whistle ability
            playerWhistle.ToggleCanWhistle(false);

            // Set the date text labels
            SetDateTexts();

            // open menu
            ToggleCanvasGroup(true);

            // should fix later
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cam.ToggleCameraRotation(false);

            GlobalSettings.SetUIState(GlobalSettings.UIStates.PLAYERCOMMAND);

            DateManager.i.StopNewWeekToast();
        }
    }

    void SetDateTexts()
    {
        weekText.text = "Week " + DateManager.i.GetRealCurrentWeek().ToString();
        monthText.text = DateManager.i.GetMonthString(DateManager.i.GetCurrentMonth());
        yearText.text = DateManager.i.GetCurrentYear().ToString();
    }

    /// <summary>
    /// Set to the 'Close' button in the player command menu
    /// These are the functions that will run when the player command menu is closed.
    /// </summary>
    public void CloseMenuButtonClicked()
    {
        // close menu
        ToggleCanvasGroup(false);

        // enable player whistle ability
        playerWhistle.ToggleCanWhistle(true);

        // enable player movement
        playerMovement.ToggleMovement(true);

        // should fix later
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam.ToggleCameraRotation(true);

        GlobalSettings.SetUIState(GlobalSettings.UIStates.IDLE);
    }

    /// <summary>
    /// Set to the 'Next Week' button in the player command menu
    /// These are the functions that will run when the player clicks this button
    /// </summary>
    public void NextWeekButtonClicked()
    {
        StartCoroutine(DateManager.i.ProcessStartWeek());

        ToggleCanvasGroup(false);
    }

    /// <summary>
    /// Simply shows/hides the Player Command Menu
    /// </summary>
    /// <param name="toggle">True to show the menu, false to hide it</param>
    void ToggleCanvasGroup(bool toggle)
    {
        anim.SetBool("toggleOn", toggle);

        if (toggle)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            commandMenuOpen = true;            
        } else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            commandMenuOpen = false;
        }
    }
}

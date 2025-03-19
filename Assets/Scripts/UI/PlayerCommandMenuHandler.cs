using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class PlayerCommandMenuHandler : MonoBehaviour
{
    CanvasGroup canvasGroup;

    bool commandMenuOpen;

    DateManager dateManager;

    PlayerMovement playerMovement;

    ThirdPersonCam cam;

    PlayerWhistle playerWhistle;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        dateManager = FindFirstObjectByType<DateManager>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        cam = FindFirstObjectByType<ThirdPersonCam>();
        playerWhistle = FindFirstObjectByType<PlayerWhistle>();
    }

    void Update()
    {
        if (!commandMenuOpen)
        {
            CheckForMenuKey();
        }        
    }

    void CheckForMenuKey()
    {
        if (Input.GetKeyDown(KeyBindings.playerCommandMenuKey))
        {
            // disable player movement
            playerMovement.ToggleMovement(false);

            // disable player whistle ability
            playerWhistle.ToggleCanWhistle(false);

            // open menu
            ToggleCanvasGroup(true);

            // should fix later
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cam.ToggleCameraRotation(false);
        }
    }

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
    }

    public void NextWeekButtonClicked()
    {
        StartCoroutine(dateManager.ProcessStartWeek());

        ToggleCanvasGroup(false);
    }

    void ToggleCanvasGroup(bool toggle)
    {
        if (toggle)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;

            commandMenuOpen = true;            
        } else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;

            commandMenuOpen = false;
        }
    }
}

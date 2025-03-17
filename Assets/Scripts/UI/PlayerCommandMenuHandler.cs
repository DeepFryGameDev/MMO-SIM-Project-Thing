using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class PlayerCommandMenuHandler : MonoBehaviour
{
    CanvasGroup canvasGroup;

    bool commandMenuOpen;
    
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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

            // disable camera rotating

            // show mouse

            // disable player whistle ability

            // open menu
            ToggleCanvasGroup(true);
        }
    }

    public void CloseMenuButtonClicked()
    {
        // close menu
        ToggleCanvasGroup(false);

        // enable player whistle ability

        // hide mouse

        // enable camera rotating

        // enable player movement

    }

    public void NextWeekButtonClicked()
    {

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

using UnityEngine;
using UnityEngine.UI;

// Purpose: Handles various UI related processes
// Directions: Attach to [UI]
// Other notes:

public class UIHandler : MonoBehaviour
{
    [Tooltip("Set to the player's menu handler when they open the menu during gameplay")]
    [SerializeField] HeroMenuHandler heroMenuHandler; // Handles the player's menu interactions during gameplay

    PrefabManager prefabManager; // Used to gather the EXP and Resource panels for game startup

    PlayerManager playerManager; // Used to gather the BasePlayer for the player object

    void Awake()
    {
        prefabManager = GetComponent<PrefabManager>();

        playerManager = FindFirstObjectByType<PlayerManager>();

        heroMenuHandler = FindFirstObjectByType<HeroMenuHandler>();
    }

    /// <summary>
    /// Toggles the given panel as active or inactive
    /// </summary>
    /// <param name="Panel">Panel to toggle</param>
    /// <param name="toggle">True: Panel is active/displayed - False: Panel is inactive/hidden</param>
    public void ToggleObject(GameObject Panel, bool toggle)
    {
        Panel.SetActive(toggle);
    }
}

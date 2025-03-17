using UnityEngine;

// Purpose: Links with the KeyBindings static class to allow easy access to edit keybindings in editor and reference them in script
// Directions: Attach to the [Editor] object. Ensure any time a new keybinding is added that it is set in SetBindings() and 
// Other notes: 

public class KeyBindingsEditor : MonoBehaviour
{
    [Tooltip("Key for the player to press to interact with a given object in the world")]
    public KeyCode interactKey = KeyCode.E;

    [Tooltip("Key to whistle for a hero when standing in their zone")]
    public KeyCode whistleKey = KeyCode.Z;

    [Tooltip("Key to open player command menu")]
    public KeyCode playerCommandMenuKey = KeyCode.Tab;

    private void Awake()
    {
        SetBindings();
    }

    /// <summary>
    /// Initializes keybindings with whatever is set in the inspector
    /// </summary>
    void SetBindings()
    {
        KeyBindings.interactKey = interactKey;
        KeyBindings.whistleKey = whistleKey;

        KeyBindings.playerCommandMenuKey = playerCommandMenuKey;
    }
}


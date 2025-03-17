using UnityEngine;

// Purpose: Used to easily reference keybindings - linked with the KeyBindingsEditor to allow easy setting in the inspector
// Directions: Just reference the KeyBindings class to get the keys. Ensure any time you add a key here to add it to KeyBindingsEditor
// Other notes: Definitions of these vars are located in the KeyBindingsEditor script in the Tooltips.

public static class KeyBindings
{
    public static KeyCode interactKey;
    public static KeyCode whistleKey;

    public static KeyCode playerCommandMenuKey;
}

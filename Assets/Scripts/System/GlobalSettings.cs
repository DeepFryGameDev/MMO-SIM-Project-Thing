// Purpose: Used to maintain any global variables
// Directions: Simply just call the script to access the vars - it is static
// Other notes: 

public static class GlobalSettings
{
    public enum UIStates
    {
        IDLE,
        HEROCOMMAND,
        PLAYERCOMMAND,
        BLOCKED
    }

    static UIStates uiState; // Set any time a menu is opened so we know what menu is opened (or if it should be blocked from being opened)

    public static void SetUIState(UIStates uiState) { GlobalSettings.uiState = uiState; }
    public static UIStates GetUIState() { return uiState; }
}

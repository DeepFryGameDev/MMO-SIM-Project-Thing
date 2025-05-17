// Purpose: Contains various variables related to heroes that is used by the HeroSettingsEditor to allow easy tweaking in the editor.
// Directions: Ensure any time a var is added to the HeroSettingsEditor that it is also added here
// Other notes: Definitions of these vars are located in the HeroSettingsEditor script in the Tooltips.

public static class HeroSettings 
{
    public static float walkSpeed;
    public static float runSpeed;
    public static float catchupSpeed;

    public static float walkToTargetDistance;
    public static float minRandomWaitSeconds;
    public static float maxRandomWaitSeconds;
    public static float stoppingDistance;

    public static float runToCatchupDistance;

    public static float minLogDistance;
    public static bool logPathingStuff;

    public static float whistleStoppingDistance;

    public static float maxAttributeVal;

    public static int maxEnergy;
    public static float lowEnergyThreshold;
}

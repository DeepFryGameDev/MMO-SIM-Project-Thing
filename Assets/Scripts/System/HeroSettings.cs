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

    public static HeroManager felricHeroManager;
    public static HeroManager archieHeroManager;
    public static HeroManager mayaHeroManager;
    public static HeroManager claraHeroManager;
    public static HeroManager nicholinHeroManager;

    public static void SetHeroManager(int ID, HeroManager heroManager)
    {
        switch (ID)
        {
            case 0:
                felricHeroManager = heroManager;
                break;
            case 1:
                archieHeroManager = heroManager;
                break;
            case 2:
                mayaHeroManager = heroManager;
                break;
            case 3:
                claraHeroManager = heroManager;
                break;
            case 4:
                nicholinHeroManager = heroManager;
                break;
        }
    }

    public static HeroManager GetHeroManager(int ID)
    {
        switch (ID)
        {
            case 0:
                return felricHeroManager;
            case 1:
                return archieHeroManager;
            case 2:
                return mayaHeroManager;
            case 3:
                return claraHeroManager;
            case 4:
                return nicholinHeroManager;
            default:
                return null;
        }
    }
}

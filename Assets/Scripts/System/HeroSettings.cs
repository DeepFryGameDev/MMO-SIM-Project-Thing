// Purpose: Contains various variables related to heroes that is used by the HeroSettingsEditor to allow easy tweaking in the editor.
// Directions: Ensure any time a var is added to the HeroSettingsEditor that it is also added here
// Other notes: Definitions of these vars are located in the HeroSettingsEditor script in the Tooltips.

using System.Collections.Generic;

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

    static List<HeroManager> heroesInParty = new List<HeroManager>();
    public static List<HeroManager> GetHeroesInParty() { return heroesInParty; }
    public static void AddToParty(HeroManager heroManager) { heroesInParty.Add(heroManager); }
    public static void RemoveFromParty(HeroManager heroManager) { heroesInParty.Remove(heroManager); }
    public static void ClearParty() { heroesInParty.Clear(); }

    static List<HeroManager> idleHeroes = new List<HeroManager>();
    public static List<HeroManager> GetIdleHeroes() { return idleHeroes; }
    public static void AddToIdleHeroes(HeroManager heroManager) { idleHeroes.Add(heroManager); }
    public static void RemoveFromIdleHeroes(HeroManager heroManager) { idleHeroes.Remove(heroManager); }
    public static void ClearIdleHeroes() { idleHeroes.Clear(); }
}

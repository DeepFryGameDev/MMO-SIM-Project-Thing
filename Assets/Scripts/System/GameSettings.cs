using System.Collections.Generic;

// Purpose: Used as a static class to keep these values from being lost when transitioning scenes.  This will be used as a source of truth for heroes in the party and idle, as well as various other vars.
// Directions: Just call this class to reference it - it's static.
// Other notes: 

public static class GameSettings
{
    static List<HeroManager> heroesInParty = new List<HeroManager>();
    public static List<HeroManager> GetHeroesInParty() { return heroesInParty; }
    public static void AddToParty(HeroManager heroManager) { heroesInParty.Add(heroManager); }
    public static void RemoveFromParty(HeroManager heroManager) { heroesInParty.Remove(heroManager); }
    public static void ClearParty() { heroesInParty.Clear(); }

    static List<HeroManager> idleHeroes = new List<HeroManager>();
    public static List<HeroManager> GetIdleHeroes() { return idleHeroes; }
    public static void AddToIdleHeroes(HeroManager heroManager) { idleHeroes.Add(heroManager); } // Debug.Log("Adding to idle heroes: " + heroManager.Hero().GetName();) }
    public static void RemoveFromIdleHeroes(HeroManager heroManager) { idleHeroes.Remove(heroManager); }
    public static void ClearIdleHeroes() { idleHeroes.Clear(); } // Debug.Log("Clearing idle heroes"); }

    static int unloadSceneIndex;
    public static int GetUnloadSceneIndex() { return unloadSceneIndex; }
    public static void SetUnloadSceneIndex(int index) { unloadSceneIndex = index; }

    static bool sceneTransitionStarted;
    public static bool GetSceneTransitionStarted() { return sceneTransitionStarted; }
    public static void SetSceneTransitionStarted(bool transitionStarted) { sceneTransitionStarted = transitionStarted; }
}

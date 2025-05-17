using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public static class GameSettings
{
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

    static int unloadSceneIndex;
    public static int GetUnloadSceneIndex() { return unloadSceneIndex; }
    public static void SetUnloadSceneIndex(int index) { unloadSceneIndex = index; }
}

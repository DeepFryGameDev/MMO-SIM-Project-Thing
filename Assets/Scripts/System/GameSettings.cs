using System.Collections.Generic;
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

    static GameObject felricObject;
    static GameObject archieObject;
    static GameObject mayaObject;
    static GameObject claraObject;
    static GameObject nicholinObject;

    public static void SetHeroObject(int ID, GameObject obj)
    {
        switch (ID)
        {
            case 0:
                felricObject = obj;
                break;
            case 1:
                archieObject = obj;
                break;
            case 2:
                mayaObject = obj;
                break;
            case 3:
                claraObject = obj;
                break;
            case 4:
                nicholinObject = obj;
                break;
        }
    }

    public static GameObject GetHeroObject(int ID)
    {
        switch (ID)
        {
            case 0:
                return felricObject;
            case 1:
                return archieObject;
            case 2:
                return mayaObject;
            case 3:
                return claraObject;
            case 4:
                return nicholinObject;
            default:
                return null;
        }
    }
}

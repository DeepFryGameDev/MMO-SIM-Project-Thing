using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class PartyManager : MonoBehaviour
{
    [Tooltip("Serialized only for easy viewing in the inspector.  Do not change these. \n These are the heroes currently at home, not in the party.")]
    [SerializeField] List<HeroManager> inactiveHeroes = new List<HeroManager>();
    public List<HeroManager> GetInactiveHeroes() { return inactiveHeroes; }
    public void AddToInactiveHeroes(HeroManager heroManager) { inactiveHeroes.Add(heroManager); }
    public void RemoveFromInactiveHeroes(HeroManager heroManager) { inactiveHeroes.Remove(heroManager); }
    public void ClearInactiveHeroes() { inactiveHeroes.Clear(); }

    [Tooltip("Serialized only for easy viewing in the inspector.  Do not change these. \n These are the heroes currently in the party and following the player.")]
    [SerializeField] List<HeroManager> activeHeroes = new List<HeroManager>();
    public List<HeroManager> GetActiveHeroes() { return activeHeroes; }
    public void AddToActiveHeroes(HeroManager heroManager) { activeHeroes.Add(heroManager); }
    public void RemoveFromActiveHeroes(HeroManager heroManager) { activeHeroes.Remove(heroManager); }
    public void ClearActiveHeroes() { activeHeroes.Clear(); }

    [SerializeField] List<HeroManager> tempInactiveHeroes = new List<HeroManager>(); // used to set the list before "confirm" is clicked
    public List<HeroManager> GetTempInactiveHeroes() { return tempInactiveHeroes; }
    public void AddToTempInactiveHeroes(HeroManager heroManager) { tempInactiveHeroes.Add(heroManager); }
    public void RemoveFromTempInactiveHeroes(HeroManager heroManager) { tempInactiveHeroes.Remove(heroManager); }
    public void ClearTempInactiveHeroes() { tempInactiveHeroes.Clear(); }

    [SerializeField] List<HeroManager> tempActiveHeroes = new List<HeroManager>(); // used to set the list before "confirm" is clicked
    public List<HeroManager> GetTempActiveHeroes() { return tempActiveHeroes; }
    public void AddToTempActiveHeroes(HeroManager heroManager) { tempActiveHeroes.Add(heroManager); }
    public void RemoveFromTempActiveHeroes(HeroManager heroManager) { tempActiveHeroes.Remove(heroManager); }
    public void ClearTempActiveHeroes() { tempActiveHeroes.Clear(); }

    PartyMenuHandler partyMenuHandler;

    public static PartyManager i;

    private void Awake()
    {
        i = this;

        partyMenuHandler = FindFirstObjectByType<PartyMenuHandler>();

        SetDefaultLists();
    }

    void SetDefaultLists()
    {
        foreach (HeroManager heroManager in NewGameSetup.i.GetActiveHeroes())
        {
            inactiveHeroes.Add(heroManager); // set to home
        }

        foreach (HeroManager heroManager in activeHeroes)
        {
            activeHeroes.Add(heroManager); // set to party
        }
    }

    public void GenerateHeroLists()
    {
        ClearTempActiveHeroes();
        ClearTempInactiveHeroes();

        foreach (HeroManager heroManager in activeHeroes)
        {
            tempActiveHeroes.Add(heroManager);
        }

        foreach (HeroManager heroManager in inactiveHeroes)
        {
            tempInactiveHeroes.Add(heroManager);
        }
    }

    public void SetPartyMenuUI()
    {
        partyMenuHandler.SetHomeHeroGroup(tempInactiveHeroes);
        partyMenuHandler.SetPartyHeroGroup(tempActiveHeroes);
    }
}

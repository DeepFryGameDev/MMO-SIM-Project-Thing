using System.Collections.Generic;
using UnityEngine;

// Purpose: Facilitates functionality for manipulating the active party
// Directions: Attach to [System] object
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

    [SerializeField] List<HeroManager> tempInactiveHeroes = new List<HeroManager>(); // used to set the list before "confirm" is clicked.  Allows the user to cancel out of the party menu without saving the choices
    public List<HeroManager> GetTempInactiveHeroes() { return tempInactiveHeroes; }
    public void AddToTempInactiveHeroes(HeroManager heroManager) { tempInactiveHeroes.Add(heroManager); }
    public void RemoveFromTempInactiveHeroes(HeroManager heroManager) { tempInactiveHeroes.Remove(heroManager); }
    public void ClearTempInactiveHeroes() { tempInactiveHeroes.Clear(); }

    [SerializeField] List<HeroManager> tempActiveHeroes = new List<HeroManager>(); // used to set the list before "confirm" is clicked.  Allows the user to cancel out of the party menu without saving the choices
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

    void Start()
    {
        SetDefaultLists();
    }

    /// <summary>
    /// Sets the active heroes based on the NewGameSetup Active Heroes
    /// </summary>
    void SetDefaultLists()
    {
        foreach (HeroManager heroManager in HeroSettings.GetIdleHeroes())
        {
            inactiveHeroes.Add(heroManager); // set to home
        }

        foreach (HeroManager heroManager in activeHeroes)
        {
            activeHeroes.Add(heroManager); // set to party
        }
    }

    /// <summary>
    /// Prepares the Party menu by setting the temp lists based on the current active/inactive heroes.
    /// </summary>
    public void GenerateHeroManagerListsForMenu()
    {
        ClearTempActiveHeroes();
        ClearTempInactiveHeroes();

        foreach (HeroManager heroManager in activeHeroes)
        {
            tempActiveHeroes.Add(heroManager);
        }

        foreach (HeroManager heroManager in inactiveHeroes)
        {
            //Debug.Log("Add " + heroManager.Hero().GetName() + ", ID: " + heroManager.GetID() + " to  inactiveHeroes");
            tempInactiveHeroes.Add(heroManager);
        }
    }

    /// <summary>
    /// Just sets the UI LayoutGroups with the temp lists
    /// </summary>
    public void SetPartyMenuUI()
    {
        partyMenuHandler.SetPartyLayoutGroup(tempInactiveHeroes, 0);
        partyMenuHandler.SetPartyLayoutGroup(tempActiveHeroes, 1);
    }
}

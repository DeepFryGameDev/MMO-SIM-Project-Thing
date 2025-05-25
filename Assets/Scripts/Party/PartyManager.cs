using System.Collections.Generic;
using UnityEngine;

// Purpose: Facilitates functionality for manipulating the active party
// Directions: Attach to [System] object
// Other notes: 

public class PartyManager : MonoBehaviour
{
    [SerializeField] List<HeroManager> tempIdleHeroes = new List<HeroManager>(); // used to set the list before "confirm" is clicked.  Allows the user to cancel out of the party menu without saving the choices
    public List<HeroManager> GetTempIDleHeroes() { return tempIdleHeroes; }
    public void AddToTempIdleHeroes(HeroManager heroManager) { tempIdleHeroes.Add(heroManager); }
    public void RemoveFromTempIdleHeroes(HeroManager heroManager) { tempIdleHeroes.Remove(heroManager); }
    public void ClearTempIdleHeroes() { tempIdleHeroes.Clear(); }

    [SerializeField] List<HeroManager> tempPartyHeroes = new List<HeroManager>(); // used to set the list before "confirm" is clicked.  Allows the user to cancel out of the party menu without saving the choices
    public List<HeroManager> GetTempPartyHeroes() { return tempPartyHeroes; }
    public void AddToTempPartyHeroes(HeroManager heroManager) { tempPartyHeroes.Add(heroManager); }
    public void RemoveFromTempPartyHeroes(HeroManager heroManager) { tempPartyHeroes.Remove(heroManager); }
    public void ClearTempPartyHeroes() { tempPartyHeroes.Clear(); }

    PartyMenuHandler partyMenuHandler;

    public static PartyManager i;

    void Awake()
    {
        i = this;

        partyMenuHandler = FindFirstObjectByType<PartyMenuHandler>();
    }

    /// <summary>
    /// Prepares the Party menu by setting the temp lists based on the current active/inactive heroes.
    /// </summary>
    public void GenerateHeroManagerListsForMenu()
    {
        ClearTempPartyHeroes();
        ClearTempIdleHeroes();

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            tempPartyHeroes.Add(heroManager);
        }

        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
        {
            //Debug.Log("Add " + heroManager.Hero().GetName() + ", ID: " + heroManager.GetID() + " to  inactiveHeroes");
            tempIdleHeroes.Add(heroManager);
        }
    }

    /// <summary>
    /// Just sets the UI LayoutGroups with the temp lists
    /// </summary>
    public void SetPartyMenuUI()
    {      
        partyMenuHandler.SetPartyLayoutGroup(tempIdleHeroes, 0);
        partyMenuHandler.SetPartyLayoutGroup(tempPartyHeroes, 1);
    }
}

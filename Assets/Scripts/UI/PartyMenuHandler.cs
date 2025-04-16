using System.Collections.Generic;
using UnityEngine;

// Purpose: Facilitates generating the Party UI menu and other various tasks
// Directions: Attach to [UI]/PartyMenuCanvas/PartyMenuHolder
// Other notes: 

public class PartyMenuHandler : MonoBehaviour
{
    [SerializeField] Transform homeHeroGroupTransform;
    [SerializeField] Transform partyHeroGroupTransform;

    [SerializeField] PartyAnchor anchorSlot0;
    [SerializeField] PartyAnchor anchorSlot1;
    [SerializeField] PartyAnchor anchorSlot2;
    [SerializeField] PartyAnchor anchorSlot3;
    [SerializeField] PartyAnchor anchorSlot4;

    [SerializeField] PartyHUDHandler partyHUDHandler;
    [SerializeField] HeroZoneUIHandler heroZoneUIHandler;

    /// <summary>
    /// Sets the appropriate layout group in the UI with instantiated HeroFrames
    /// </summary>
    /// <param name="heroManagers">List of HeroManagers to add to the UI</param>
    /// <param name="option">Use 0 to set inactive heroes, 1 to set active</param>
    public void SetPartyLayoutGroup(List<HeroManager> heroManagers, int option)
    {
        switch (option)
        {
            case 0:
                ClearLayoutGroup(homeHeroGroupTransform);

                foreach (HeroManager heroManager in heroManagers)
                {
                    // instantiate new PartyInactiveHeroFrame
                    GameObject newInactiveFrame = Instantiate(PrefabManager.i.PartyInactiveHeroFrame, homeHeroGroupTransform);
                    PartySwitchButtonHandler handler = newInactiveFrame.GetComponent<PartySwitchButtonHandler>();

                    // set hero manager first to the handler
                    handler.SetHeroManager(heroManager);

                    // set name, level, face image vals
                    handler.SetUI();
                }
                break;
            case 1:
                ClearLayoutGroup(partyHeroGroupTransform);

                foreach (HeroManager heroManager in heroManagers)
                {
                    // instantiate new PartyActiveHeroFrame
                    GameObject newActiveFrame = Instantiate(PrefabManager.i.PartyActiveHeroFrame, partyHeroGroupTransform);
                    PartySwitchButtonHandler handler = newActiveFrame.GetComponent<PartySwitchButtonHandler>();

                    // set hero manager first to the handler
                    handler.SetHeroManager(heroManager);

                    // set name, level, face image vals
                    handler.SetUI();
                }
                break;
        }
    }

    /// <summary>
    /// When the user clicks "confirm" in the party menu, this will set the active lists to the user's choices.
    /// </summary>
    void SavePartyGroups()
    {
        PartyManager.i.ClearActiveHeroes();
        PartyManager.i.ClearInactiveHeroes();

        // clear follow anchors
        foreach (HeroManager heroManager in PartyManager.i.GetActiveHeroes())
        {
            heroManager.HeroParty().GetPartyAnchor().SetHeroManager(null);
            heroManager.HeroParty().SetPartyAnchor(null);
        }

        foreach (HeroManager heroManager in PartyManager.i.GetTempInactiveHeroes())
        {
            // save to inactive
            PartyManager.i.AddToInactiveHeroes(heroManager);
        }

        foreach (HeroManager heroManager in PartyManager.i.GetTempActiveHeroes())
        {
            // save to active
            PartyManager.i.AddToActiveHeroes(heroManager);
        }

        // Set follow anchors here
        for (int i = 0; i < PartyManager.i.GetActiveHeroes().Count; i++)
        {
            SetFollowAnchor(PartyManager.i.GetActiveHeroes()[i], i);
        }
    }

    /// <summary>
    /// Sets the transform of the given anchor to the given heroManager
    /// </summary>
    /// <param name="heroManager">The HeroManager of the hero to have the anchor set</param>
    /// <param name="slot">The slot of the anchor in the UI to be set</param>
    void SetFollowAnchor(HeroManager heroManager, int slot)
    {
        switch (slot)
        {
            case 0:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot0);
                anchorSlot0.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.Hero().GetName() + " to follow anchor slot 0", false, false);
                break;
            case 1:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot1);
                anchorSlot1.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.Hero().GetName() + " to follow anchor slot 1", false, false);
                break;
            case 2:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot2);
                anchorSlot2.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.Hero().GetName() + " to follow anchor slot 2", false, false);
                break;
            case 3:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot3);
                anchorSlot3.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.Hero().GetName() + " to follow anchor slot 3", false, false);
                break;
            case 4:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot4);
                anchorSlot4.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.Hero().GetName() + " to follow anchor slot 4", false, false);
                break;
        }
    }

    /// <summary>
    /// Just clears out all the objects under the given transform (instantiated party menu frames)
    /// </summary>
    /// <param name="group">LayoutGroup transform to clear out</param>
    void ClearLayoutGroup(Transform group)
    {
        foreach (Transform transform in group) 
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Called when the user clicks "Confirm" on the Party Menu to save the user's choices.
    /// </summary>
    public void ConfirmButtonOnClick()
    {
        SavePartyGroups();

        // Show party HUD
        partyHUDHandler.SetHeroManagers(PartyManager.i.GetActiveHeroes());
        partyHUDHandler.SetPartyHUD();

        if (!heroZoneUIHandler.getHeroZoneUIShowing())
        {
            partyHUDHandler.ToggleHUD(true);
        }

        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandMenuStates.ROOT);
    }

    /// <summary>
    /// Called when the user clicks "Cancel" on the Party menu, just clears out the temporary values and closes the menu.
    /// </summary>
    public void CancelButtonOnClick()
    {
        ClearLayoutGroup(homeHeroGroupTransform);
        ClearLayoutGroup(partyHeroGroupTransform);

        PartyManager.i.ClearTempActiveHeroes();
        PartyManager.i.ClearTempInactiveHeroes();

        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandMenuStates.ROOT);
    }
}

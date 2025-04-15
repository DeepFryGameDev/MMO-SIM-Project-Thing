using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Facilitates generating the Party UI menu and other various tasks
// Directions: 
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

    public void SetHomeHeroGroup(List<HeroManager> heroManagers)
    {
        // clear layout group
        ClearLayoutGroup(homeHeroGroupTransform);

        // for each hero manager
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
    }

    public void SetPartyHeroGroup(List<HeroManager> heroManagers)
    {
        // clear layout group
        ClearLayoutGroup(partyHeroGroupTransform);

        // for each hero manager
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
    }

    public void ConfirmButtonOnClick()
    {
        SavePartyGroups();

        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandMenuStates.ROOT);
    }

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

    void SetFollowAnchor(HeroManager heroManager, int i)
    {
        switch (i)
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

    public void CancelButtonOnClick()
    {
        ClearLayoutGroup(homeHeroGroupTransform);
        ClearLayoutGroup(partyHeroGroupTransform);

        PartyManager.i.ClearTempActiveHeroes();
        PartyManager.i.ClearTempInactiveHeroes();

        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandMenuStates.ROOT);
    }

    void ClearLayoutGroup(Transform group)
    {
        foreach (Transform transform in group) 
        {
            Destroy(transform.gameObject);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

// Purpose: Facilitates generating the Party UI menu and other various tasks
// Directions: Attach to [UI]/PartyMenuCanvas/PartyMenuHolder
// Other notes: 

public class PartyMenuHandler : MonoBehaviour
{
    [SerializeField] Transform homeHeroGroupTransform;
    [SerializeField] Transform partyHeroGroupTransform;

    [SerializeField] PartyHUDHandler partyHUDHandler;
    [SerializeField] HeroZoneUIHandler heroZoneUIHandler;

    /// <summary>
    /// Sets the appropriate layout group in the UI with instantiated HeroFrames
    /// Note: If you get an error here, it is likely because the Idle heroes were never instantiated correctly in SpawnManager.  Check SpawnManager.InitializeHeroes()
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
        if (PartyFollow.i == null) { PartyFollow.i = FindFirstObjectByType<PartyFollow>(); }

        // clear follow anchors
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            heroManager.HeroParty().GetPartyAnchor().SetHeroManager(null);
            heroManager.HeroParty().SetPartyAnchor(null);
        }

        GameSettings.ClearParty();
        GameSettings.ClearIdleHeroes();

        foreach (HeroManager heroManager in PartyManager.i.GetTempIDleHeroes())
        {
            // save to idle
            //Debug.Log("Saving to idle: " + heroManager.Hero().name);
            GameSettings.AddToIdleHeroes(heroManager);
        }

        foreach (HeroManager heroManager in PartyManager.i.GetTempPartyHeroes())
        {
            // save to party
            GameSettings.AddToParty(heroManager);
        }

        // Set follow anchors here
        for (int i = 0; i < GameSettings.GetHeroesInParty().Count; i++)
        {
            PartyFollow.i.SetFollowAnchor(GameSettings.GetHeroesInParty()[i], i);
        }

        PartyFollow.i.SetAnchoredHeroesList();

        // And make sure they are all children of SpawnManager so they can leave the scene.
        //foreach (HeroManager heroManager in PartyManager.i.GetActiveHeroes())
        //{
        //    heroManager.transform.SetParent(SpawnManager.i.transform);
        //}
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
        partyHUDHandler.SetHeroManagers(GameSettings.GetHeroesInParty());
        partyHUDHandler.SetPartyHUD();

        PartyFollow.i.SetTempFollowList();

        if (!heroZoneUIHandler.getHeroZoneUIShowing() && GameSettings.GetHeroesInParty().Count > 0)
        {
            partyHUDHandler.ToggleHUD(true);
        }

        if (GameSettings.GetHeroesInParty().Count == 0) // if user sets an empty party
        {
            partyHUDHandler.ToggleHUD(false);
        }

        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandHomeMenuStates.ROOT);
    }

    /// <summary>
    /// Called when the user clicks "Cancel" on the Party menu, just clears out the temporary values and closes the menu.
    /// </summary>
    public void CancelButtonOnClick()
    {
        ClearLayoutGroup(homeHeroGroupTransform);
        ClearLayoutGroup(partyHeroGroupTransform);

        PartyManager.i.ClearTempPartyHeroes();
        PartyManager.i.ClearTempIdleHeroes();

        MenuProcessingHandler.i.SetPlayerCommandMenuState(EnumHandler.PlayerCommandHomeMenuStates.ROOT);
    }
}

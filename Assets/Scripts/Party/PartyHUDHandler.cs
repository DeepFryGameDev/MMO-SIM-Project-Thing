using System.Collections.Generic;
using UnityEngine;

// Purpose: Facilitates mainpulating the party HUD UI
// Directions: Attach to [UI]/HUDCanvas/PartyPanel
// Other notes: 

public class PartyHUDHandler : MonoBehaviour
{
    List<HeroManager> heroManagers = new List<HeroManager>();
    public void ClearHeroManagers() { heroManagers.Clear(); }
    public void SetHeroManagers(List<HeroManager> heroManagers) { foreach (HeroManager heroManager in heroManagers) this.heroManagers.Add(heroManager); }

    Animator anim;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets the party HUD up by clearing the old HUD frames and adding new ones
    /// </summary>
    public void SetPartyHUD()
    {
        ClearPartyHUD();

        // for each active hero
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            // instantiate new party hud frame
            GameObject newPartyHudFrame = Instantiate(PrefabManager.i.PartyHUDFrame, transform);

            // get access to its hud frame handler and set the UI
            newPartyHudFrame.GetComponent<PartyHUDFrameHandler>().SetUI(heroManager);
        }
    }

    /// <summary>
    /// Clears the HUD frames under this object
    /// </summary>
    void ClearPartyHUD()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Just turns the HUD on and off
    /// </summary>
    /// <param name="toggle">True to turn it on, False to turn it off</param>
    public void ToggleHUD(bool toggle)
    {
        anim.SetBool("toggleOn", toggle);
    }
}

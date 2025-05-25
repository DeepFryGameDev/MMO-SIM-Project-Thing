using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Handles UI functionality for the party switch menu
// Directions: Attach to the 'PartyActiveHeroFrame' and 'PartyInactiveHeroFrame' prefabs
// Other notes: 

public class PartySwitchButtonHandler : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    void SetNameText() { transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = heroManager.Hero().GetName(); }
    void SetClassText() { transform.Find("ClassText").GetComponent<TextMeshProUGUI>().text = UITasks.CapitalizeFirstLetter(heroManager.HeroClass().GetCurrentClass().name); }
    void SetFaceImage() { transform.Find("FacePanel").GetComponent<Image>().sprite = heroManager.GetFaceImage(); }
    void SetLevelText() { transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Lv. 1"; } // will obviously be updated when leveling up is functional.  For now, level 1 is fine.

    // Just sets the UI objects based on the heroManager
    public void SetUI()
    {
        SetNameText();
        SetClassText();
        SetFaceImage();
        SetLevelText();
    }

    /// <summary>
    /// Called when the user clicks on the 'Join Party' button in the UI
    /// </summary>
    public void JoinPartyButtonOnClick()
    {
        PartyManager.i.AddToTempPartyHeroes(heroManager);
        PartyManager.i.RemoveFromTempIdleHeroes(heroManager);

        PartyManager.i.SetPartyMenuUI();
    }

    /// <summary>
    /// Called when the user clicks on the 'Leave Party' button in the UI
    /// </summary>
    public void LeavePartyButtonOnClick()
    {
        PartyManager.i.AddToTempIdleHeroes(heroManager);
        PartyManager.i.RemoveFromTempPartyHeroes(heroManager);

        PartyManager.i.SetPartyMenuUI();
    }
}

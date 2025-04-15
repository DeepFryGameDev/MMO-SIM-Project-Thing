using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class PartySwitchButtonHandler : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    public void SetNameText() { transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = heroManager.Hero().GetName(); }
    public void SetClassText() { transform.Find("ClassText").GetComponent<TextMeshProUGUI>().text = UITasks.CapitalizeFirstLetter(heroManager.GetHeroClass().ToString()); }
    public void SetFaceImage() { transform.Find("FacePanel").GetComponent<Image>().sprite = heroManager.GetFaceImage(); }
    public void SetLevelText() { transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Lv. 1"; } // will obviously be updated when leveling up is functional.  For now, level 1 is fine.

    public void SetUI()
    {
        SetNameText();
        SetClassText();
        SetFaceImage();
        SetLevelText();
    }

    public void JoinPartyButtonOnClick()
    {
        PartyManager.i.AddToTempActiveHeroes(heroManager);
        PartyManager.i.RemoveFromTempInactiveHeroes(heroManager);

        PartyManager.i.SetPartyMenuUI();
    }

    public void LeavePartyButtonOnClick()
    {
        PartyManager.i.AddToTempInactiveHeroes(heroManager);
        PartyManager.i.RemoveFromTempActiveHeroes(heroManager);

        PartyManager.i.SetPartyMenuUI();
    }
}

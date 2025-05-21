using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroCommandFieldMenuHandler : MonoBehaviour
{
    int heroID;
    HeroManager heroManager;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI classText;

    [SerializeField] Image faceImage;

    [SerializeField] Image expFill;
    [SerializeField] TextMeshProUGUI expFillText;

    [SerializeField] Image hpFill;
    [SerializeField] TextMeshProUGUI hpFillText;

    [SerializeField] Image mpFill;
    [SerializeField] TextMeshProUGUI mpFillText;

    public void SetValues(HeroManager heroManager)
    {
        heroID = heroManager.GetID();
        this.heroManager = heroManager;

        nameText.SetText(heroManager.Hero().GetName());

        levelText.SetText("Lv. " + heroManager.HeroClass().GetLevel().ToString());
        classText.SetText(heroManager.HeroClass().GetCurrentClass().name);

        faceImage.sprite = heroManager.GetFaceImage();

        expFill.fillAmount = 1f; // these will obviously need to be updated
        expFillText.SetText("1/1");

        hpFill.fillAmount = 1f;
        hpFillText.SetText("1/1");

        mpFill.fillAmount = 1f;
        mpFillText.SetText("1/1");
    }

    public void StatusButtonOnClick()
    {
        if (StatusMenuHandler.i == null) { StatusMenuHandler.i = FindFirstObjectByType<StatusMenuHandler>(); }

        MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.STATUS);
        DebugManager.i.UIDebugOut("HeroCommandFieldMenu", "Display status for " + heroManager.Hero().GetName());

        StatusMenuHandler.i.SetHeroManager(heroManager);

        StatusMenuHandler.i.SetStatusValues();
        StatusMenuHandler.i.SetFacePanelValues();

        StatusMenuHandler.i.ToggleActiveEffectsStatusMenu(true);
    }

    public void OnInventoryButtonOnClick()
    {

    }

    public void OnEquipButtonOnClick()
    {

    }
}

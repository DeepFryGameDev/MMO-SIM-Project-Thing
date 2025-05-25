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

        MenuProcessingHandler.i.SetHeroCommandFieldMenuHandler(this);
        HeroCommandProcessing.i.SetHeroManager(heroManager);

        MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.STATUS);
        DebugManager.i.UIDebugOut("HeroCommandFieldMenu", "Display status for " + heroManager.Hero().GetName());

        StatusMenuHandler.i.SetHeroManager(heroManager);

        StatusMenuHandler.i.SetStatusValues();

        StatusMenuHandler.i.ToggleActiveEffectsStatusMenu(true);
    }

    public void OnInventoryButtonOnClick()
    {
        if (HeroCommandProcessing.i == null) HeroCommandProcessing.i = FindFirstObjectByType<HeroCommandProcessing>();
        if (MenuProcessingHandler.i == null) MenuProcessingHandler.i = FindFirstObjectByType<MenuProcessingHandler>();
        if (HeroInventoryUIHandler.i == null) HeroInventoryUIHandler.i = FindFirstObjectByType<HeroInventoryUIHandler>();
        ContextMenuHandler.i = FindFirstObjectByType<ContextMenuHandler>();

        ContextMenuHandler.i.SetFieldHeroManager(heroManager);

        HeroCommandProcessing.i.SetHeroManager(heroManager);

        MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.INVENTORY);

        HeroInventoryUIHandler.i.ClearUI();
        HeroInventoryUIHandler.i.GenerateInventory(heroManager);
        DebugManager.i.UIDebugOut("HeroCommandFieldMenu", "Display inventory for " + heroManager.Hero().GetName());
    }

    public void OnEquipButtonOnClick()
    {
        if (HeroCommandProcessing.i == null) { HeroCommandProcessing.i = FindFirstObjectByType<HeroCommandProcessing>(); }
        if (HeroEquipMenuHandler.i == null) { HeroEquipMenuHandler.i = FindFirstObjectByType<HeroEquipMenuHandler>(); }
        if (StatusMenuHandler.i == null) { StatusMenuHandler.i = FindFirstObjectByType<StatusMenuHandler>(); }
        ContextMenuHandler.i = FindFirstObjectByType<ContextMenuHandler>();

        ContextMenuHandler.i.SetFieldHeroManager(heroManager);       

        HeroCommandProcessing.i.SetHeroManager(heroManager);
        HeroEquipMenuHandler.i.SetHeroManager(heroManager);

        HeroEquipMenuHandler.i.GenerateEquippedEquipmentButtons(heroManager);

        HeroCommandProcessing.i.Setup();
        HeroCommandProcessing.i.SetFacePanelValues();
        HeroCommandProcessing.i.ToggleFacePanel(true);

        StatusMenuHandler.i.Setup();
        StatusMenuHandler.i.SetHeroManager(heroManager);
        StatusMenuHandler.i.SetStatusValues();

        StatusMenuHandler.i.ToggleMenu(true);

        MenuProcessingHandler.i.SetHeroCommandFieldMenuHandler(this);
        MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.EQUIP);
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Facilitates all status menu display functionality in the status menu UI
// Directions: Attach to [UI]/HeroStatusCanvas/HeroStatusHolder
// Other notes: 

public class StatusMenuHandler : MonoBehaviour
{
    [SerializeField] Image expBarFill;
    [SerializeField] TextMeshProUGUI expBarText;

    [SerializeField] Image hpBarFill;
    [SerializeField] TextMeshProUGUI hpBarText;

    [SerializeField] Image mpBarFill;
    [SerializeField] TextMeshProUGUI mpBarText;

    [SerializeField] TextMeshProUGUI classText;

    [SerializeField] TextMeshProUGUI strengthValue;
    [SerializeField] TextMeshProUGUI enduranceValue;
    [SerializeField] TextMeshProUGUI agilityValue;
    [SerializeField] TextMeshProUGUI dexterityValue;
    [SerializeField] TextMeshProUGUI intelligenceValue;
    [SerializeField] TextMeshProUGUI faithValue;

    Animator anim;
    [SerializeField] Animator activeEffectsPanelAnim;
    [SerializeField] CanvasGroup closeButtonCanvasGroup;

    [SerializeField] Animator heroFacePanelAnim;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image faceImage;

    public static StatusMenuHandler i;

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    private void Awake()
    {
        anim = GetComponent<Animator>();

        i = this;
    }

    /// <summary>
    /// Simply sets the text values in UI to the hero's parameters in the heroManager.
    /// </summary>
    /// <param name="heroManager">HeroManager of the hero to set the status values.</param>
    public void SetStatusValues()
    {
        ClearValues();

        classText.SetText(heroManager.HeroClass().GetCurrentClass().name);

        strengthValue.SetText(heroManager.Hero().GetStrength().ToString());
        enduranceValue.SetText(heroManager.Hero().GetEndurance().ToString());
        agilityValue.SetText(heroManager.Hero().GetAgility().ToString());
        dexterityValue.SetText(heroManager.Hero().GetDexterity().ToString());
        intelligenceValue.SetText(heroManager.Hero().GetIntelligence().ToString());
        faithValue.SetText(heroManager.Hero().GetFaith().ToString());
    }

    public void SetFacePanelValues()
    {
        faceImage.sprite = heroManager.GetFaceImage();
        nameText.SetText(heroManager.Hero().GetName());        

        heroFacePanelAnim.SetBool("toggleOn", true);
    }

    /// <summary>
    /// Maybe not needed.  Just clears the text values in UI.
    /// </summary>
    public void ClearValues()
    {
        expBarFill.fillAmount = 0;
        expBarText.SetText("0/0");

        hpBarFill.fillAmount = 0;
        hpBarText.SetText("0/0");

        mpBarFill.fillAmount = 0;
        mpBarText.SetText("0/0");

        classText.SetText(string.Empty);

        strengthValue.SetText(string.Empty);
        enduranceValue.SetText(string.Empty);
        agilityValue.SetText(string.Empty);
        dexterityValue.SetText(string.Empty);
        intelligenceValue.SetText(string.Empty);
        faithValue.SetText(string.Empty);
    }

    /// <summary>
    /// Displays and hides the status menu
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleMenu(bool toggle)
    {
        anim.SetBool("toggleOn", toggle);        
    }

    /// <summary>
    /// Displays and hides the extra window that pops out from the side
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleActiveEffectsStatusMenu(bool toggle)
    {
        activeEffectsPanelAnim.SetBool("toggleOn", toggle);

        if (toggle) closeButtonCanvasGroup.alpha = 1;
        else closeButtonCanvasGroup.alpha = 0;

        closeButtonCanvasGroup.interactable = toggle;
        closeButtonCanvasGroup.blocksRaycasts = toggle;
    }

    public void ToggleFacePanelStatusMenu(bool toggle)
    {
        heroFacePanelAnim.SetBool("toggleOn", toggle);
    }

    /// <summary>
    /// Called when the user clicks the Status button in the Hero Command menu
    /// Assigned to: [UI]/HeroZoneCanvas/HeroCommand/Holder/ButtonGroup/StatusButton.OnClick()
    /// </summary>
    public void OpenStatusMenu()
    {
        SetStatusValues();

        MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.STATUS);
    }

    /// <summary>
    /// Called when the user clicks the 'Close' button in the Status menu
    /// Assigned to: [UI]/HeroStatusCanvas/HeroStatusHolder/CloseButton.OnClick()
    /// </summary>
    public void CloseStatusMenu()
    {
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.FIELD:
                MenuProcessingHandler.i.SetHeroCommandFieldMenuState(EnumHandler.HeroCommandFieldMenuStates.ROOT);
                MenuProcessingHandler.i.SetPlayerCommandFieldMenuState(EnumHandler.PlayerCommandFieldMenuStates.ROOT);
                break;
            case EnumHandler.SceneMode.HOME:
                MenuProcessingHandler.i.SetHeroCommandHomeMenuState(EnumHandler.HeroCommandHomeMenuStates.ROOT);
                break;
        }        
    }
}

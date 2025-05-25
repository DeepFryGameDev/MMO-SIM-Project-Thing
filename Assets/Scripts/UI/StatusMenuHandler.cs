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

    public static StatusMenuHandler i;

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    private void Awake()
    {
        anim = GetComponent<Animator>();

        i = this;
    }

    public void Setup()
    {
        i = this;
    }

    /// <summary>
    /// Simply sets the text values in UI to the hero's parameters in the heroManager.
    /// </summary>
    /// <param name="heroManager">HeroManager of the hero to set the status values.</param>
    public void SetStatusValues()
    {
        ClearValues(); // maybe need i.?

        if (i.heroManager == null)
        {
            switch (SceneInfo.i.GetSceneMode())
            {
                case EnumHandler.SceneMode.FIELD:

                    break;
                case EnumHandler.SceneMode.HOME:
                    i.heroManager = FindFirstObjectByType<PlayerWhistle>().GetHeroManagerWhistled();
                    break;
            }
        }

        classText.SetText(i.heroManager.HeroClass().GetCurrentClass().name);

        strengthValue.SetText(i.heroManager.Hero().GetStrength().ToString());
        enduranceValue.SetText(i.heroManager.Hero().GetEndurance().ToString());
        agilityValue.SetText(i.heroManager.Hero().GetAgility().ToString());
        dexterityValue.SetText(i.heroManager.Hero().GetDexterity().ToString());
        intelligenceValue.SetText(i.heroManager.Hero().GetIntelligence().ToString());
        faithValue.SetText(i.heroManager.Hero().GetFaith().ToString());
    }

    /// <summary>
    /// Maybe not needed.  Just clears the text values in UI.
    /// </summary>
    public void ClearValues()
    {
        if (expBarFill == null) { Debug.Log("Is null"); }
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
        if (i == null) { i = FindFirstObjectByType<StatusMenuHandler>(); }

        i.activeEffectsPanelAnim.SetBool("toggleOn", toggle);

        if (toggle) i.closeButtonCanvasGroup.alpha = 1;
        else i.closeButtonCanvasGroup.alpha = 0;

        i.closeButtonCanvasGroup.interactable = toggle;
        i.closeButtonCanvasGroup.blocksRaycasts = toggle;
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

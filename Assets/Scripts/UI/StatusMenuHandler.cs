using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Animator skillsAndEffectsPanelAnim;
    [SerializeField] CanvasGroup closeButtonCanvasGroup;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetStatusValues(HeroManager heroManager)
    {
        expBarFill.fillAmount = 0; // update
        expBarText.SetText("0/0"); // update

        hpBarFill.fillAmount = 0; // update
        hpBarText.SetText("0/0"); // update

        mpBarFill.fillAmount = 0; // update
        mpBarText.SetText("0/0"); // update

        classText.SetText(UITasks.CapitalizeFirstLetter(heroManager.HeroClass().GetCurrentClass().heroClass.ToString()));

        strengthValue.SetText(heroManager.Hero().GetStrength().ToString());
        enduranceValue.SetText(heroManager.Hero().GetEndurance().ToString());
        agilityValue.SetText(heroManager.Hero().GetAgility().ToString());
        dexterityValue.SetText(heroManager.Hero().GetDexterity().ToString());
        intelligenceValue.SetText(heroManager.Hero().GetIntelligence().ToString());
        faithValue.SetText(heroManager.Hero().GetFaith().ToString());
    }

    public void ClearValues()
    {
        expBarFill.fillAmount = 0; // update
        expBarText.SetText("0/0"); // update

        hpBarFill.fillAmount = 0; // update
        hpBarText.SetText("0/0"); // update

        mpBarFill.fillAmount = 0; // update
        mpBarText.SetText("0/0"); // update

        classText.SetText(string.Empty);

        strengthValue.SetText(string.Empty);
        enduranceValue.SetText(string.Empty);
        agilityValue.SetText(string.Empty);
        dexterityValue.SetText(string.Empty);
        intelligenceValue.SetText(string.Empty);
        faithValue.SetText(string.Empty);
    }

    public void ToggleMenu(bool toggle)
    {
        anim.SetBool("toggleOn", toggle);        
    }

    public void ToggleSkillStatusMenu(bool toggle)
    {
        skillsAndEffectsPanelAnim.SetBool("toggleOn", toggle);

        if (toggle) closeButtonCanvasGroup.alpha = 1;
        else closeButtonCanvasGroup.alpha = 0;

        closeButtonCanvasGroup.interactable = toggle;
        closeButtonCanvasGroup.blocksRaycasts = toggle;
    }

    public void OpenStatusMenu()
    {
        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.STATUS);
    }

    public void CloseStatusMenu()
    {
        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.ROOT);
    }
}

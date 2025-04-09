using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Used to process the UI display to the player of any hero's room they are standing in
// Directions: Attach to [UI]/HeroZoneCanvas/HeroZone
// Other notes: 

public class HeroZoneUIHandler : MonoBehaviour
{
    CanvasGroup canvasGroup; // Used to show/hide the Hero Zone UI

    //Chart controllers
    RadarChartValueController valueController; // manages the values of the radar chart
    RadarChartController chartController; // used to update the chart in the UI    

    [SerializeField] TrainingManager trainingManager;

    [SerializeField] ScheduleManager scheduleManager;

    //------ UI objects----------
    [Tooltip("Set to the text object that will display the hero's name")]
    [SerializeField] TextMeshProUGUI nameText;

    [Tooltip("Set to the text object that will display the hero's level")]
    [SerializeField] TextMeshProUGUI levelText;

    [Tooltip("Set to the fill Image object for the HP Parameter bar")]
    [SerializeField] Image hpFill;
    [Tooltip("Set to the text object that will display the hero's HP")]
    [SerializeField] TextMeshProUGUI hpVal;

    [Tooltip("Set to the fill Image object for the Mana Parameter bar")]
    [SerializeField] Image manaFill;
    [Tooltip("Set to the text object that will display the hero's Mana")]
    [SerializeField] TextMeshProUGUI manaVal;

    [Tooltip("Set to the fill Image object for the Strength Parameter bar")]
    [SerializeField] Image strFill;
    [Tooltip("Set to the text object that will display the hero's Strength Level")]
    [SerializeField] TextMeshProUGUI strLevelVal;
    [Tooltip("Set to the text object that will display the hero's Strength Experience")]
    [SerializeField] TextMeshProUGUI strExpVal;

    [Tooltip("Set to the fill Image object for the Endurance Parameter bar")]
    [SerializeField] Image endFill;
    [Tooltip("Set to the text object that will display the hero's Endurance Level")]
    [SerializeField] TextMeshProUGUI endLevelVal;
    [Tooltip("Set to the text object that will display the hero's Endurance Experience")]
    [SerializeField] TextMeshProUGUI endExpVal;

    [Tooltip("Set to the fill Image object for the Agility Parameter bar")]
    [SerializeField] Image agiFill;
    [Tooltip("Set to the text object that will display the hero's Agility Level")]
    [SerializeField] TextMeshProUGUI agiLevelVal;
    [Tooltip("Set to the text object that will display the hero's Agility Experience")]
    [SerializeField] TextMeshProUGUI agiExpVal;

    [Tooltip("Set to the fill Image object for the Dexterity Parameter bar")]
    [SerializeField] Image dexFill;
    [Tooltip("Set to the text object that will display the hero's Dexterity Level")]
    [SerializeField] TextMeshProUGUI dexLevelVal;
    [Tooltip("Set to the text object that will display the hero's Dexterity Experience")]
    [SerializeField] TextMeshProUGUI dexExpVal;

    [Tooltip("Set to the fill Image object for the Intelligence Parameter bar")]
    [SerializeField] Image intFill;
    [Tooltip("Set to the text object that will display the hero's Intelligence Level")]
    [SerializeField] TextMeshProUGUI intLevelVal;
    [Tooltip("Set to the text object that will display the hero's Intelligence Experience")]
    [SerializeField] TextMeshProUGUI intExpVal;

    [Tooltip("Set to the fill Image object for the Faith Parameter bar")]
    [SerializeField] Image fthFill;
    [Tooltip("Set to the text object that will display the hero's Faith Level")]
    [SerializeField] TextMeshProUGUI fthLevelVal;
    [Tooltip("Set to the text object that will display the hero's Faith Experience")]
    [SerializeField] TextMeshProUGUI fthExpVal;

    //-----------------------
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        ShowPanel(false);
    }
    
    /// <summary>
    /// Sets the text objects for the HeroZone stat panel and calls SetRadarChart
    /// </summary>
    /// <param name="hero">Hero to set stats for</param>
    public void DrawHeroStatsToPanel(HeroManager heroManager)
    {
        nameText.text = heroManager.Hero().name;
        levelText.text = "Lv. " + "1"; // to update - this is the hero's Hero Level

        DrawStatBars(heroManager);

        SetRadarChart(heroManager.Hero());
    }

    public void SetScheduleHeroManager(HeroManager heroManager)
    {
        scheduleManager.SetHeroManager(heroManager);
    }

    private void DrawStatBars(HeroManager heroManager)
    {
        strLevelVal.text = heroManager.Hero().GetStrength().ToString();
        endLevelVal.text = heroManager.Hero().GetEndurance().ToString();
        agiLevelVal.text = heroManager.Hero().GetAgility().ToString();
        dexLevelVal.text = heroManager.Hero().GetDexterity().ToString();
        intLevelVal.text = heroManager.Hero().GetIntelligence().ToString();
        fthLevelVal.text = heroManager.Hero().GetFaith().ToString();

        strExpVal.text = heroManager.HeroTraining().GetStrengthExp() + "/" + trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager).ToString();
        endExpVal.text = heroManager.HeroTraining().GetEnduranceExp() + "/" + trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager).ToString();
        agiExpVal.text = heroManager.HeroTraining().GetAgilityExp() + "/" + trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager).ToString();
        dexExpVal.text = heroManager.HeroTraining().GetDexterityExp() + "/" + trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager).ToString();
        intExpVal.text = heroManager.HeroTraining().GetIntelligenceExp() + "/" + trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager).ToString();
        fthExpVal.text = heroManager.HeroTraining().GetFaithExp() + "/" + trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager).ToString();

        strFill.fillAmount = heroManager.HeroTraining().GetStrengthExp() / trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager);
        endFill.fillAmount = heroManager.HeroTraining().GetEnduranceExp() / trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager);
        agiFill.fillAmount = heroManager.HeroTraining().GetAgilityExp() / trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager);
        dexFill.fillAmount = heroManager.HeroTraining().GetDexterityExp() / trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager);
        intFill.fillAmount = heroManager.HeroTraining().GetIntelligenceExp() / trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager);
        fthFill.fillAmount = heroManager.HeroTraining().GetFaithExp() / trainingManager.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager);
    }

    /// <summary>
    /// Sets the radar chart values
    /// </summary>
    /// <param name="hero">Hero to set the radar chart stats</param>
    private void SetRadarChart(BaseHero hero)
    {
        if (valueController == null)
        {
            valueController = FindFirstObjectByType<RadarChartValueController>();
            chartController = FindFirstObjectByType<RadarChartController>();
        }

        if (valueController != null)
        {
            valueController._valueInfo.UpdateValue(0, (hero.GetStrength() / HeroSettings.maxAttributeVal));
            valueController._valueInfo.UpdateValue(1, (hero.GetEndurance() / HeroSettings.maxAttributeVal));
            valueController._valueInfo.UpdateValue(2, (hero.GetAgility() / HeroSettings.maxAttributeVal));
            valueController._valueInfo.UpdateValue(3, (hero.GetDexterity() / HeroSettings.maxAttributeVal));
            valueController._valueInfo.UpdateValue(4, (hero.GetIntelligence() / HeroSettings.maxAttributeVal));
            valueController._valueInfo.UpdateValue(5, (hero.GetFaith() / HeroSettings.maxAttributeVal));

            chartController.UpdateValueList(0);
        }
    }

    /// <summary>
    /// Show/Hide the HeroZone Panels
    /// </summary>
    /// <param name="show">True to show panel, false to hide</param>
    public void ShowPanel(bool show)
    {
        if (show) {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        } else {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }  
    }
}

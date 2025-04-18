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

    //------ UI objects----------
    [Tooltip("Set to the text object that will display the hero's name")]
    [SerializeField] TextMeshProUGUI nameText;

    [Tooltip("Set to the image object that will display the hero's face graphic")]
    [SerializeField] Image faceImage;

    [Tooltip("Set to the fill Image object for the Mana Parameter bar")]
    [SerializeField] Image energyFill;
    [Tooltip("Set to the text object that will display the hero's Mana")]
    [SerializeField] TextMeshProUGUI energyVal;

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

    [Tooltip("Set to the Party HUD Handler on the Party Panel")]
    [SerializeField] PartyHUDHandler partyHudHandler;

    bool heroZoneUIShowing; // Is true when the hero zone interface is displayed
    public bool getHeroZoneUIShowing() { return heroZoneUIShowing; }

    Animator anim;

    //-----------------------
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        anim = GetComponent<Animator>();
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
        nameText.text = heroManager.Hero().GetName();

        faceImage.sprite = heroManager.GetFaceImage();

        DrawEnergyBar(heroManager);

        DrawStatBars(heroManager);

        SetRadarChart(heroManager.Hero());
    }

    /// <summary>
    /// Sets the energy bar fill for the given hero
    /// </summary>
    /// <param name="heroManager">Hero to have energy drawn</param>
    void DrawEnergyBar(HeroManager heroManager)
    {
        // set energy bar fill
        energyFill.fillAmount = (float)heroManager.Hero().GetEnergy() / HeroSettings.maxEnergy;

        // set energy bar value
        energyVal.text = heroManager.Hero().GetEnergy().ToString() + " / " + HeroSettings.maxEnergy;
    }

    /// <summary>
    /// Sets the strings and fills for stat levels/exp
    /// </summary>
    /// <param name="heroManager">Hero to have stats drawn</param>
    void DrawStatBars(HeroManager heroManager)
    {
        strLevelVal.text = heroManager.Hero().GetBaseStrength().ToString();
        endLevelVal.text = heroManager.Hero().GetBaseEndurance().ToString();
        agiLevelVal.text = heroManager.Hero().GetBaseAgility().ToString();
        dexLevelVal.text = heroManager.Hero().GetBaseDexterity().ToString();
        intLevelVal.text = heroManager.Hero().GetBaseIntelligence().ToString();
        fthLevelVal.text = heroManager.Hero().GetBaseFaith().ToString();

        strExpVal.text = heroManager.HeroTraining().GetStrengthExp() + "/" + TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager).ToString();
        endExpVal.text = heroManager.HeroTraining().GetEnduranceExp() + "/" + TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager).ToString();
        agiExpVal.text = heroManager.HeroTraining().GetAgilityExp() + "/" + TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager).ToString();
        dexExpVal.text = heroManager.HeroTraining().GetDexterityExp() + "/" + TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager).ToString();
        intExpVal.text = heroManager.HeroTraining().GetIntelligenceExp() + "/" + TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager).ToString();
        fthExpVal.text = heroManager.HeroTraining().GetFaithExp() + "/" + TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager).ToString();

        strFill.fillAmount = heroManager.HeroTraining().GetStrengthExp() / TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager);
        endFill.fillAmount = heroManager.HeroTraining().GetEnduranceExp() / TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager);
        agiFill.fillAmount = heroManager.HeroTraining().GetAgilityExp() / TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager);
        dexFill.fillAmount = heroManager.HeroTraining().GetDexterityExp() / TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager);
        intFill.fillAmount = heroManager.HeroTraining().GetIntelligenceExp() / TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager);
        fthFill.fillAmount = heroManager.HeroTraining().GetFaithExp() / TrainingManager.i.GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager);
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
        heroZoneUIShowing = show;

        anim.SetBool("toggleOn", show);

        if (show) {
            if (PartyManager.i.GetActiveHeroes().Count > 0)
            {
                partyHudHandler.ToggleHUD(false); // hide the party HUD
            }

            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        } else {
            if (PartyManager.i.GetActiveHeroes().Count > 0)
            {
                partyHudHandler.ToggleHUD(true); // show the party HUD again
            }

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }  
    }
}

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
    public TextMeshProUGUI nameText;

    [Tooltip("Set to the text object that will display the hero's level")]
    public TextMeshProUGUI levelText;

    [Tooltip("Set to the fill Image object for the HP Parameter bar")]
    public Image hpFill;
    [Tooltip("Set to the text object that will display the hero's HP")]
    public TextMeshProUGUI hpVal;

    [Tooltip("Set to the fill Image object for the Mana Parameter bar")]
    public Image manaFill;
    [Tooltip("Set to the text object that will display the hero's Mana")]
    public TextMeshProUGUI manaVal;

    [Tooltip("Set to the fill Image object for the Strength Parameter bar")]
    public Image strFill;
    [Tooltip("Set to the text object that will display the hero's Strength")]
    public TextMeshProUGUI strVal;

    [Tooltip("Set to the fill Image object for the Endurance Parameter bar")]
    public Image endFull;
    [Tooltip("Set to the text object that will display the hero's Endurance")]
    public TextMeshProUGUI endVal;

    [Tooltip("Set to the fill Image object for the Agility Parameter bar")]
    public Image agiFill;
    [Tooltip("Set to the text object that will display the hero's Agility")]
    public TextMeshProUGUI agiVal;

    [Tooltip("Set to the fill Image object for the Dexterity Parameter bar")]
    public Image dexFill;
    [Tooltip("Set to the text object that will display the hero's Dexterity")]
    public TextMeshProUGUI dexVal;

    [Tooltip("Set to the fill Image object for the Intelligence Parameter bar")]
    public Image intFill;
    [Tooltip("Set to the text object that will display the hero's Intelligence")]
    public TextMeshProUGUI intVal;

    [Tooltip("Set to the fill Image object for the Faith Parameter bar")]
    public Image fthFill;
    [Tooltip("Set to the text object that will display the hero's Faith")]
    public TextMeshProUGUI fthVal;

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
    public void DrawHeroStatsToPanel(BaseHero hero)
    {
        nameText.text = hero.name;
        levelText.text = "Lv. " + "1"; // to update

        SetRadarChart(hero);
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

using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Purpose: Manages the UI elements of the player menu used when the player presses the 'tab' key
// Directions: Add to the '[UI]/Canvas/CharacterMenuPanel' object
// Other notes: 

public class HeroMenuHandler : MonoBehaviour
{
    // Stats Panel
    [Tooltip("Set to the object holding TextMeshPro text component for the player's name")]
    [SerializeField] TextMeshProUGUI nameText;

    [Tooltip("Set to the object holding TextMeshPro text component for the player's strength value (PlayerStatsPanel/StatsContainer/StrengthContainer/StrengthValue)")]
    [SerializeField] TextMeshProUGUI strValueText;
    [Tooltip("Set to the object holding fill Image component for the player's strength bar (PlayerStatsPanel/StatsContainer/StrengthContainer/StrengthBarBackground/StrengthBarFill)")]
    [SerializeField] Image strFillImage;
    [Tooltip("Set to the object holding TextMeshPro text component for the player's endurance value (PlayerStatsPanel/StatsContainer/EnduranceContainer/EnduranceValue)")]
    [SerializeField] TextMeshProUGUI endValueText;
    [Tooltip("Set to the object holding fill Image component for the player's endurance bar (PlayerStatsPanel/StatsContainer/EnduranceContainer/EnduranceBarBackground/EnduranceBarFill)")]
    [SerializeField] Image endFillImage;
    [Tooltip("Set to the object holding TextMeshPro text component for the player's agility value (PlayerStatsPanel/StatsContainer/AgilityContainer/AgilityValue)")]
    [SerializeField] TextMeshProUGUI agiValueText;
    [Tooltip("Set to the object holding fill Image component for the player's agility bar (PlayerStatsPanel/StatsContainer/AgilityContainer/AgilityBarBackground/AgilityBarFill)")]
    [SerializeField] Image agiFillImage;
    [Tooltip("Set to the object holding TextMeshPro text component for the player's dexterity value (PlayerStatsPanel/StatsContainer/DexterityContainer/DexterityValue)")]
    [SerializeField] TextMeshProUGUI dexValueText;
    [Tooltip("Set to the object holding fill Image component for the player's dexterity bar (PlayerStatsPanel/StatsContainer/DexterityContainer/DexterityBarBackground/DexterityBarFill)")]
    [SerializeField] Image dexFillImage;
    [Tooltip("Set to the object holding TextMeshPro text component for the player's intelligence value (PlayerStatsPanel/StatsContainer/IntelligenceContainer/IntelligenceValue)")]
    [SerializeField] TextMeshProUGUI intValueText;
    [Tooltip("Set to the object holding fill Image component for the player's intelligence bar (PlayerStatsPanel/StatsContainer/IntelligenceContainer/IntelligenceBarBackground/IntelligenceBarFill)")]
    [SerializeField] Image intFillImage;
    [Tooltip("Set to the object holding TextMeshPro text component for the player's resistance value (PlayerStatsPanel/StatsContainer/ResistanceContainer/ResistanceValue)")]
    [SerializeField] TextMeshProUGUI resValueText;
    [Tooltip("Set to the object holding fill Image component for the player's resistance bar (PlayerStatsPanel/StatsContainer/ResistanceContainer/ResistanceBarBackground/ResistanceBarFill)")]
    [SerializeField] Image resFillImage;

    [Tooltip("Set to the object holding TextMeshPro text component for the player's armor value (CharacterMenuPanel/PlayerEquipmentPanel/ArmorIcon/ArmorText)")]
    [SerializeField] TextMeshProUGUI armorValueText;
    [Tooltip("Set to the object holding TextMeshPro text component for the player's magic resist value (CharacterMenuPanel/PlayerEquipmentPanel/MagicResistIcon/MagicResistText)")]
    [SerializeField] TextMeshProUGUI magicResistValueText;

    [Tooltip("Set to the object holding CanvasGroup component for CharacterMenuPanel")]
    [SerializeField] CanvasGroup characterMenuCanvasGroup;

    // Equipment Panel
    [Tooltip("Set to the object holding image component to display the player's silhouette (CharacterMenuPanel/PlayerEquipmentPanel/SilhouetteContainer/SilhouetteImage")]
    [SerializeField] Image silhouetteImage;
    [Tooltip("Set to whichever sprite should be used to display as the silhouette for warrior class")]
    [SerializeField] Sprite warriorSilouette;
    [Tooltip("Set to whichever sprite should be used to display as the silhouette for mage class")]
    [SerializeField] Sprite mageSilhouette;
    [Tooltip("Set to whichever sprite should be used to display as the silhouette for archer class")]
    [SerializeField] Sprite archerSilhouette;

    [Tooltip("Set to the object holding GridLayoutGroup component to display player's inventory (CharacterMenuPanel/PlayerInventoryPanel/InventoryGrid")]
    [SerializeField] GridLayoutGroup inventoryGrid;

    [Tooltip("Set to the object for the player's equipped main hand container (CharacterMenuPanel/PlayerEquipmentPanel/LeftFrameLayoutGroup/MainHandContainer)")]
    [SerializeField] GameObject mainHandContainer;
    [Tooltip("Set to the object for the player's equipped off hand container (CharacterMenuPanel/PlayerEquipmentPanel/LeftFrameLayoutGroup/OffHandContainer)")]
    [SerializeField] GameObject offHandContainer;
    [Tooltip("Set to the object for the player's equipped helm container (CharacterMenuPanel/PlayerEquipmentPanel/RightFrameLayoutGroup/HelmContainer)")]
    [SerializeField] GameObject helmContainer;
    [Tooltip("Set to the object for the player's equipped chest container (CharacterMenuPanel/PlayerEquipmentPanel/RightFrameLayoutGroup/ChestContainer)")]
    [SerializeField] GameObject chestContainer;
    [Tooltip("Set to the object for the player's equipped hands container (CharacterMenuPanel/PlayerEquipmentPanel/RightFrameLayoutGroup/HandsContainer)")]
    [SerializeField] GameObject handsContainer;
    [Tooltip("Set to the object for the player's equipped legs container (CharacterMenuPanel/PlayerEquipmentPanel/RightFrameLayoutGroup/LegsContainer)")]
    [SerializeField] GameObject legsContainer;
    [Tooltip("Set to the object for the player's equipped feet container (CharacterMenuPanel/PlayerEquipmentPanel/RightFrameLayoutGroup/FeetContainer)")]
    [SerializeField] GameObject feetContainer;
    [Tooltip("Set to the object for the player's equipped amulet container (CharacterMenuPanel/PlayerEquipmentPanel/LeftFrameLayoutGroup/AmuletContainer)")]
    [SerializeField] GameObject amuletContainer;
    [Tooltip("Set to the object for the player's equipped ring one container (CharacterMenuPanel/PlayerEquipmentPanel/LeftFrameLayoutGroup/RingOneContainer)")]
    [SerializeField] GameObject ringOneContainer;
    [Tooltip("Set to the object for the player's equipped ring two container (CharacterMenuPanel/PlayerEquipmentPanel/LeftFrameLayoutGroup/RingTwoContainer)")]
    [SerializeField] GameObject ringTwoContainer;

    float statMax = 9; // Should be moved - maximum any stat can be set to

    BaseHero player; // Used to gather the player's stats and equipment
    PlayerManager pm; // Used to gather player class

    bool menuOpen; // Set to true when the menu is open

    public bool canOpenMenu; // Set in SceneInfo to determine if menu can be opened (keeps player from opening player menu on main menu)

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canOpenMenu)
        {
            if (!menuOpen)
            {
                menuOpen = true;

                Time.timeScale = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                characterMenuCanvasGroup.alpha = 1;
                characterMenuCanvasGroup.blocksRaycasts = true;
                characterMenuCanvasGroup.interactable = true;

            } else
            {
                menuOpen = false;

                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                characterMenuCanvasGroup.alpha = 0;
                characterMenuCanvasGroup.blocksRaycasts = false;
                characterMenuCanvasGroup.interactable = false;
            }            
        }
    }

    private void Awake()
    {
        if (GameManager.GetGameSet())
        {
            Startup();
        } else
        {
            Debug.Log("Game not yet set! cannot run startup!");
        }
    }

    /// <summary>
    /// Call when creating the game session to set the required vars for script to function
    /// </summary>
    public void Startup()
    {
        pm = FindFirstObjectByType<PlayerManager>();
        player = pm.GetPlayer();

        StartupStatsPanel();
        StartupSilhouette();
        StartupInventory();
    }

    /// <summary>
    /// Clears any previous values from UI elements and sets them with the current values
    /// </summary>
    public void RefreshUI()
    {
        ClearInventoryUI();
        //DrawInventory();

        //DrawEquipment();

        DrawStats();
    }

    /// <summary>
    /// Used to draw player name and stats when script during Startup()
    /// </summary>
    void StartupStatsPanel()
    {
        DrawStats();
    }

    /// <summary>
    /// Displays the player's stat values and stat bars
    /// </summary>
    void DrawStats()
    {
        if (player == null) // Weird webGL build error occurs here as player is still null when this tries to run. Just a bandaid for now.
        {
            Startup();
        }

        // Set value texts
        strValueText.SetText(player.GetStrength().ToString());
        strValueText.text = player.GetStrength().ToString();
        endValueText.text = player.GetEndurance().ToString();
        agiValueText.text = player.GetAgility().ToString();
        dexValueText.text = player.GetDexterity().ToString();
        intValueText.text = player.GetIntelligence().ToString();
        resValueText.text = player.GetFaith().ToString();

        // set fills
        strFillImage.fillAmount = ((float)player.GetStrength() / statMax);
        endFillImage.fillAmount = ((float)player.GetEndurance() / statMax);
        agiFillImage.fillAmount = ((float)player.GetAgility() / statMax);
        dexFillImage.fillAmount = ((float)player.GetDexterity() / statMax);
        intFillImage.fillAmount = ((float)player.GetIntelligence() / statMax);
        resFillImage.fillAmount = ((float)player.GetFaith() / statMax);

        // set armor and magic resist
        armorValueText.text = player.GetArmor().ToString();
        magicResistValueText.text = player.GetMagicResist().ToString();
    }

    /// <summary>
    /// Sets the silhouette image to the sprite for the player class
    /// </summary>
    void StartupSilhouette()
    {
        // set silhouette
        /*switch (pm.playerClass)
        {
            case EnumHandler.PlayerClasses.WARRIOR:
                silhouetteImage.sprite = warriorSilouette;
                break;
            case EnumHandler.PlayerClasses.MAGE:
                silhouetteImage.sprite = mageSilhouette;
                break;
            case EnumHandler.PlayerClasses.ARCHER:
                silhouetteImage.sprite = archerSilhouette;
                break;
        }*/
    }

    /// <summary>
    /// Calls the startup functions for Inventory and Equipment managers, as well as draws the player's inventory into the menu
    /// </summary>
    void StartupInventory()
    {
        //InventoryManager.Startup();
        //EquipmentManager.Startup();

        ClearInventoryUI();
        //DrawInventory();
    }

    /// <summary>
    /// Used to clear out the player's inventory from the player menu
    /// </summary>
    void ClearInventoryUI()
    {

    }
}

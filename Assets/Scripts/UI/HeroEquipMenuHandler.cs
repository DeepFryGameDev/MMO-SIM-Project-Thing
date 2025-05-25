using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Facilitates the functionality of equipping armor and weapons to the hero from the menu
// Directions: Attach to [UI]/HeroEquipCanvas/HeroEquipHolder
// Other notes: 

public class HeroEquipMenuHandler : MonoBehaviour
{    
    [Header("---Base Item Stuff---")]
    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI goldValueText;
    [SerializeField] TextMeshProUGUI weightValueText;

    [Header("---Armor Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI armorClassText;
    [SerializeField] TextMeshProUGUI armorSlotText;
    [SerializeField] TextMeshProUGUI armorBaseArmorText;
    [SerializeField] TextMeshProUGUI armorBaseMagicResistText;

    [SerializeField] TextMeshProUGUI armorStatBonus0Text;
    [SerializeField] TextMeshProUGUI armorStatBonus1Text;
    [SerializeField] TextMeshProUGUI armorStatBonus2Text;

    [SerializeField] CanvasGroup armorDetailsCanvasGroup;
    // ------------

    [Header("---Weapon Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI weaponClassText;
    [SerializeField] TextMeshProUGUI weaponSlotText;

    [SerializeField] TextMeshProUGUI attackDamageText;
    [SerializeField] TextMeshProUGUI attackSpeedText;

    [SerializeField] CanvasGroup handDetailsCanvasGroup;
    // ------------

    [Header("---Shield Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI shieldSlotText;
    [SerializeField] TextMeshProUGUI shieldClassText;
    [SerializeField] TextMeshProUGUI shieldDamageBlockedText;
    [SerializeField] TextMeshProUGUI shieldArmorText;
    [SerializeField] TextMeshProUGUI shieldMagicResistText;

    [SerializeField] CanvasGroup shieldDetailsCanvasGroup;
    // ------------

    [Header("---Ring Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI ringSlotText;

    [SerializeField] CanvasGroup ringDetailsCanvasGroup;

    // ------------

    [Header("---Relic Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI relicSlotText;

    [SerializeField] CanvasGroup relicDetailsCanvasGroup;
    // ------------

    [Header("---Trinket Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI trinketSlotText;

    [SerializeField] CanvasGroup trinketDetailsCanvasGroup;
    // ------------

    [Header("---Equip Buttons---")]
    [SerializeField] HeroEquipButtonHandler headEquipButton;
    [SerializeField] HeroEquipButtonHandler chestEquipButton;
    [SerializeField] HeroEquipButtonHandler handsEquipButton;
    [SerializeField] HeroEquipButtonHandler legsEquipButton;
    [SerializeField] HeroEquipButtonHandler feetEquipButton;
    [SerializeField] HeroEquipButtonHandler ring1EquipButton;
    [SerializeField] HeroEquipButtonHandler ring2EquipButton;
    [SerializeField] HeroEquipButtonHandler relic1EquipButton;
    [SerializeField] HeroEquipButtonHandler relic2EquipButton;
    [SerializeField] HeroEquipButtonHandler trinketEquipButton;
    [SerializeField] HeroEquipButtonHandler mainHandEquipButton;
    [SerializeField] HeroEquipButtonHandler offHandEquipButton;

    [Header("---Other Stuff---")]
    [SerializeField] TextMeshProUGUI debugIDText;
    [SerializeField] Transform equipInventoryTransform;

    Sprite defaultHeadIcon, defaultChestIcon, defaultHandsIcon, defaultLegsIcon, defaultFeetIcon, defaultRingIcon, defaultRelicIcon, defaultTrinketIcon, defaultMainHandIcon, defaultOffHandIcon;

    CanvasGroup equipScrollCanvasGroup;
    Animator equipScrollAnim;

    public Transform GetEquipInventoryTransform() { return equipInventoryTransform; }

    HeroBaseEquipment equipmentClickedInMenu;
    public void SetEquipmentClickedInMenu(HeroBaseEquipment equipment) { equipmentClickedInMenu = equipment; }
    public HeroBaseEquipment GetEquipmentClickedInMenu() { return equipmentClickedInMenu; }

    public static HeroEquipMenuHandler i;

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    void Awake()
    {
        equipScrollCanvasGroup = transform.Find("EquipScroll").GetComponent<CanvasGroup>();
        equipScrollAnim = transform.Find("EquipScroll").GetComponent<Animator>();

        ClearEquipmentDetails();

        SetDefaultIcons();

        i = this;
    }

    /// <summary>
    /// Sets the default icon vars based on what is already set in the inspector
    /// </summary>
    void SetDefaultIcons()
    {
        defaultHeadIcon = headEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;
        defaultChestIcon = chestEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;
        defaultHandsIcon = handsEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;
        defaultLegsIcon = legsEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;
        defaultFeetIcon = feetEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;

        defaultRingIcon = ring1EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;

        defaultRelicIcon = relic1EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;

        defaultTrinketIcon = trinketEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;

        defaultMainHandIcon = mainHandEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;
        defaultOffHandIcon = offHandEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite;
    }

    /// <summary>
    /// Sets the button vars and UI objects to the given hero manager's equipment
    /// </summary>
    /// <param name="heroManager">HeroManager of the hero to have equipment drawn to the UI</param>
    public void GenerateEquippedEquipmentButtons(HeroManager heroManager)
    {
        Debug.Log("Displaying equipment for " + heroManager.Hero().GetName());
        #region Armor

        // gather anything equipped to the hero and set buttons for them in the menu
        if (heroManager.HeroEquipment().GetEquippedHead() != null)
        {
            headEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedHead().icon;
            headEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedHead());
        } else
        {
            headEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultHeadIcon;
            headEquipButton.SetAssignedEquipment(null);
        }

        if (heroManager.HeroEquipment().GetEquippedChest() != null)
        {
            chestEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedChest().icon;
            chestEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedChest());
        }
        else
        {
            chestEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultChestIcon;
            chestEquipButton.SetAssignedEquipment(null);
        }

        if (heroManager.HeroEquipment().GetEquippedHands() != null)
        {
            handsEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedHands().icon;
            handsEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedHands());
        }
        else
        {
            handsEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultHandsIcon;
            handsEquipButton.SetAssignedEquipment(null);
        }

        if (heroManager.HeroEquipment().GetEquippedLegs() != null)
        {
            legsEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedLegs().icon;
            legsEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedLegs());
        }
        else
        {
            legsEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultLegsIcon;
            legsEquipButton.SetAssignedEquipment(null);
        }

        if (heroManager.HeroEquipment().GetEquippedFeet() != null)
        {
            feetEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedFeet().icon;
            feetEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedFeet());
        }
        else
        {
            feetEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultFeetIcon;
            feetEquipButton.SetAssignedEquipment(null);
        }

        #endregion

        #region Rings

        if (heroManager.HeroEquipment().GetEquippedRing1() != null)
        {
            ring1EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedRing1().icon;
            ring1EquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedRing1());
        }
        else
        {
            ring1EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultRingIcon;
            ring1EquipButton.SetAssignedEquipment(null);
        }

        if (heroManager.HeroEquipment().GetEquippedRing2() != null)
        {
            ring2EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedRing2().icon;
            ring2EquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedRing2());
        }
        else
        {
            ring2EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultRingIcon;
            ring2EquipButton.SetAssignedEquipment(null);
        }

        #endregion

        #region Relics

        if (heroManager.HeroEquipment().GetEquippedRelic1() != null)
        {
            relic1EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedRelic1().icon;
            relic1EquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedRelic1());
        }
        else
        {
            relic1EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultRelicIcon;
            relic1EquipButton.SetAssignedEquipment(null);
        }

        if (heroManager.HeroEquipment().GetEquippedRelic2() != null)
        {
            relic2EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedRelic2().icon;
            relic2EquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedRelic2());
        }
        else
        {
            relic2EquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultRelicIcon;
            relic2EquipButton.SetAssignedEquipment(null);
        }

        #endregion

        #region Trinket

        if (heroManager.HeroEquipment().GetEquippedTrinket() != null)
        {
            trinketEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedTrinket().icon;
            trinketEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedTrinket());
        }
        else
        {
            trinketEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultTrinketIcon;
            trinketEquipButton.SetAssignedEquipment(null);
        }

        #endregion

        #region Weapons

        if (heroManager.HeroEquipment().GetEquippedMainHand() != null)
        {
            mainHandEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = heroManager.HeroEquipment().GetEquippedMainHand().icon;
            mainHandEquipButton.SetAssignedEquipment(heroManager.HeroEquipment().GetEquippedMainHand());
        }
        else
        {
            mainHandEquipButton.transform.Find("EquipSlotIcon").GetComponent<Image>().sprite = defaultMainHandIcon;
            mainHandEquipButton.SetAssignedEquipment(null);
        }

        #endregion
    }

    /// <summary>
    /// Draws the given equipment's basic item details to the UI
    /// </summary>
    /// <param name="equipment"></param>
    void DrawBasicEquipDetails(HeroBaseEquipment equipment)
    {
        nameText.SetText(equipment.name);
        nameText.color = UISettings.GetRarityColor(equipment);

        debugIDText.SetText("[" + equipment.ID.ToString() + "]");

        descriptionText.SetText(equipment.description);
        goldValueText.SetText(equipment.goldValue.ToString());
        weightValueText.SetText(equipment.weight.ToString());
    }

    //-----------------------------------
    #region EquipDetails

    #region Armor

    /// <summary>
    /// Draws the given armor's armor specific details to the UI
    /// </summary>
    /// <param name="armorEquip">The armor to have the details drawn to the UI</param>
    public void DrawArmorEquipmentDetails(ArmorEquipment armorEquip)
    {
        armorDetailsCanvasGroup.alpha = 1;

        DrawBasicEquipDetails(armorEquip);

        armorClassText.SetText(UITasks.CapitalizeFirstLetter(armorEquip.armorClass.ToString()));
        armorSlotText.SetText(UITasks.CapitalizeFirstLetter(armorEquip.equipSlot.ToString()));

        armorBaseArmorText.SetText(armorEquip.baseArmorValue.ToString());
        armorBaseMagicResistText.SetText(armorEquip.baseMagicResistValue.ToString());

        SetArmorBonusTexts(armorEquip);
    }

    /// <summary>
    /// Draws the given armor's stat bonus texts to the UI.  These are unique to armor pieces.
    /// </summary>
    /// <param name="armorEquip">The armor to have the stat bonuses drawn to the UI</param>
    void SetArmorBonusTexts(ArmorEquipment armorEquip)
    {
        int index = 0;

        if (armorEquip.strengthBonus > 0)
        {
            armorStatBonus0Text.SetText("STR: " + armorEquip.strengthBonus.ToString());
            index++;
        }

        if (armorEquip.enduranceBonus > 0)
        {
            switch (index)
            {
                case 0:
                    armorStatBonus0Text.SetText("END: " + armorEquip.enduranceBonus.ToString());
                    break;
                case 1:
                    armorStatBonus1Text.SetText("END: " + armorEquip.enduranceBonus.ToString());
                    break;
            }
            index++;
        }

        if (armorEquip.agilityBonus > 0)
        {
            switch (index)
            {
                case 0:
                    armorStatBonus0Text.SetText("AGI: " + armorEquip.agilityBonus.ToString());
                    break;
                case 1:
                    armorStatBonus1Text.SetText("AGI: " + armorEquip.agilityBonus.ToString());
                    break;
                case 2:
                    armorStatBonus2Text.SetText("AGI: " + armorEquip.agilityBonus.ToString());
                    break;
            }
            index++;
        }

        if (armorEquip.dexterityBonus > 0)
        {
            switch (index)
            {
                case 0:
                    armorStatBonus0Text.SetText("DEX: " + armorEquip.dexterityBonus.ToString());
                    break;
                case 1:
                    armorStatBonus1Text.SetText("DEX: " + armorEquip.dexterityBonus.ToString());
                    break;
                case 2:
                    armorStatBonus2Text.SetText("DEX: " + armorEquip.dexterityBonus.ToString());
                    break;
            }
            index++;
        }

        if (armorEquip.intelligenceBonus > 0)
        {
            switch (index)
            {
                case 0:
                    armorStatBonus0Text.SetText("INT: " + armorEquip.intelligenceBonus.ToString());
                    break;
                case 1:
                    armorStatBonus1Text.SetText("INT: " + armorEquip.intelligenceBonus.ToString());
                    break;
                case 2:
                    armorStatBonus2Text.SetText("INT: " + armorEquip.intelligenceBonus.ToString());
                    break;
            }
            index++;
        }

        if (armorEquip.faithBonus > 0)
        {
            switch (index)
            {
                case 0:
                    armorStatBonus0Text.SetText("FTH: " + armorEquip.faithBonus.ToString());
                    break;
                case 1:
                    armorStatBonus1Text.SetText("FTH: " + armorEquip.faithBonus.ToString());
                    break;
                case 2:
                    armorStatBonus2Text.SetText("FTH: " + armorEquip.faithBonus.ToString());
                    break;
            }
            index++;
        }
    }

    #endregion

    #region Hands

    /// <summary>
    /// Draws the given weapon specific details to the UI
    /// </summary>
    /// <param name="weaponEquip">The weapon to have the details drawn to the UI</param>
    public void DrawWeaponEquipmentDetails(WeaponEquipment weaponEquip)
    {
        handDetailsCanvasGroup.alpha = 1;

        DrawBasicEquipDetails(weaponEquip);

        weaponSlotText.SetText(UITasks.CapitalizeFirstLetter(weaponEquip.equipSlot.ToString()));
        weaponClassText.SetText(UITasks.CapitalizeFirstLetter(weaponEquip.weaponClass.ToString()));

        attackDamageText.SetText(weaponEquip.attackDamage.ToString());
        attackSpeedText.SetText(weaponEquip.attackSpeed.ToString());
    }

    #endregion

    #region Shields

    /// <summary>
    /// Draws the given shield specific details to the UI
    /// </summary>
    /// <param name="shieldEquip">The shield to have the details drawn to the UI</param>
    public void DrawShieldEquipmentDetails(ShieldEquipment shieldEquip)
    {
        shieldDetailsCanvasGroup.alpha = 1;

        DrawBasicEquipDetails(shieldEquip);

        shieldSlotText.SetText(UITasks.CapitalizeFirstLetter(shieldEquip.equipSlot.ToString()));
        shieldClassText.SetText(UITasks.CapitalizeFirstLetter(shieldEquip.shieldClass.ToString()));

        shieldDamageBlockedText.SetText(shieldEquip.damageBlocked.ToString());
        shieldArmorText.SetText(shieldEquip.baseArmorValue.ToString());
        shieldMagicResistText.SetText(shieldEquip.baseMagicResistValue.ToString());
    }

    #endregion

    #region Rings

    /// <summary>
    /// Draws the given ring specific details to the UI
    /// </summary>
    /// <param name="ringEquip">The ring to have the details drawn to the UI</param>
    public void DrawRingEquipmentDetails(RingEquipment ringEquip)
    {
        ringDetailsCanvasGroup.alpha = 1;

        DrawBasicEquipDetails(ringEquip);

        ringSlotText.SetText("Ring");
    }

    #endregion

    #region Relics

    /// <summary>
    /// Draws the given relic specific details to the UI
    /// </summary>
    /// <param name="relicEquip">The relic to have the details drawn to the UI</param>
    public void DrawRelicEquipmentDetails(RelicEquipment relicEquip)
    {
        relicDetailsCanvasGroup.alpha = 1;

        DrawBasicEquipDetails(relicEquip);

        relicSlotText.SetText("Relic");
    }

    #endregion

    #region Trinkets

    /// <summary>
    /// Draws the given trinket specific details to the UI
    /// </summary>
    /// <param name="trinketEquip">The trinket to have the details drawn to the UI</param>
    public void DrawTrinketEquipmentDetails(TrinketEquipment trinketEquip)
    {
        trinketDetailsCanvasGroup.alpha = 1;

        DrawBasicEquipDetails(trinketEquip);

        trinketSlotText.SetText("Trinket");
    }

    #endregion

    #endregion
    //-----------------------------------

    /// <summary>
    /// Shows or hides the equipment scroll menu (shown when the user clicks on a slot and the list of available equipment should be displayed)
    /// </summary>
    /// <param name="toggle">True to display the scroll, false to hide it</param>
    public void ToggleEquipScroll(bool toggle)
    {
        if (toggle) // equip scroll should be showing
        {
            equipScrollAnim.SetBool("toggleOn", true);

            // enable raycasting on equipScrollCanvasGroup
            equipScrollCanvasGroup.interactable = true;
            equipScrollCanvasGroup.blocksRaycasts = true;


        }
        else // hide equip scroll
        {
            equipScrollAnim.SetBool("toggleOn", false);

            // disable raycasting on equipScrollCanvasGroup
            equipScrollCanvasGroup.interactable = false;
            equipScrollCanvasGroup.blocksRaycasts = false;
        }
    }

    /// <summary>
    /// Just clears the inventory list and hides the equipment scroll
    /// </summary>
    public void CloseEquipScroll()
    {
        ToggleEquipScroll(false);
        ClearInventoryList();
    }

    /// <summary>
    /// Called when the user clicks the 'Close' button in the UI
    /// Assigned to: [UI]/HeroEquipCanvas/HeroEquipHolder/EquipDetailsPanel/CloseButton.OnClick()
    /// </summary>
    public void CloseEquipMenu()
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

        CloseEquipScroll();
    }

    /// <summary>
    /// Sets all the text objects on the UI to an empty string and hides their canvas groups
    /// </summary>
    public void ClearEquipmentDetails()
    {
        nameText.SetText(string.Empty);
        debugIDText.SetText(string.Empty);

        descriptionText.SetText(string.Empty);
        goldValueText.SetText(string.Empty);
        weightValueText.SetText(string.Empty);

        // armor
        if (armorDetailsCanvasGroup.alpha != 0)
        {
            armorDetailsCanvasGroup.alpha = 0;
            
            armorClassText.SetText(string.Empty);
            armorSlotText.SetText(string.Empty);
            armorBaseArmorText.SetText(string.Empty);
            armorBaseMagicResistText.SetText(string.Empty);

            armorStatBonus0Text.SetText(string.Empty);
            armorStatBonus1Text.SetText(string.Empty);
            armorStatBonus2Text.SetText(string.Empty);
        }

        // weapons
        if (handDetailsCanvasGroup.alpha != 0)
        {
            handDetailsCanvasGroup.alpha = 0;
            
            weaponSlotText.SetText(string.Empty);
            weaponClassText.SetText(string.Empty);

            attackDamageText.SetText(string.Empty);
            attackSpeedText.SetText(string.Empty);
        }
        
        // shield
        if (shieldDetailsCanvasGroup.alpha != 0)
        {
            shieldDetailsCanvasGroup.alpha = 0;

            shieldSlotText.SetText(string.Empty);
            shieldClassText.SetText(string.Empty);

            shieldDamageBlockedText.SetText(string.Empty);
            shieldArmorText.SetText(string.Empty);
            shieldMagicResistText.SetText(string.Empty);
        }

        // Ring
        if (ringDetailsCanvasGroup.alpha != 0)
        {
            ringDetailsCanvasGroup.alpha = 0;

            ringSlotText.SetText(string.Empty);
        }

        // Relic
        if (relicDetailsCanvasGroup.alpha != 0)
        {
            relicDetailsCanvasGroup.alpha = 0;

            relicSlotText.SetText(string.Empty);
        }

        // Trinket
        if (trinketDetailsCanvasGroup.alpha != 0)
        {
            trinketDetailsCanvasGroup.alpha = 0;

            trinketSlotText.SetText(string.Empty);
        }
    }

    /// <summary>
    /// Clears out the inventory list by destroying the instantiated objects
    /// </summary>
    public void ClearInventoryList()
    {
        foreach (Transform transform in equipInventoryTransform)
        {
            Destroy(transform.gameObject);
        }
    }
}

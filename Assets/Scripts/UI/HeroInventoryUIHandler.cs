using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Purpose: Handles updating the UI with inventory details when the menu is opened and closed.
// Directions: Attach to [UI]/HeroInventoryCanvas
// Other notes: 

public class HeroInventoryUIHandler : MonoBehaviour
{
    [Header("---Hero Stuff---")]
    [SerializeField] TextMeshProUGUI heroWeightText;

    [Header("---Base Item Stuff---")]
    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI goldValueText;
    [SerializeField] TextMeshProUGUI weightValueText;

    [Header("---Training Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI trainingEquipLevelText;
    [SerializeField] TextMeshProUGUI trainingEquipTypeText;

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

    [Header("---Other Stuff---")]
    [SerializeField] TextMeshProUGUI debugIDText;

    [SerializeField] Transform layoutGroupTransform;
    [SerializeField] CanvasGroup trainingEquipDetailsCanvasGroup;

    // ---------

    List<HeroItem> itemsAccountedFor = new List<HeroItem>();

    public static HeroInventoryUIHandler i;

    private void Awake()
    {
        i = this;
    }

    /// <summary>
    /// Any hero specific details should go here.  For now it's just carry weight.
    /// Used to set the total item weight for the player (combined items in inventory weight/maximum carry weight).
    /// </summary>
    /// <param name="heroManager">HeroManager of the hero to set details</param>

    public void SetHeroDetailsPanel(HeroManager heroManager)
    {
        heroWeightText.text = "999/999"; // will set later
    }

    /// <summary>
    /// Generates the inventory UI by looping through each of the items in the hero's inventory and instantiating a button for it.
    /// </summary>
    /// <param name="heroManager"></param>
    public void GenerateInventory(HeroManager heroManager)
    {
        foreach (HeroItem item in heroManager.HeroInventory().GetInventory())
        {
            if (!itemsAccountedFor.Contains(item))
            {
                // instantiate button object in layoutGroupTransform
                GameObject newItemObject = Instantiate(PrefabManager.i.HeroInventoryButton, layoutGroupTransform);

                HeroInventoryButtonHandler heroInventoryButtonHandler = newItemObject.GetComponent<HeroInventoryButtonHandler>();

                heroInventoryButtonHandler.SetHandler(this);

                heroInventoryButtonHandler.SetItemIcon(item.icon);

                // if count > 1
                int itemCount = heroManager.HeroInventory().GetInventory().Count;
                if (itemCount > 1)
                {
                    heroInventoryButtonHandler.SetCountText(itemCount.ToString());
                }
                else // if count == 1, hide the countText
                {
                    heroInventoryButtonHandler.HideCountBG();
                }          

                heroInventoryButtonHandler.SetItem(item);

                itemsAccountedFor.Add(item);
            }            
        }
    }

    #region Base Item Details
    /// <summary>
    /// Sets the base item text objects in the UI with the given item's details
    /// </summary>
    /// <param name="item">Item to draw the UI for</param>
    public void DrawBaseItemDetails(HeroItem item)
    {
        nameText.SetText(item.name);
        nameText.color = UISettings.GetRarityColor(item);

        debugIDText.SetText("[" + item.ID.ToString() + "]");

        descriptionText.SetText(item.description);

        goldValueText.SetText(item.goldValue.ToString());

    }

    #endregion

    #region Training Equipment

    /// <summary>
    /// Sets the Training Equipment text objects in the UI with the given item's details
    /// </summary>
    /// <param name="trainingEquipment">Training Equipment to draw the UI for</param>
    public void DrawTrainingEquipUI(TrainingEquipment trainingEquipment)
    {
        DrawBaseItemDetails(trainingEquipment);

        trainingEquipDetailsCanvasGroup.alpha = 1;

        trainingEquipLevelText.SetText("Training Level: " + trainingEquipment.trainingLevel.ToString());
        trainingEquipTypeText.SetText(UITasks.CapitalizeFirstLetter(trainingEquipment.trainingType.ToString()));        
    }

    #endregion

    //-----------------------------------
    #region EquipDetails

    #region Armor

    /// <summary>
    /// Sets the Armor text objects in the UI with the given item's details
    /// </summary>
    /// <param name="armorEquip">Armor to draw the UI For</param>
    public void DrawArmorEquipmentDetails(ArmorEquipment armorEquip)
    {
        armorDetailsCanvasGroup.alpha = 1;

        DrawBaseItemDetails(armorEquip);

        armorClassText.SetText(UITasks.CapitalizeFirstLetter(armorEquip.armorClass.ToString()));
        armorSlotText.SetText(UITasks.CapitalizeFirstLetter(armorEquip.equipSlot.ToString()));

        armorBaseArmorText.SetText(armorEquip.baseArmorValue.ToString());
        armorBaseMagicResistText.SetText(armorEquip.baseMagicResistValue.ToString());

        SetArmorBonusTexts(armorEquip);
    }

    /// <summary>
    /// Sets the armor bonus text vals with the given armor's details
    /// </summary>
    /// <param name="armorEquip">Armor to draw the UI for</param>
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
    /// Sets the weapon text vals with the given weapon's details
    /// </summary>
    /// <param name="weaponEquip">Weapon to draw the UI for</param>
    public void DrawWeaponEquipmentDetails(WeaponEquipment weaponEquip)
    {
        handDetailsCanvasGroup.alpha = 1;

        DrawBaseItemDetails(weaponEquip);

        weaponSlotText.SetText(UITasks.CapitalizeFirstLetter(weaponEquip.equipSlot.ToString()));
        weaponClassText.SetText(UITasks.CapitalizeFirstLetter(weaponEquip.weaponClass.ToString()));

        attackDamageText.SetText(weaponEquip.attackDamage.ToString());
        attackSpeedText.SetText(weaponEquip.attackSpeed.ToString());
    }

    #endregion

    #region Shields

    /// <summary>
    /// Sets the shield text vals with the given shield's details
    /// </summary>
    /// <param name="shieldEquip">Shield to draw the UI for</param>
    public void DrawShieldEquipmentDetails(ShieldEquipment shieldEquip)
    {
        shieldDetailsCanvasGroup.alpha = 1;

        DrawBaseItemDetails(shieldEquip);

        shieldSlotText.SetText(UITasks.CapitalizeFirstLetter(shieldEquip.equipSlot.ToString()));
        shieldClassText.SetText(UITasks.CapitalizeFirstLetter(shieldEquip.shieldClass.ToString()));

        shieldDamageBlockedText.SetText(shieldEquip.damageBlocked.ToString());
        shieldArmorText.SetText(shieldEquip.baseArmorValue.ToString());
        shieldMagicResistText.SetText(shieldEquip.baseMagicResistValue.ToString());
    }

    #endregion

    #region Rings

    /// <summary>
    /// Sets the ring text vals with the given ring's details
    /// </summary>
    /// <param name="ringEquip">Ring to draw the UI for</param>
    public void DrawRingEquipmentDetails(RingEquipment ringEquip)
    {
        ringDetailsCanvasGroup.alpha = 1;

        DrawBaseItemDetails(ringEquip);

        ringSlotText.SetText("Ring");
    }

    #endregion

    #region Relics

    /// <summary>
    /// Sets the relic text vals with the given relic's details
    /// </summary>
    /// <param name="relicEquip">Relic to draw the UI for</param>
    public void DrawRelicEquipmentDetails(RelicEquipment relicEquip)
    {
        relicDetailsCanvasGroup.alpha = 1;

        DrawBaseItemDetails(relicEquip);

        relicSlotText.SetText("Relic");
    }

    #endregion

    #region Trinkets

    /// <summary>
    /// Sets the trinket text vals with the given trinket's details
    /// </summary>
    /// <param name="trinketEquip">Trinket to draw the UI for</param>
    public void DrawTrinketEquipmentDetails(TrinketEquipment trinketEquip)
    {
        trinketDetailsCanvasGroup.alpha = 1;

        DrawBaseItemDetails(trinketEquip);

        trinketSlotText.SetText("Trinket");
    }

    #endregion

    #endregion
    //-----------------------------------

    #region Cleanup

    /// <summary>
    /// Sets all base item text vals to empty string
    /// </summary>
    void ClearItemDetails()
    {
        nameText.SetText(string.Empty);
        debugIDText.SetText(string.Empty);

        descriptionText.SetText(string.Empty);
        goldValueText.SetText(string.Empty);
        weightValueText.SetText(string.Empty);
    }

    /// <summary>
    /// Sets all training equipment text vals to empty string
    /// </summary>
    void ClearTrainingEquipmentDetails()
    {
        trainingEquipLevelText.SetText(string.Empty);
        trainingEquipTypeText.SetText(string.Empty);
    }

    /// <summary>
    /// Sets all equipment text vals to empty string
    /// </summary>
    void ClearEquipmentDetails()
    {
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

    #endregion

    /// <summary>
    /// Clears all text vals in the UI
    /// </summary>
    public void ResetUI()
    {
        ClearItemDetails();

        ClearTrainingEquipmentDetails();

        ClearEquipmentDetails();
    }

    /// <summary>
    /// Resets private vars and destroys all instanitated item objects in the inventory list
    /// </summary>
    public void ClearUI()
    {
        itemsAccountedFor.Clear();

        foreach (Transform transform in layoutGroupTransform)
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Called when the user clicks the "Close" button in the inventory menu.  Just goes back to the root menu.
    /// </summary>
    public void OnCloseButtonClick()
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

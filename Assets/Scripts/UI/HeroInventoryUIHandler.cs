using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroInventoryUIHandler : MonoBehaviour
{
    // make serialized fields for all item objects in inventory canvas and fill them in
    [Header("---Hero Stuff---")]
    [SerializeField] TextMeshProUGUI weightText;

    [Header("---Item Stuff---")]
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;
    [SerializeField] TextMeshProUGUI itemGoldValueText;
    [SerializeField] TextMeshProUGUI itemWeightValueText;    

    [Header("---Armor Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI armorEquipSlotText;
    [SerializeField] TextMeshProUGUI armorEquipClassText;
    [SerializeField] TextMeshProUGUI armorBaseArmorText;
    [SerializeField] TextMeshProUGUI armorBaseMagicResistText;

    [Header("---Shield Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI shieldSlotText;
    [SerializeField] TextMeshProUGUI shieldTypeText;
    [SerializeField] TextMeshProUGUI shieldDamageBlockedText;
    [SerializeField] TextMeshProUGUI shieldBaseArmorText;
    [SerializeField] TextMeshProUGUI shieldBaseMagicResistText;

    [Header("---Hand Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI handEquipSlotText;
    [SerializeField] TextMeshProUGUI handEquipTypeText;
    [SerializeField] TextMeshProUGUI handEquipAttackDamageText;
    [SerializeField] TextMeshProUGUI handEquipAttackSpeedText;

    [Header("---Jewlery Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI jewelryEquipSlotText;    

    [Header("---Training Equipment Stuff---")]
    [SerializeField] TextMeshProUGUI trainingEquipLevelText;
    [SerializeField] TextMeshProUGUI trainingEquipTypeText;

    [Header("---Other Stuff---")]
    // serialize the layout group transform for instantiating buttons
    [SerializeField] TextMeshProUGUI debugIDText;

    [SerializeField] Transform layoutGroupTransform;
    [SerializeField] CanvasGroup trainingEquipDetailsCanvasGroup;
    [SerializeField] CanvasGroup equipmentHandDetailsCanvasGroup;
    [SerializeField] CanvasGroup equipmentArmorDetailsCanvasGroup;
    [SerializeField] CanvasGroup shieldDetailsCanvasGroup;

    // ---------

    List<HeroItem> itemsAccountedFor = new List<HeroItem>();


    /// <summary>
    ///
    /// </summary>
    /// <param name="heroManager"></param>

    public void SetHeroDetailsPanel(HeroManager heroManager)
    {
        weightText.text = "999/999"; // will set later
    }

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
                int itemCount = GetCountInInventory(item, heroManager);
                if (itemCount > 1)
                {
                    heroInventoryButtonHandler.SetCountText(GetCountInInventory(item, heroManager).ToString());
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

    public void DrawBaseItemUI(HeroItem item)
    {
        itemNameText.color = GetRarityColor(item);
        itemNameText.SetText(item.name);

        debugIDText.SetText("[" + item.ID.ToString() + "]");

        itemDescriptionText.SetText(item.description);

        itemGoldValueText.SetText(item.goldValue.ToString());

    }

    Color GetRarityColor(HeroItem item)
    {
        switch (item.rarity)
        {
            case EnumHandler.InventoryRarities.JUNK:
                return UISettings.junkItemColor;
            case EnumHandler.InventoryRarities.COMMON:
                return UISettings.commonItemColor;
            case EnumHandler.InventoryRarities.UNCOMMON:
                return UISettings.uncommonItemColor;
            case EnumHandler.InventoryRarities.RARE:
                return UISettings.rareItemColor;
            case EnumHandler.InventoryRarities.EPIC:
                return UISettings.epicItemColor;
            case EnumHandler.InventoryRarities.LEGENDARY:
                return UISettings.legendaryItemColor;
            default:
                return new Color(0, 0, 0, 0);
        }
    }

    public void DrawTrainingEquipUI(TrainingEquipment trainingEquipment)
    {
        trainingEquipLevelText.SetText("Training Level: " + trainingEquipment.trainingLevel.ToString());
        trainingEquipTypeText.SetText(UITasks.CapitalizeFirstLetter(trainingEquipment.trainingType.ToString()));

        trainingEquipDetailsCanvasGroup.alpha = 1;
    }

    public void DrawEquipmentWeaponUI(WeaponEquipment weaponEquipment)
    {
        handEquipSlotText.SetText(UITasks.CapitalizeFirstLetter(weaponEquipment.equipSlot.ToString()));
        handEquipTypeText.SetText(UITasks.CapitalizeFirstLetter(weaponEquipment.weaponClass.ToString()));

        handEquipAttackDamageText.SetText(weaponEquipment.attackDamage.ToString());
        handEquipAttackSpeedText.SetText(weaponEquipment.attackSpeed.ToString());

        equipmentHandDetailsCanvasGroup.alpha = 1;
    }

    public void DrawEquipmentShieldUI(ShieldEquipment shieldEquipment)
    {
        shieldSlotText.SetText(UITasks.CapitalizeFirstLetter(shieldEquipment.equipSlot.ToString()));
        shieldTypeText.SetText(UITasks.CapitalizeFirstLetter(shieldEquipment.shieldClass.ToString()));

        shieldBaseArmorText.SetText(shieldEquipment.baseArmorValue.ToString());
        shieldBaseMagicResistText.SetText(shieldEquipment.baseMagicResistValue.ToString());

        shieldDamageBlockedText.SetText(shieldEquipment.damageBlocked.ToString());

        shieldDetailsCanvasGroup.alpha = 1;
    }

    public void DrawEquipmentArmorUI(ArmorEquipment armorEquipment)
    {
        armorEquipSlotText.SetText(UITasks.CapitalizeFirstLetter(armorEquipment.equipSlot.ToString()));
        armorEquipClassText.SetText(UITasks.CapitalizeFirstLetter(armorEquipment.armorClass.ToString()));

        armorBaseArmorText.SetText(armorEquipment.baseArmorValue.ToString());
        armorBaseMagicResistText.SetText(armorEquipment.baseMagicResistValue.ToString());

        equipmentArmorDetailsCanvasGroup.alpha = 1;
    }

    public void DrawEquipmentJewelryUI(HeroEquipment heroEquipment)
    {
        
    }

    int GetCountInInventory(BaseItem item, HeroManager heroManager)
    {
        int count = 0;

        foreach (BaseItem baseItem in heroManager.HeroInventory().GetInventory())
        {
            if (baseItem.ID == item.ID)
            {
                count++;
            }
        }

        return count;
    }

    public void ResetUI()
    {
        weightText.SetText(string.Empty);

        itemNameText.SetText(string.Empty);
        itemDescriptionText.SetText(string.Empty);
        itemGoldValueText.SetText(string.Empty);
        itemWeightValueText.SetText(string.Empty);
        debugIDText.SetText(string.Empty);

        trainingEquipDetailsCanvasGroup.alpha = 0;
        trainingEquipLevelText.SetText(string.Empty);
        trainingEquipTypeText.SetText(string.Empty);

        equipmentHandDetailsCanvasGroup.alpha = 0;
        handEquipAttackDamageText.SetText(string.Empty);
        handEquipAttackSpeedText.SetText(string.Empty);
        handEquipSlotText.SetText(string.Empty);
        handEquipTypeText.SetText(string.Empty);

        equipmentArmorDetailsCanvasGroup.alpha = 0;
        armorBaseArmorText.SetText(string.Empty);
        armorBaseMagicResistText.SetText(string.Empty);
        armorEquipClassText.SetText(string.Empty);
        armorEquipSlotText.SetText(string.Empty);

        shieldDetailsCanvasGroup.alpha = 0;
        shieldTypeText.SetText(string.Empty);
        shieldSlotText.SetText(string.Empty);
        shieldBaseArmorText.SetText(string.Empty);
        shieldBaseMagicResistText.SetText(string.Empty);
        shieldDamageBlockedText.SetText(string.Empty);
    }

    public void ClearUI()
    {
        itemsAccountedFor.Clear();

        foreach (Transform transform in layoutGroupTransform)
        {
            Destroy(transform.gameObject);
        }
    }
}

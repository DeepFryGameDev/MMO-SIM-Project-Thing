using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroEquipMenuHandler : MonoBehaviour
{    
    CanvasGroup equipScrollCanvasGroup;
    Animator equipScrollAnim;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI debugIDText;

    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI goldValueText;
    [SerializeField] TextMeshProUGUI weightValueText;

    // Armor
    [SerializeField] TextMeshProUGUI armorClassText;
    [SerializeField] TextMeshProUGUI armorSlotText;

    [SerializeField] TextMeshProUGUI armorBaseArmorText;
    [SerializeField] TextMeshProUGUI armorBaseMagicResistText;

    [SerializeField] CanvasGroup armorDetailsCanvasGroup;

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

    Sprite defaultHeadIcon, defaultChestIcon, defaultHandsIcon, defaultLegsIcon, defaultFeetIcon, defaultRingIcon, defaultRelicIcon, defaultTrinketIcon, defaultMainHandIcon, defaultOffHandIcon;

    [SerializeField] Transform equipInventoryTransform;
    public Transform GetEquipInventoryTransform() { return equipInventoryTransform; }

    HeroBaseEquipment equipmentClickedInMenu;
    public void SetEquipmentClickedInMenu(HeroBaseEquipment equipment) { equipmentClickedInMenu = equipment; }
    public HeroBaseEquipment GetEquipmentClickedInMenu() { return equipmentClickedInMenu; }

    void Awake()
    {
        equipScrollCanvasGroup = transform.Find("EquipScroll").GetComponent<CanvasGroup>();
        equipScrollAnim = transform.Find("EquipScroll").GetComponent<Animator>();

        ClearEquipmentDetails();

        SetDefaultIcons();
    }

    private void SetDefaultIcons()
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

    public void GenerateEquippedEquipmentButtons(HeroManager heroManager)
    {
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

        // Add for the rest of the slots

    }

    public void ClearInventoryList()
    {
        foreach (Transform transform in equipInventoryTransform)
        {
            Destroy(transform.gameObject);
        }
    }

    public void ToggleEquipScroll(bool toggle)
    {
        if (toggle) // equip scroll should be showing
        {
            equipScrollAnim.SetBool("toggleOn", true);

            // enable raycasting on equipScrollCanvasGroup
            equipScrollCanvasGroup.interactable = true;
            equipScrollCanvasGroup.blocksRaycasts = true;


        } else // hide equip scroll
        {
            equipScrollAnim.SetBool("toggleOn", false);

            // disable raycasting on equipScrollCanvasGroup
            equipScrollCanvasGroup.interactable = false;
            equipScrollCanvasGroup.blocksRaycasts = false;
        }
    }

    void showBasicEquipDetails(HeroBaseEquipment equipment)
    {
        nameText.SetText(equipment.name);
        debugIDText.SetText("[" + equipment.ID.ToString() + "]");

        descriptionText.SetText(equipment.description);
        goldValueText.SetText(equipment.goldValue.ToString());
        weightValueText.SetText(equipment.weight.ToString());
    }

    public void ShowArmorEquipmentDetails(ArmorEquipment armorEquip)
    {
        armorDetailsCanvasGroup.alpha = 1;

        showBasicEquipDetails(armorEquip);

        armorClassText.SetText(UITasks.CapitalizeFirstLetter(armorEquip.armorClass.ToString()));
        armorSlotText.SetText(UITasks.CapitalizeFirstLetter(armorEquip.equipSlot.ToString()));

        armorBaseArmorText.SetText(armorEquip.baseArmorValue.ToString());
        armorBaseMagicResistText.SetText(armorEquip.baseMagicResistValue.ToString());
    }

    public void ClearEquipmentDetails()
    {
        armorDetailsCanvasGroup.alpha =  0;

        nameText.SetText(string.Empty);
        debugIDText.SetText(string.Empty);

        descriptionText.SetText(string.Empty);
        goldValueText.SetText(string.Empty);
        weightValueText.SetText(string.Empty);

        // armor
        armorClassText.SetText(string.Empty);
        armorSlotText.SetText(string.Empty);
        armorBaseArmorText.SetText(string.Empty);
        armorBaseMagicResistText.SetText(string.Empty);
    }
}

using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class TrainingEquipmentMenu : MonoBehaviour
{
    VerticalLayoutGroup equipmentButtonGroup;
    GridLayoutGroup listButtonGroup;

    TrainingEquipmentEquippedButtonHandler clickedEquippedTrainingButton;
    public TrainingEquipmentEquippedButtonHandler GetClickedEquippedTrainingButton() { return clickedEquippedTrainingButton; }
    public void SetClickedEquippedTrainingButtonHandler(TrainingEquipmentEquippedButtonHandler trainingEquipmentEquippedButtonHandler) { this.clickedEquippedTrainingButton = trainingEquipmentEquippedButtonHandler; }

    TrainingEquipmentListButtonHandler clickedListTrainingButton;
    public TrainingEquipmentListButtonHandler GetClickedListTrainingButton() { return clickedListTrainingButton; }
    public void SetTrainingEquipmentListButtonHandler(TrainingEquipmentListButtonHandler trainingEquipmentListButtonHandler) { this.clickedListTrainingButton = trainingEquipmentListButtonHandler; }

    private void Awake()
    {
        equipmentButtonGroup = GameObject.Find("HeroZoneCanvas/TrainingEquipmentMenu/MenuHolder/EquipmentButtonGroup").GetComponent<VerticalLayoutGroup>();
        listButtonGroup = GameObject.Find("HeroZoneCanvas/TrainingEquipmentMenu/EquipmentListHolder/ListButtonGroup").GetComponent<GridLayoutGroup>();
    }

    public void InstantiateEquipmentSlots(HeroManager heroManager)
    {
        ClearEquipmentSlots(); // should be run when closing equipment slots

        for (int i=0; i < heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlots(); i++)
        {
            GameObject newEquipmentSlot = Instantiate(PrefabManager.i.TrainingEquipmentButton, equipmentButtonGroup.transform); // this will need to be tweaked - blank slots for now.
            TrainingEquipmentEquippedButtonHandler teebh = newEquipmentSlot.GetComponent<TrainingEquipmentEquippedButtonHandler>();

            teebh.SetTrainingEquipMenu(this);
            teebh.SetHeroManager(heroManager);

            teebh.SetEquipSlot(i);

            if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(i) != null) // something is equipped in this slot already
            {
                teebh.SetIcon(heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(i).icon);
                teebh.SetLevelText(heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(i).trainingLevel);
            } else // set blank icons
            {
                teebh.SetIcon(null);
                teebh.SetLevelText(0);
            }
        }
    }

    public void InstantiateEquipmentInventoryList(HeroManager heroManager)
    {
        ClearEquipmentInventoryList(); // should be run when closing inventory list

        // instantiate a blank one for unequipping
        GameObject blankButton = Instantiate(PrefabManager.i.TrainingEquipmentListButton, listButtonGroup.transform);
        TrainingEquipmentListButtonHandler blankButtonHandler = blankButton.GetComponent<TrainingEquipmentListButtonHandler>();

        blankButtonHandler.SetTrainingEquipmentMenu(this);
        blankButtonHandler.SetHeroManager(heroManager);

        blankButtonHandler.SetTrainingEquipment(null);

        // set icon, etc. tooltip will be needed eventually
        blankButtonHandler.SetIcon(null);
        blankButtonHandler.SetLevelText(0);


        for (int i = 0; i < heroManager.HeroInventory().GetInventory().Count; i++)
        {
            GameObject newListButton = Instantiate(PrefabManager.i.TrainingEquipmentListButton, listButtonGroup.transform);
            TrainingEquipmentListButtonHandler telbh = newListButton.GetComponent<TrainingEquipmentListButtonHandler>();

            telbh.SetTrainingEquipmentMenu(this);
            telbh.SetHeroManager(heroManager);

            TrainingEquipment itemAsTrainingEquip = heroManager.HeroInventory().GetInventory()[i] as TrainingEquipment;

            telbh.SetTrainingEquipment(itemAsTrainingEquip);

            // set icon, etc. tooltip will be needed eventually
            telbh.SetIcon(itemAsTrainingEquip.icon);
            telbh.SetLevelText(itemAsTrainingEquip.trainingLevel);
        }
    }

    void ClearEquipmentSlots()
    {
        foreach (Transform transform in equipmentButtonGroup.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    void ClearEquipmentInventoryList()
    {
        foreach (Transform transform in listButtonGroup.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    public void OnBackClick()
    {
        if (MenuProcessingHandler.i.GetHeroCommandMenuState() == EnumHandler.HeroCommandMenuStates.TRAININGEQUIPLIST)
        {
            // hide the equip list
            MenuProcessingHandler.i.TransitionToMenu(MenuProcessingHandler.i.GetTrainingEquipmentMenuCanvasGroup(), true);
        }

        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.ROOT);
    }
}

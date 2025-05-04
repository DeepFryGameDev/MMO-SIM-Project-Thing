using UnityEngine;
using UnityEngine.UI;

// Purpose: Facilitates all procedures to be run within the Training Equipment Menu
// Directions: Attach to '[UI]/HeroZoneCanvas/TrainingEquipmentMenu'
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

    /// <summary>
    /// When opening the Training Equipment menu, this method will generate the slot buttons the user can click to change the equipped Training Equipment
    /// </summary>
    /// <param name="heroManager">HeroManager of hero to check for already equipped Training Equipment</param>
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

    /// <summary>
    /// When clicking on a Training Equipment button to change the equipment in that slot, this method will be called to generate the inventory list the user can choose from.
    /// </summary>
    /// <param name="heroManager">HeroManager of the hero in question - this checks their inventory and sets the hero manager to each button generated</param>
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
            TrainingEquipment itemAsTrainingEquip = heroManager.HeroInventory().GetInventory()[i] as TrainingEquipment;

            if (itemAsTrainingEquip != null)
            {
                GameObject newListButton = Instantiate(PrefabManager.i.TrainingEquipmentListButton, listButtonGroup.transform);
                TrainingEquipmentListButtonHandler telbh = newListButton.GetComponent<TrainingEquipmentListButtonHandler>();

                telbh.SetTrainingEquipmentMenu(this);
                telbh.SetHeroManager(heroManager);



                telbh.SetTrainingEquipment(itemAsTrainingEquip);

                // set icon, etc. tooltip will be needed eventually
                telbh.SetIcon(itemAsTrainingEquip.icon);
                telbh.SetLevelText(itemAsTrainingEquip.trainingLevel);
            }
        }
    }

    /// <summary>
    /// Simply just clears any existing equipment slots in the UI
    /// </summary>
    void ClearEquipmentSlots()
    {
        foreach (Transform transform in equipmentButtonGroup.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Simply just clears the inventory list in the UI
    /// </summary>
    void ClearEquipmentInventoryList()
    {
        foreach (Transform transform in listButtonGroup.transform)
        {
            Destroy(transform.gameObject);
        }
    }
}

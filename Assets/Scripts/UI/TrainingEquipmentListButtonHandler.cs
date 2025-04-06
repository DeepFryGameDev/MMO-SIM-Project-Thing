using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: Used to manipulate the behavior for buttons in the training equipment list - this is when the user clicks the "TrainingEquipment" button in the hero command menu to display the hero's inventory of training equipment.
// Directions: Just make sure this is attached to the Training Equipment List Button prefab.
// Other notes: 

public class TrainingEquipmentListButtonHandler : MonoBehaviour
{
    TrainingEquipmentMenu trainingEquipmentMenu;
    public void SetTrainingEquipmentMenu(TrainingEquipmentMenu trainingEquipmentMenu) { this.trainingEquipmentMenu = trainingEquipmentMenu; }

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }

    TrainingEquipment trainingEquipment;
    public void SetTrainingEquipment(TrainingEquipment trainingEquipment) { this.trainingEquipment = trainingEquipment; }

    int equipSlot;
    public void SetEquipSlot(int equipSlot) { this.equipSlot = equipSlot; }

    public void SetIcon(Sprite icon) { transform.Find("Icon").GetComponent<Image>().sprite = icon; }
    public void SetLevelText(int level) { transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = level.ToString(); }

    TrainingEquipmentManager trainingEquipmentManager;

    private void Awake()
    {
        trainingEquipmentManager = FindFirstObjectByType<TrainingEquipmentManager>();
    }

    public void OnClick()
    {
        if (trainingEquipment != null) // Clicked on a valid equipment button
        {
            // equip the training equipment
            Debug.Log("Equip the " + trainingEquipment.name + " to " + trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot());
            trainingEquipmentManager.Equip(trainingEquipment, trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot(), heroManager);
        }
        else // Clicked the unequip button
        {
            trainingEquipmentManager.Equip(null, trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot(), heroManager);
        }

        // generate new equipment slots in equip menu
        trainingEquipmentMenu.InstantiateEquipmentSlots(heroManager);

        // Instantiate prefab zones
        heroManager.HomeZone().InstantiateTrainingEquipmentPrefabs();

        // hide the List window
        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.TRAININGEQUIP);
    }
}

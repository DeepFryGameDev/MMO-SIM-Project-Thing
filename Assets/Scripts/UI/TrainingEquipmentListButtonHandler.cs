using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
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
        // equip the training equipment
        Debug.Log("Equip the " + trainingEquipment.name + " to " + trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot());
        trainingEquipmentManager.Equip(trainingEquipment, trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot(), heroManager);

        // generate new equipment slots in equip menu
        trainingEquipmentMenu.InstantiateEquipmentSlots(heroManager);

        // hide the List window
        MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.TRAININGEQUIP);
    }

    
}

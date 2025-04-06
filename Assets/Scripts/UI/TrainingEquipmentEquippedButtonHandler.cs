using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class TrainingEquipmentEquippedButtonHandler : MonoBehaviour
{
    TrainingEquipmentMenu trainingEquipMenu;
    public void SetTrainingEquipMenu(TrainingEquipmentMenu trainingEquipMenu) { this.trainingEquipMenu = trainingEquipMenu; }

    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }

    int equipSlot;
    public void SetEquipSlot(int equipSlot) { this.equipSlot = equipSlot; }
    public int GetEquipSlot() { return equipSlot; }

    public void SetIcon(Sprite icon) { transform.Find("Icon").GetComponent<Image>().sprite = icon; }
    public void SetLevelText(int level) { transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = level.ToString(); }

    public void OnClick()
    {
        Debug.Log("Should show some kind of highlight on object " + trainingEquipMenu.GetClickedEquippedTrainingButton());
        trainingEquipMenu.SetClickedEquippedTrainingButtonHandler(this);

        if (MenuProcessingHandler.i.GetHeroCommandMenuState() != EnumHandler.HeroCommandMenuStates.TRAININGEQUIPLIST)
        {
            // instantiate all training equipment from inventory into list
            trainingEquipMenu.InstantiateEquipmentInventoryList(heroManager);

            // open the list menu
            MenuProcessingHandler.i.SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates.TRAININGEQUIPLIST);
        }
    }
}

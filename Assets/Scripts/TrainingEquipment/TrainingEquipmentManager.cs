using UnityEngine;

// Purpose: Facilitates equipping to hero and other training equipment mechanisms
// Directions: Just attach to [System]
// Other notes: 

public class TrainingEquipmentManager : MonoBehaviour
{
    TrainingEquipmentMenu trainingEquipmentMenu;

    private void Awake()
    {
        trainingEquipmentMenu = FindFirstObjectByType<TrainingEquipmentMenu>();
    }

    /// <summary>
    /// Equips the given training equipment into the given equip slot
    /// </summary>
    /// <param name="trainingEquipment">The equipment to be equipped</param>
    /// <param name="equipSlot">The equipment slot for the hero in which this should go</param>
    /// <param name="heroManager">HeroManager for the hero to have equipment equipped</param>
    public void Equip(TrainingEquipment trainingEquipment, int equipSlot, HeroManager heroManager)
    {
        if (trainingEquipment != null) // equipping valid equipment
        {
            // if something already exists in this slot, add it back to inventory
            if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot()) != null)
            {
                Debug.Log("Return " + heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot()).name + " to hero's inventory");
                heroManager.HeroInventory().AddToInventory(heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot()));
            }

            switch (equipSlot)
            {
                case 0:
                    heroManager.HeroTrainingEquipment().SetTrainingEquipmentSlot0(trainingEquipment);
                    break;
                case 1:
                    heroManager.HeroTrainingEquipment().SetTrainingEquipmentSlot1(trainingEquipment);
                    break;
            }

            // remove equipment from inventory
            Debug.Log("removing " + trainingEquipment.name + " from inventory");
            heroManager.HeroInventory().RemoveFromInventory(trainingEquipment);
        } else // unequipping
        {
            if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot()) != null)
            {
                Debug.Log("Return " + heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot()).name + " to hero's inventory");
                heroManager.HeroInventory().AddToInventory(heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(trainingEquipmentMenu.GetClickedEquippedTrainingButton().GetEquipSlot()));

                switch (equipSlot)
                {
                    case 0:
                        heroManager.HeroTrainingEquipment().SetTrainingEquipmentSlot0(null);
                        break;
                    case 1:
                        heroManager.HeroTrainingEquipment().SetTrainingEquipmentSlot1(null);
                        break;
                }
            }            
        }
    }
}

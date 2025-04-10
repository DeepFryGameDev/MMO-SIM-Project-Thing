using UnityEngine;

// Purpose: Facilitates equipping to hero and other training equipment mechanisms
// Directions: Just attach to [System]
// Other notes: 

public class TrainingEquipmentManager : MonoBehaviour
{
    TrainingEquipmentMenu trainingEquipmentMenu;

    public static TrainingEquipmentManager i;

    private void Awake()
    {
        trainingEquipmentMenu = FindFirstObjectByType<TrainingEquipmentMenu>();

        i = this;
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
            //Debug.Log("removing " + trainingEquipment.name + " from inventory");
            heroManager.HeroInventory().RemoveFromInventory(trainingEquipment);
        } else // unequipping
        {
            // first check if it's being used currently.  If it is, block the unequip.
            if (heroManager.HeroSchedule().GetScheduleEvents()[0].GetID() != heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(equipSlot).ID)
            {
                // Then Check if anything is scheduled for this hero that is using this training equipment.  If so, replace it with a rest.
                if (ScheduleManager.i.GetTrainingSlotsFromEventID(heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(equipSlot).ID).Count > 0)
                {
                    // Debug.LogWarning("You just unequipped something that was scheduled.  Setting to rest.");
                    // get training slots that event is scheduled
                    foreach (int slot in ScheduleManager.i.GetTrainingSlotsFromEventID(heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(equipSlot).ID))
                    {
                        Debug.LogWarning("Replacing " + slot + " with rest");
                        RestScheduleEvent newRestScheduleEvent = new RestScheduleEvent();

                        HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[slot] = newRestScheduleEvent;
                    }
                }

                // and then unequip it
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
}

using UnityEngine;

// Purpose: Contains the means to equip/set training equipment to heroes
// Directions: 
// Other notes: 

public class HeroTrainingEquipment : MonoBehaviour
{
    int trainingEquipmentSlots = 2; // Will eventually be controlled by hero level or something
    public int GetTrainingEquipmentSlots() { return trainingEquipmentSlots; }
    public void SetTrainingEquipmentSlots(int slots) { trainingEquipmentSlots = slots; }

    HeroManager heroManager;

    TrainingEquipment trainingEquipmentSlot0;
    public void SetTrainingEquipmentSlot0(TrainingEquipment trainingEquipment) { this.trainingEquipmentSlot0 = trainingEquipment; }
    public TrainingEquipment GetTrainingEquipmentSlot0() { return trainingEquipmentSlot0; }

    TrainingEquipment trainingEquipmentSlot1;
    public void SetTrainingEquipmentSlot1(TrainingEquipment trainingEquipment) { this.trainingEquipmentSlot1 = trainingEquipment; }
    public TrainingEquipment GetTrainingEquipmentSlot1() { return trainingEquipmentSlot1; }

    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
    }

    private void Start()
    {
        // for testing, add the 2 test training equips to inventory.
        /*Debug.Log("Adding " + TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[0].name + " to inventory");
        heroManager.HeroInventory().AddToInventory(TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[0]);

        Debug.Log("Adding " + TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[1].name + " to inventory");
        heroManager.HeroInventory().AddToInventory(TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[1]);*/
    }

    /// <summary>
    /// Just returns the TrainingEquipment in the given slot that is equipped to the hero
    /// </summary>
    /// <param name="slot">Slot # to return the equipment</param>
    /// <returns>TrainingEquipment that is equipped by the hero in the given slot</returns>
    public TrainingEquipment GetTrainingEquipmentBySlot(int slot)
    {
        switch (slot)
        {
            case 0:
                return trainingEquipmentSlot0;
            case 1:
                return trainingEquipmentSlot1;
            default:
                return null;
        }
    }
}

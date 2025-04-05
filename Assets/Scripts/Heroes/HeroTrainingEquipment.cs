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

    TrainingEquipment trainingEquipSlot0;
    public void SetTrainingEquipSlot0(TrainingEquipment trainingEquipment) { this.trainingEquipSlot0 = trainingEquipment; }
    public TrainingEquipment GetTrainingEquipSlot0() { return trainingEquipSlot0; }

    TrainingEquipment trainingEquipSlot1;
    public void SetTrainingEquipSlot1(TrainingEquipment trainingEquipment) { this.trainingEquipSlot1 = trainingEquipment; }
    public TrainingEquipment GetTrainingEquipmentSlot1() { return trainingEquipSlot1; }

    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
    }

    private void Start()
    {
        // for testing, add the 2 test training equips to inventory.
        Debug.Log("Adding " + TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[0].name + " to inventory");
        heroManager.HeroInventory().AddToInventory(TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[0]);

        Debug.Log("Adding " + TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[1].name + " to inventory");
        heroManager.HeroInventory().AddToInventory(TrainingEquipmentDatabase.db.GetBasicTrainingEquipment()[1]);
    }

    public TrainingEquipment GetTrainingEquipmentBySlot(int slot)
    {
        switch (slot)
        {
            case 0:
                return trainingEquipSlot0;
            case 1:
                return trainingEquipSlot1;
            default:
                return null;
        }
    }
}

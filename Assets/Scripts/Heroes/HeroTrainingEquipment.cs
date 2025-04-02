using UnityEngine;

// Purpose: Contains the means to equip/set training equipment to heroes
// Directions: 
// Other notes: 

public class HeroTrainingEquipment : MonoBehaviour
{
    int trainingEquipmentSlots = 2; // Will eventually be controlled by hero level or something
    public int GetTrainingEquipmentSlots() { return trainingEquipmentSlots; }
    public void SetTrainingEquipmentSlots(int slots) { trainingEquipmentSlots = slots; }
}

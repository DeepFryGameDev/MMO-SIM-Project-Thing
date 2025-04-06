using System.Collections.Generic;
using UnityEngine;

// Purpose: Used to easily access created ScriptableObjects for Training Equipment
// Directions: Attach to [Asset Database] and just call "TrainingEquipmentDatabase.db" to access the getters
// Other notes: 

public class TrainingEquipmentDatabase : MonoBehaviour
{
    public static TrainingEquipmentDatabase db;

    [SerializeField] List<TrainingEquipment> basicTrainingEquipment = new List<TrainingEquipment>();
    public List<TrainingEquipment> GetBasicTrainingEquipment() { return basicTrainingEquipment; }

    private void Awake()
    {
        db = this;
    }
}

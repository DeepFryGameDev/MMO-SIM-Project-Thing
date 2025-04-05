using System.Collections.Generic;
using UnityEngine;

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

using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class ScheduleSettingsEditor : MonoBehaviour
{
    public int BaseEnergyRestoredFromRest;

    private void Awake()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        ScheduleSettings.BaseEnergyRestoredFromRest = BaseEnergyRestoredFromRest;
    }
}

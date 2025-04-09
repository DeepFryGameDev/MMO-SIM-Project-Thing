using UnityEngine;

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

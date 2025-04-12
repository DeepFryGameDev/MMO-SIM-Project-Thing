using UnityEngine;

// Purpose: Holds any vars for the schedule that should be set in the inspector.
// Directions: Any vals added here just make sure to add to ScheduleSettings as well.
// Other notes: 

public class ScheduleSettingsEditor : MonoBehaviour
{
    [Tooltip("This is the energy that is restored to the hero whenever a rest is performed.")] // Further calcs will be done on this later.
    public int BaseEnergyRestoredFromRest;

    [Tooltip("Used before the # of the week in the schedule UI to display the week.")]
    public string weekText = "Week";

    private void Awake()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        ScheduleSettings.baseEnergyRestoredFromRest = BaseEnergyRestoredFromRest;
        ScheduleSettings.weekText = weekText;
    }
}

using UnityEngine;

// Purpose: Used to tweak various settings related to Date scripts - linked to the DateSettings static class so that other scripts can simply reference that.
// Directions: Attach to the [Editor] object
// Other notes: Any time something is added here, it should be set in the SetSettings() method as well.  Ensure the var is also added to DateSettings

public class DateSettingsEditor : MonoBehaviour
{
    [Tooltip("The amount of time in seconds that the 'Date' panel will be displayed in the upper left corner of the screen")]
    [Range(.5f, 5f)] public float toastSeconds = 4.0f;

    [Tooltip("The amount of time in seconds that the black image will fade in and out while transitioning weeks")]
    [Range(.1f, 1f)] public float fadeSeconds = .5f;

    [Tooltip("The amount of time in seconds that the total transition from fade out/fade in when transition to the next week")]
    [Range(1f, 5f)] public float transitionSeconds = 2.0f;

    void Awake()
    {
        SetSettings();
    }

    void SetSettings()
    {
        DateSettings.toastSeconds = toastSeconds;

        DateSettings.fadeSeconds = fadeSeconds;

        DateSettings.transitionSeconds = transitionSeconds;
    }
}

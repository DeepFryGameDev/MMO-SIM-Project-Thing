using UnityEngine;

// Purpose: Used to tweak various settings related to Date scripts - linked to the DateSettings static class so that other scripts can simply reference that.
// Directions: Attach to the [Editor] object
// Other notes: Any time something is added here, it should be set in the SetSettings() method as well.  Ensure the var is also added to DateSettings

public class DateSettingsEditor : MonoBehaviour
{
    [Header("-----Next Day Transition-----")]
    [Tooltip("The amount of time in seconds that the 'Date' panel will be displayed in the upper left corner of the screen")]
    [Range(.5f, 10f)] public float toastSeconds = 4.0f;

    [Tooltip("The amount of time in seconds that the black image will fade in and out while transitioning weeks")]
    [Range(.1f, 2f)] public float fadeSeconds = .5f;

    [Tooltip("The amount of time in seconds that the total transition from fade out/fade in when transition to the next week")]
    [Range(4f, 10f)] public float transitionSeconds = 5.0f;

    [Header("Training Results Display")]
    [Tooltip("The amount of time in seconds that the training result UI will be shown when transitioning to next week\n" +
    "NOTE: this value should be >= the sum of both trainingResultsDelaySeconds and trainingResultsFillSeconds, and should be set with transitionSeconds in mind")]
    [Range(4f, 10f)] public float trainingResultShowSeconds = 5;

    [Tooltip("The amount of time in seconds that the training result UI will before appearing")]
    [Range(.25f, 3f)] public float trainingResultsDelaySeconds = 1;

    [Tooltip("The amount in time in seconds that the training result UI will delay before animating the fill bars")]
    [Range(.25f, 3f)] public float trainingResultsFillDelaySeconds = 1;

    [Tooltip("The amount of time in seconds that the training result UI will animate the fill bars")]
    [Range(.25f, 5f)] public float trainingResultsFillSeconds = 3;

    void Awake()
    {
        SetSettings();
    }

    void SetSettings()
    {
        DateSettings.toastSeconds = toastSeconds;

        DateSettings.fadeSeconds = fadeSeconds;

        DateSettings.transitionSeconds = transitionSeconds;

        DateSettings.trainingResultShowSeconds = trainingResultShowSeconds;

        DateSettings.trainingResultsDelaySeconds = trainingResultsDelaySeconds;

        DateSettings.trainingResultsFillDelaySeconds = trainingResultsFillDelaySeconds;

        DateSettings.trainingResultsFillSeconds = trainingResultsFillSeconds;
    }
}

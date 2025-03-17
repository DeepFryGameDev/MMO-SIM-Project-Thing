using UnityEngine;

// Purpose: Used to tweak various settings related to Hero scripts - linked to the HeroSettings static class so that other scripts can simply reference that.
// Directions: Attach to the [Editor] object
// Other notes: Any time something is added here, it should be set in the SetSettings() method as well.  Ensure the var is also added to HeroSettings

public class HeroSettingsEditor : MonoBehaviour
{
    //-----------------------------------------------------------------
    [Header("-----Attributes and Stats-----")]

    [Tooltip("The maximum amount an attribute can be increased to.")] // This will be massively reworked, but leaving for now.
    public float maxAttributeVal = 9;

    [Space(10)]
    //-----------------------------------------------------------------
    [Header("-----Movement-----")]
    
    [Tooltip("The base movement speed that hero navMeshAgents should move at")]
    [Range(1.5f, 5.0f)] public float walkSpeed = 2.5f;

    [Tooltip("The movement speed that hero navMeshAgents should move at when they are running")]
    [Range(5.0f, 8.0f)] public float runSpeed = 6;

    [Space(10)]
    //-----------------------------------------------------------------
    [Header("-----Pathing-----")]

    [Tooltip("When directing a hero to a target (such as whistle), this is the distance away from the player at which the hero will run")]
    [Range(1f, 5f)] public float walkToTargetDistance = 3.0f;

    [Tooltip("When a hero's pathing mode is set to RANDOM, this is the minimum amount of wait time they will wait at each destination")]
    [Range(0f, 5f)] public float minRandomWaitSeconds = 3.0f;

    [Tooltip("When a hero's pathing mode is set to RANDOM, this is the maximum amount of wait time they will wait at each destination")]
    [Range(5f, 10f)] public float maxRandomWaitSeconds = 6.0f;

    [Tooltip("Used by various scripts to determine at which distance the hero's unit is away from the target, and is considered 'stopped'.  Probably do not need to tweak.")]
    public float stoppingDistance = .01f;

    [Header("Debugging")] // Logging

    [Tooltip("If logging should be enabled on pathing.  Keep in mind, this will clog the console, so don't leave it enabled for long lest you don't care about messy logs.")]
    public bool logPathingStuff;

    [Tooltip("When logging is enabled, if the distance of the hero's position and target destination is <= this value, 'Distance to destination' logs will be returned.\n" +
        "Keep it small if you are only looking for logs when the hero is arriving the destination.  A larger number will capture more of the travel.")]
    public float minLogDistance = .1f;

    [Space(10)]
    //-----------------------------------------------------------------
    [Header("-----Whistle-----")]

    [Tooltip("Used to determine how far away from the target destination that the hero will stop when being whistled")]
    public float whistleStoppingDistance = 1f;

    //-----------------------------------------------------------------

    private void Awake()
    {
        SetSettings();
    }

    /// <summary>
    /// Just initializes all of the HeroSettings values to whatever is set in here.
    /// </summary>
    void SetSettings()
    {
        // Attributes and stats
        HeroSettings.maxAttributeVal = maxAttributeVal;

        // Movement
        HeroSettings.walkSpeed = walkSpeed;
        HeroSettings.runSpeed = runSpeed;

        // Pathing
        HeroSettings.walkToTargetDistance = walkToTargetDistance;
        HeroSettings.minRandomWaitSeconds = minRandomWaitSeconds;
        HeroSettings.maxRandomWaitSeconds = maxRandomWaitSeconds;
        HeroSettings.stoppingDistance = stoppingDistance;
        HeroSettings.minLogDistance = minLogDistance;
        HeroSettings.logPathingStuff = logPathingStuff;

        // Whistle
        HeroSettings.whistleStoppingDistance = whistleStoppingDistance;
    }
}

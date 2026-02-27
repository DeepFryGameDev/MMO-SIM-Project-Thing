using UnityEngine;

public class BattleSettingsEditor : MonoBehaviour
{
    [Header("ATB Settings")]

    [Tooltip("The minimum random value that the ATB bar will start with at the beginning of battle")]
    public int minATBStartingVal = 5;

    [Tooltip("The maximum random value that the ATB bar will start with at the beginning of battle")]
    public int maxATBStartingVal = 40;

    [Tooltip("The maximum value that the ATB bar can reach before the unit can take an action")]
    public float maxATBVal = 100f;

    [Tooltip("The fastest time (in seconds) to go from 0 -> full at max ATB Fill Speed")]
    public float minFillTime = 2.5f;

    [Tooltip("The slowest time (in seconds) to go from 0 -> full at max ATB Fill Speed")]
    public float maxFillTime = 10f;

    [Tooltip("The ATB Speed value that maps to fastest time")]
    public float maxAtbSpeed = 99f;

    [Tooltip("The stopping distance for an agent when moving towards a target unit. This should be large enough to prevent the agent from trying to move inside the target's collider, but small enough to look natural.")]
    public float agentStoppingDistanceToTarget = 2.5f;

    [Tooltip("The stopping distance for an agent when moving towards a Vector3 destination point.")]
    public float agentStoppingDistanceToDestination = 0f;

    [Tooltip("Amount of time (in seconds) that the game will halt before an attack animation has triggered")]
    public float preAttackAnimWaitTime = .25f;

    [Tooltip("Amount of time (in seconds) that the game will halt after an attack animation has triggered")]
    public float postAttackAnimWaitTime = 0f;

    private void Awake()
    {
        SetSettings();
    }

    void SetSettings()
    {
        BattleSettings.minATBStartingVal = minATBStartingVal;
        BattleSettings.maxATBStartingVal = maxATBStartingVal;
        BattleSettings.maxATBVal = maxATBVal;

        BattleSettings.minFillTime = minFillTime;
        BattleSettings.maxFillTime = maxFillTime;
        BattleSettings.maxAtbSpeed = maxAtbSpeed;

        BattleSettings.agentStoppingDistanceToTarget = agentStoppingDistanceToTarget;
        BattleSettings.agentStoppingDistanceToDestination = agentStoppingDistanceToDestination;

        BattleSettings.preAttackAnimWaitTime = preAttackAnimWaitTime;
        BattleSettings.postAttackAnimWaitTime = postAttackAnimWaitTime;
    }
}

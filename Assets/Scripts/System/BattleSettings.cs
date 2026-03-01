using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public static class BattleSettings
{
    public static int minATBStartingVal;
    public static int maxATBStartingVal;
    public static float maxATBVal;

    public static float minFillTime;
    public static float maxFillTime;
    public static float maxAtbSpeed;

    public static float agentStoppingDistanceToTarget;
    public static float agentStoppingDistanceToDestination;
    public static float agentRunToTargetRunSpeed;

    public static float preAttackAnimWaitTime;

    public static float postAttackAnimWaitTime;

    public static string enemyDupeNamingConvention;
}

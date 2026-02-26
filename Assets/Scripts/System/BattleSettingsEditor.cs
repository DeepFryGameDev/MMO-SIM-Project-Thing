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

    private void Awake()
    {
        SetSettings();
    }

    void SetSettings()
    {
        BattleSettings.minATBStartingVal = minATBStartingVal;
        BattleSettings.maxATBStartingVal = maxATBStartingVal;
        BattleSettings.maxATBVal = maxATBVal;
    }
}

using System;
using UnityEngine;

public class DebugSettingsEditor : MonoBehaviour
{
    public Color playerDebugColor;

    public Color uiDebugColor;

    public Color scheduleDebugColor;

    public Color heroDebugColor;

    public Color systemDebugColor;

    public Color partyDebugColor;

    public Color inventoryDebugColor;

    public Color classDebugColor;

    private void Awake()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        DebugSettings.playerDebugColor = playerDebugColor;

        DebugSettings.uiDebugColor = uiDebugColor;

        DebugSettings.scheduleDebugColor = scheduleDebugColor;

        DebugSettings.heroDebugColor = heroDebugColor;

        DebugSettings.systemDebugColor = systemDebugColor;

        DebugSettings.partyDebugColor = partyDebugColor;

        DebugSettings.inventoryDebugColor = inventoryDebugColor;

        DebugSettings.classDebugColor = classDebugColor;
    }
}

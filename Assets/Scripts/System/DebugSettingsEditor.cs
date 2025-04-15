using System;
using UnityEngine;

public class DebugSettingsEditor : MonoBehaviour
{
    public Color uiDebugColor;

    public Color scheduleDebugColor;

    public Color heroDebugColor;

    public Color systemDebugColor;

    public Color partyDebugColor;

    private void Awake()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        DebugSettings.uiDebugColor = uiDebugColor;

        DebugSettings.scheduleDebugColor = scheduleDebugColor;

        DebugSettings.heroDebugColor = heroDebugColor;

        DebugSettings.systemDebugColor = systemDebugColor;

        DebugSettings.partyDebugColor = partyDebugColor;
    }
}

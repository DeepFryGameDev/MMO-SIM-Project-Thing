using System;
using UnityEngine;

public class UISettingsEditor : MonoBehaviour
{
    public Color junkItemColor;
    public Color commonItemColor;
    public Color uncommonItemColor;
    public Color rareItemColor;
    public Color epicItemColor;
    public Color legendaryItemColor;

    private void Awake()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        UISettings.junkItemColor = junkItemColor;
        UISettings.commonItemColor = commonItemColor;
        UISettings.uncommonItemColor = uncommonItemColor;
        UISettings.rareItemColor = rareItemColor;
        UISettings.epicItemColor = epicItemColor;
        UISettings.legendaryItemColor = legendaryItemColor;
    }
}

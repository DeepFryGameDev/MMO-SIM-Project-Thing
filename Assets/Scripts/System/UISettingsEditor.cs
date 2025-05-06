using UnityEngine;

// Purpose: Facilitates the functionality for easily updating various UI related settings in the inspector.
// Directions: Just attach to [Editor]
// Other notes: 

public class UISettingsEditor : MonoBehaviour
{
    [Tooltip("The color for various font related to an item if it's rarity is 'JUNK'")]
    public Color junkItemColor;
    [Tooltip("The color for various font related to an item if it's rarity is 'COMMON'")]
    public Color commonItemColor;
    [Tooltip("The color for various font related to an item if it's rarity is 'UNCOMMON'")]
    public Color uncommonItemColor;
    [Tooltip("The color for various font related to an item if it's rarity is 'RARE'")]
    public Color rareItemColor;
    [Tooltip("The color for various font related to an item if it's rarity is 'EPIC'")]
    public Color epicItemColor;
    [Tooltip("The color for various font related to an item if it's rarity is 'LEGENDARY'")]
    public Color legendaryItemColor;

    void Awake()
    {
        SetSettings();
    }

    void SetSettings()
    {
        UISettings.junkItemColor = junkItemColor;
        UISettings.commonItemColor = commonItemColor;
        UISettings.uncommonItemColor = uncommonItemColor;
        UISettings.rareItemColor = rareItemColor;
        UISettings.epicItemColor = epicItemColor;
        UISettings.legendaryItemColor = legendaryItemColor;
    }
}

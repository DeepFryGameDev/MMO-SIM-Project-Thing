using UnityEngine;

public static class UISettings
{
    public static Color junkItemColor;
    public static Color uncommonItemColor;
    public static Color rareItemColor;
    public static Color epicItemColor;
    public static Color legendaryItemColor;
    public static Color commonItemColor;

    public static Color GetRarityColor(HeroItem item)
    {
        switch (item.rarity)
        {
            case EnumHandler.InventoryRarities.JUNK:
                return junkItemColor;
            case EnumHandler.InventoryRarities.COMMON:
                return commonItemColor;
            case EnumHandler.InventoryRarities.UNCOMMON:
                return uncommonItemColor;
            case EnumHandler.InventoryRarities.RARE:
                return rareItemColor;
            case EnumHandler.InventoryRarities.EPIC:
                return epicItemColor;
            case EnumHandler.InventoryRarities.LEGENDARY:
                return legendaryItemColor;
            default:
                return new Color(0, 0, 0, 0);
        }
    }
}

using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroItem : BaseItem
{
    [Header("---Base Hero Item Values---")]
    public EnumHandler.InventoryRarities rarity;

    public int weight;

    public int goldValue;

    public void AddToHeroInventory(HeroManager heroManager)
    {
        heroManager.HeroInventory().AddToInventory(this);
    }
}

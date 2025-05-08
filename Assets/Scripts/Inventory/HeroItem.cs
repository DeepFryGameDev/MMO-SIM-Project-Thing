using UnityEngine;

// Purpose: Used as the base level class derived from BaseItem for all items that can be used/equipped by the hero.
// Directions: Anything specific to items on heroes should go here. (Pretty much everything that isn't a quest item)
// Other notes: 

public class HeroItem : BaseItem
{
    [Header("---Base Hero Item Values---")]
    public EnumHandler.InventoryRarities rarity;

    public float weight;

    public int goldValue;

    public void AddToHeroInventory(HeroManager heroManager)
    {
        heroManager.HeroInventory().AddToInventory(this);
    }
}

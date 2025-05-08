using UnityEngine;

// Purpose: Used to outline all functionality for junk items.  Junk items in particular are really only useful to sell.
// Directions: Create a new item with right click > Create > the path below in CreateAssetMenu.
// Other notes: 

[CreateAssetMenu(menuName = "Item/New Junk Item")]
public class JunkItem : HeroItem
{
    private void Awake()
    {
        rarity = EnumHandler.InventoryRarities.JUNK;  // this will keep all items created as a Junk Item with rarity = junk.
    }
}

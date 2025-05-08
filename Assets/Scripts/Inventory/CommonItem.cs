using UnityEngine;

// Purpose: These items are used for crafting or serve some other useful purpose, but also can be commonly sold as well.
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Item/New Common Item")]
public class CommonItem : HeroItem
{
    private void Awake()
    {
        rarity = EnumHandler.InventoryRarities.COMMON;
    }
}

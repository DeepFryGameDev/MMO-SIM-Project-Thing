using UnityEngine;

// Purpose: These items are used for crafting or serve some other useful purpose.
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

using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

[CreateAssetMenu(menuName = "Item/New Junk Item")]
public class JunkItem : HeroItem
{
    private void Awake()
    {
        rarity = EnumHandler.InventoryRarities.JUNK;
    }
}

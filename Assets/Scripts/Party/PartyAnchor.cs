using UnityEngine;

// Purpose: Used to manipulate the Party Follow script to keep the hero anchored to the object this is attached to
// Directions: Attach to the anchor objects under transform [Player]/[PlayerThings]/PartyFollow
// Other notes: 

public class PartyAnchor : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    /// <summary>
    /// Returns the position that this object is attached to.
    /// </summary>
    /// <returns>Position of the transform</returns>
    public Vector3 GetPosition() { return transform.position; }
}

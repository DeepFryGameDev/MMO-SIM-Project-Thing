using UnityEngine;

// Purpose: At the moment doesn't do a lot - just holds the current hero that is active when the player steps on a corresponding home zone.
// Directions: Attach to [System] object
// Other notes: 

public class HomeZoneManager : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    public static HomeZoneManager i;

    private void Awake()
    {
        i = this;
    }
}

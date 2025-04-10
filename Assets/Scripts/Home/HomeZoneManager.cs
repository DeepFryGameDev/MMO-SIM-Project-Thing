using UnityEngine;

// Purpose: 
// Directions: 
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

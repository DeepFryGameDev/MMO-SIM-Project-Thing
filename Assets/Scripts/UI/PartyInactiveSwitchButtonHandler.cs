using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class PartyInactiveSwitchButtonHandler : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }
}

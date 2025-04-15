using UnityEngine;

public class PartyAnchor : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    public Vector3 GetPosition() { return transform.position; }
}

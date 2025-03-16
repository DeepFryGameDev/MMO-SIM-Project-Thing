// Purpose: Used to facilitate the processing of triggering interactions between player objects and hero objects
// Directions: Attach to each hero
// Other notes: Derived from InteractionProcessing class

public class HeroInteraction : InteractionProcessing
{
    HeroCommandProcessing hcp; // The command processing script that handles the actual command procedures

    HeroManager heroManager; // The hero manager in which to reference linked scripts

    private void Start() // For scripts attached to heroes, set vars in Start so that HeroManager vars are set in Awake first.
    {
        hcp = FindFirstObjectByType<HeroCommandProcessing>();

        heroManager = GetComponent<HeroManager>();
    }

    /// <summary>
    /// Called automatically whenever the player interacts with a hero object
    /// </summary>
    public override void OnInteract()
    {        
        base.OnInteract();

        hcp.SetHeroPathing(heroManager.HeroPathing());

        // look at player

        hcp.OnInteract();
    }
}

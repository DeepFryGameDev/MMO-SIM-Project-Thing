using UnityEngine;

// Purpose: Used to handle interaction between the hero and the party
// Directions: Attach to each hero
// Other notes: 

public class HeroParty : MonoBehaviour
{
    PartyAnchor partyAnchor;
    public void SetPartyAnchor(PartyAnchor partyAnchor) { this.partyAnchor = partyAnchor; }
    public PartyAnchor GetPartyAnchor() { return partyAnchor; }
}

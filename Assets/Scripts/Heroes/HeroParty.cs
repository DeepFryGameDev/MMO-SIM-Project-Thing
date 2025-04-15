using UnityEngine;

public class HeroParty : MonoBehaviour
{
    PartyAnchor partyAnchor;
    public void SetPartyAnchor(PartyAnchor partyAnchor) { this.partyAnchor = partyAnchor; }
    public PartyAnchor GetPartyAnchor() { return partyAnchor; }
}

using System.Collections.Generic;
using UnityEngine;

// Purpose: Handles checking if the player should be interacting with any objects in the world that will change party states
// Directions: Attach to the [Player]/[PlayerThings]/PartyFollow object
// Other notes: 

public class PartyFollow : MonoBehaviour
{
    const float raycastDist = 3.0f; // the distance that the raycast will travel under the player to detect collision with the LeavingBaseZone

    LayerMask leavingBaseZoneLayer; // The layer mask for "LeavingBaseZone" / Set in Setup()
    LayerMask isFloorLayer; // The layer mask for "LeavingBaseZone" / Set in Setup()

    EnumHandler.PartyFollowStates partyFollowState;
    public EnumHandler.PartyFollowStates GetPartyFollowState() { return partyFollowState; }

    List<HeroManager> currentlyAnchoredHeroes = new List<HeroManager>();

    private void Awake()
    {
        Setup();
    }

    void Setup()
    {
        leavingBaseZoneLayer = LayerMask.GetMask("LeavingBaseZone");
        isFloorLayer = LayerMask.GetMask("whatIsBaseFloor");
    }

    void Update()
    {
        switch (partyFollowState)
        {
            case EnumHandler.PartyFollowStates.IDLE:
                CheckForLeavingBaseZone();
                break;
            case EnumHandler.PartyFollowStates.FOLLOWINBASE:
                // Ensure any heroes in the party are anchored
                KeepActiveHeroesAnchored();

                // at any point, if a hero is inactive AND anchored, they should be sent home immediately.  This means they were removed from the party before leaving the leaveZone.
                ReleaseAnchoredInactiveHeroes();

                CheckForEnteringBaseZone();
                break;
        }       
    }

    /// <summary>
    /// Just checks if the player is standing on the zone designated for heros to 'catch up' to them before leaving the base and out into the world
    /// </summary>
    void CheckForLeavingBaseZone()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            transform.TransformDirection(Vector3.down), out hit, raycastDist, leavingBaseZoneLayer))
        {
            // DebugManager.i.PartyDebugOut("PartyFollow", "HitLeavingBaseZone", false, false);

            //SetAnchoredHeroesList();

            // set the heroes to the anchor points and change their pathing
            partyFollowState = EnumHandler.PartyFollowStates.FOLLOWINBASE;            
        }

        // Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector3.down), Color.green);
    }

    /// <summary>
    /// Checks if the player is standing in the base, but not on a 'catch up' zone.  This is used to send the heros back to their home point if the player leave the exit area of the base.
    /// </summary>
    void CheckForEnteringBaseZone()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            transform.TransformDirection(Vector3.down), out hit, raycastDist, isFloorLayer))
        {
             // DebugManager.i.PartyDebugOut("PartyFollow", "HitEnteringBaseZone", false, false);

            // Send all heroes back to their starting point

            foreach (HeroManager heroManager in currentlyAnchoredHeroes)
            {
                heroManager.HeroPathing().StopPathing();

                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CANRUN);

                heroManager.HeroPathing().StartPartyRunHomePathing();
            }

            partyFollowState = EnumHandler.PartyFollowStates.IDLE;

            currentlyAnchoredHeroes.Clear();
        }

        // Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector3.down), Color.green);
    }

    /// <summary>
    /// Sets the currentlyAnchoredHeroes list to active heroes in the PartyManager
    /// </summary>
    void SetAnchoredHeroesList()
    {
        currentlyAnchoredHeroes.Clear();

        foreach (HeroManager heroManager in PartyManager.i.GetActiveHeroes())
        {
            currentlyAnchoredHeroes.Add(heroManager);
        }
    }

    /// <summary>
    /// Any heroes that are still in the party but should not be anchored will be sent back home here
    /// </summary>
    void ReleaseAnchoredInactiveHeroes()
    {
        foreach (HeroManager heroManager in currentlyAnchoredHeroes)
        {
            if (PartyManager.i.GetInactiveHeroes().Contains(heroManager))
            {
                heroManager.HeroPathing().StopPathing();

                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CANRUN);

                heroManager.HeroPathing().StartPartyRunHomePathing();
            }
        }

        SetAnchoredHeroesList();
    }

    /// <summary>
    /// Keeps active party members anchored to the player
    /// </summary>
    void KeepActiveHeroesAnchored()
    {
        bool setNewAnchoredList = false;

        foreach (HeroManager heroManager in HeroSettings.GetHeroesInParty())
        {
            if (!currentlyAnchoredHeroes.Contains(heroManager))
            {
                // DebugManager.i.PartyDebugOut("PartyFollow", heroManager.Hero().GetName() + " should be anchored");
                setNewAnchoredList = true;

                heroManager.HeroPathing().StopPathing();

                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CATCHUP);

                heroManager.HeroPathing().StartPartyPathing();
            }          
        }

        if (setNewAnchoredList) SetAnchoredHeroesList();
    }
}

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
                CheckForEnteringBaseZone();
                break;
        }        

        //transform.localRotation = Quaternion.Euler(transform.parent.rotation.x, 0, transform.parent.rotation.z);
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

            // set the heroes to the anchor points and change their pathing
            partyFollowState = EnumHandler.PartyFollowStates.FOLLOWINBASE;

            foreach (HeroManager heroManager in PartyManager.i.GetActiveHeroes())
            {
                heroManager.HeroPathing().StopPathing();

                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CATCHUP);

                heroManager.HeroPathing().StartPartyPathing();
            }
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

            foreach (HeroManager heroManager in PartyManager.i.GetActiveHeroes())
            {
                heroManager.HeroPathing().StopPathing();

                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CANRUN);

                heroManager.HeroPathing().StartPartyRunHomePathing();
            }

            partyFollowState = EnumHandler.PartyFollowStates.IDLE;
        }

        // Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector3.down), Color.green);
    }
}

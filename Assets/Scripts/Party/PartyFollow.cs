using System;
using UnityEngine;

public class PartyFollow : MonoBehaviour
{
    const float raycastDist = 3.0f; // the distance that the raycast will travel under the player to detect collision with the LeavingBaseZone

    LayerMask leavingBaseZoneLayer; // The layer mask for "LeavingBaseZone" / Set in Setup()
    LayerMask isFloorLayer; // The layer mask for "LeavingBaseZone" / Set in Setup()

    EnumHandler.PartyFollowStates partyFollowState;

    private void Awake()
    {
        leavingBaseZoneLayer = LayerMask.GetMask("LeavingBaseZone");
        isFloorLayer = LayerMask.GetMask("whatIsBaseFloor");
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch (partyFollowState)
        {
            case EnumHandler.PartyFollowStates.IDLE:
                CheckForLeavingBaseZone();
                break;
            case EnumHandler.PartyFollowStates.FOLLOWINBASE:
                // Check for base layer (make a new one)
                // if base layer detected, disperse the heroes back to their home zone and change follow state back to idle

                CheckForEnteringBaseZone();
                break;
        }        

        //transform.localRotation = Quaternion.Euler(transform.parent.rotation.x, 0, transform.parent.rotation.z);
    }

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

    void CheckForEnteringBaseZone()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            transform.TransformDirection(Vector3.down), out hit, raycastDist, isFloorLayer))
        {
             DebugManager.i.PartyDebugOut("PartyFollow", "HitEnteringBaseZone", false, false);

            // Send all heroes back to their starting point

            partyFollowState = EnumHandler.PartyFollowStates.IDLE;
        }

        // Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector3.down), Color.green);
    }
}

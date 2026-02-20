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

    [SerializeField] PartyAnchor anchorSlot0;
    [SerializeField] PartyAnchor anchorSlot1;
    [SerializeField] PartyAnchor anchorSlot2;
    [SerializeField] PartyAnchor anchorSlot3;
    [SerializeField] PartyAnchor anchorSlot4;

    public EnumHandler.PartyFollowStates partyFollowState;
    public EnumHandler.PartyFollowStates GetPartyFollowState() { return partyFollowState; }
    public void SetPartyFollowState(EnumHandler.PartyFollowStates followState) { partyFollowState = followState; }

    List<HeroManager> currentlyAnchoredHeroes = new List<HeroManager>(); // Used as a list to store heroes that are actually anchored to the player
    List<HeroManager> tempCurrentlyAnchoredHeroes = new List<HeroManager>(); // Used as a list to store when heroes are still in the 'catch up' state

    public static PartyFollow i;

    private void Awake()
    {
        Setup();

        i = this;
    }

    void Setup()
    {
        leavingBaseZoneLayer = LayerMask.GetMask("LeavingBaseZone");
        isFloorLayer = LayerMask.GetMask("whatIsBaseFloor");
        partyFollowState = EnumHandler.PartyFollowStates.IDLE;
    }

    void Update()
    {
        switch (partyFollowState)
        {
            case EnumHandler.PartyFollowStates.IDLE:

                CheckForLeavingBaseZone();

                break;
            case EnumHandler.PartyFollowStates.FOLLOWINBASE:
                //Debug.Log("In followinbase");

                // at any point, if a hero is inactive AND anchored, they should be sent home immediately.  This means they were removed from the party before leaving the leaveZone.
                ReleaseAnchoredInactiveHeroes();

                // Ensure anyone in temp list goes to catchup
                RunCatchup();

                // Ensure any heroes in the party are anchored
                KeepActiveHeroesAnchored();                

                // And finally check if you are exiting the 'Leaving Home' area to allow the heroes to go back to their business
                CheckForEnteringBaseZone();

                break;
            case EnumHandler.PartyFollowStates.FOLLOW: // this is for outside of base.  So far i haven't had to do anything with this.

                // Not sure if this is needed
                CheckForLeavingBaseZone();
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
            DebugManager.i.PartyDebugOut("PartyFollow", "Leaving home - party members anchoring!", false, false);

            //SetAnchoredHeroesList();

            SetTempFollowList();

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
            transform.TransformDirection(Vector3.down), out hit, raycastDist, isFloorLayer) && SceneInfo.i.GetSceneMode() == EnumHandler.SceneMode.HOME)
        {            
            DebugManager.i.PartyDebugOut("PartyFollow", "Entering home - party members going back to their business!", false, false);

            // Send all heroes back to their starting point

            foreach (HeroManager heroManager in currentlyAnchoredHeroes)
            {
                heroManager.HeroPathing().StopPathing();

                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CANRUN);

                heroManager.HeroPathing().StartPartyRunHomePathing();
            }

            foreach (HeroManager heroManager in tempCurrentlyAnchoredHeroes)
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
    public void SetAnchoredHeroesList()
    {
        //Debug.Log("Setting anchored heroes list");

        currentlyAnchoredHeroes.Clear();

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            //Debug.Log("Adding " + heroManager + " to anchored heroes list");
            // check if they are close enough to their anchor part to add them to currentlyAnchored
            // if so, add them to currentlyAnchored.  If not, add them to temporary list instead.
            //
            //
            if (Vector3.Distance(heroManager.HeroParty().GetPartyAnchor().GetPosition(), heroManager.transform.position) > 3)
            {
                tempCurrentlyAnchoredHeroes.Add(heroManager);
            } else
            {
                currentlyAnchoredHeroes.Add(heroManager);
            }                
        }
    }

    /// <summary>
    /// Any heroes that are still in the party but should not be anchored will be sent back home here
    /// </summary>
    void ReleaseAnchoredInactiveHeroes()
    {
        foreach (HeroManager heroManager in tempCurrentlyAnchoredHeroes)
        {
            if (GameSettings.GetIdleHeroes().Contains(heroManager))
            {
                heroManager.HeroPathing().StopPathing();
                heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CANRUN);
                heroManager.HeroPathing().StartPartyRunHomePathing();
                SetAnchoredHeroesList();
            }

            /*if (tempCurrentlyAnchoredHeroes.Contains(heroManager))
            {
                Debug.Log("Hero is in tempCurrentlyAnchoredHeroes");

                if (GameSettings.GetHeroesInParty().Contains(heroManager))
                {
                    Debug.Log("Hero is in party");
                }
                else
                {
                    Debug.Log("Hero is not in party");
                }
            } else
            {
                Debug.Log("Hero is not in tempCurrentlyAnchoredHeroes");

            }

            if (currentlyAnchoredHeroes.Contains(heroManager))
            {
                Debug.Log("hero is currently anchored");
            }
            else
            {
                Debug.Log("Hero is not currently anchored.");
                break;
            }*/

        }        
    }

    /// <summary>
    /// Keeps active party members anchored to the player
    /// </summary>
    void KeepActiveHeroesAnchored()
    {
        // Run catchup on anyone who is in the temp list. (should be in a separate method)

        // if they are currently anchored, run party pathing instead
        bool setNewAnchoredList = false;
        foreach (HeroManager heroManager in currentlyAnchoredHeroes)
        {
            if (!currentlyAnchoredHeroes.Contains(heroManager))
            {
                //Debug.Log("Stopping " + heroManager.Hero().GetName());
                // DebugManager.i.PartyDebugOut("PartyFollow", heroManager.Hero().GetName() + " should be anchored");
                setNewAnchoredList = true;

                heroManager.HeroPathing().StartPartyPathing();
            }          
        }

        if (setNewAnchoredList) SetAnchoredHeroesList();
    }

   /// <summary>
   /// Any tempCurrentlyAnchoredHeroes (which should be running to the player) during FOLLOWINBASE are processed here - this is where they are set to run catchup
   /// </summary>
    void RunCatchup()
    {
        {
            foreach (HeroManager heroManager in tempCurrentlyAnchoredHeroes)
            {
                if (heroManager.HeroPathing().GetRunMode() != EnumHandler.pathRunMode.CATCHUP && !currentlyAnchoredHeroes.Contains(heroManager))
                {
                    heroManager.HeroPathing().StopPathing();

                    heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.CATCHUP);
                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.PARTYFOLLOW);

                    // check if they should are close enough to be considered 'anchored', and them add them to currentlyAnchoredHeroes, and remove them from tempCurrentlyAnchoredHeroes.  Will probably need to use a separate list for this.

                    if (IfCloseEnoughForAnchor(heroManager))
                    {
                        // remove them from tempCurrentlyAnchored and set them to anchored
                        currentlyAnchoredHeroes.Add(heroManager);
                    }
                }
            }
        }

        // If we run this in the above loop, we will get a Collection was modified error.  This just removes them from the temp list once they are actually anchored.
        foreach (HeroManager heroManager in currentlyAnchoredHeroes)
        {
            if (tempCurrentlyAnchoredHeroes.Contains(heroManager))
            {
                tempCurrentlyAnchoredHeroes.Remove(heroManager);
            }
        }
    }

    /// <summary>
    /// Returns if the hero is close enough to their anchor point to be considered "anchored".
    /// </summary>
    /// <param name="heroManager">Hero to check</param>
    /// <returns>True if they are close enough to be anchored, false if they are not</returns>
    bool IfCloseEnoughForAnchor(HeroManager heroManager)
    {
        if (Vector3.Distance(heroManager.transform.position, heroManager.HeroParty().GetPartyAnchor().GetPosition()) > 5) // will need to be set to a value in a settings script.  For now, this is ok.
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// This sets the temp follow list for any heroes that have just joined the party
    /// 1. They are moved to this list
    /// 2. And then ran through CATCHUP
    /// 3. Finally when the hero is close enough to be anchored, is removed from this list and added to the CurrentlyAnchoredHeroes list
    /// </summary>
    public void SetTempFollowList()
    {
        tempCurrentlyAnchoredHeroes.Clear();

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            tempCurrentlyAnchoredHeroes.Add(heroManager);
        }
    }

    /// <summary>
    /// Sets the transform of the given anchor to the given heroManager
    /// </summary>
    /// <param name="heroManager">The HeroManager of the hero to have the anchor set</param>
    /// <param name="slot">The slot of the anchor in the UI to be set</param>
    public void SetFollowAnchor(HeroManager heroManager, int slot)
    {
        switch (slot)
        {
            case 0:
                anchorSlot0 = transform.Find("Anchor[0]").GetComponent<PartyAnchor>();
                heroManager.HeroParty().SetPartyAnchor(anchorSlot0);
                anchorSlot0.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.gameObject.name + " to follow anchor slot 0", false, false);
                break;
            case 1:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot1);
                anchorSlot1.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.gameObject.name + " to follow anchor slot 1", false, false);
                break;
            case 2:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot2);
                anchorSlot2.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.gameObject.name + " to follow anchor slot 2", false, false);
                break;
            case 3:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot3);
                anchorSlot3.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.gameObject.name + " to follow anchor slot 3", false, false);
                break;
            case 4:
                heroManager.HeroParty().SetPartyAnchor(anchorSlot4);
                anchorSlot4.SetHeroManager(heroManager);
                DebugManager.i.PartyDebugOut("PartyMenuHandler", "Set " + heroManager.gameObject.name + " to follow anchor slot 4", false, false);
                break;
        }
    }
}

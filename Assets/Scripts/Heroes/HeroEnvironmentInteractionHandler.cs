using UnityEngine;

// Purpose: Used to process the UI display to the player of any hero's room they are standing in
// Directions: This script relies on the player's transform position, so for now, attach to the [Player] object.
// Other notes: 

public class HeroEnvironmentInteractionHandler : MonoBehaviour
{
    const float raycastDist = 3.0f; // the distance that the raycast will travel under the player to detect collision with a Hero's Zone

    bool inHomeZone; // When home zone is detected, this is true

    HeroManager heroManager; // The hero in which to display the stats

    LayerMask ZoneLayer; // The layer mask for "HomeZone" / Set in Setup()

    HeroZoneUIHandler zoneUIHandler; // Used to display the panel / Set in Setup()

    private void Awake()
    {
        Setup();
    }

    /// <summary>
    /// Used to set all the needed vars for the script to process
    /// This method should always be called in Awake()
    /// </summary>
    void Setup()
    {
        ZoneLayer = LayerMask.GetMask("HomeZone");
        zoneUIHandler = FindFirstObjectByType<HeroZoneUIHandler>();
    }


    void Update()
    {
        RunHandler();
    }

    /// <summary>
    /// Runs all functionality for checking the Hero Zone room
    /// </summary>
     void RunHandler()
    {
        CheckForRoom();
    }

    /// <summary>
    /// Processes functionality for when room state is changed
    /// </summary>
    void CheckForRoom()
    {
        RaycastHit hit;

        if (!inHomeZone) // Not in a hero room - scanning to turn it on
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            transform.TransformDirection(Vector3.down), out hit, raycastDist, ZoneLayer))
            {
                SetUpdatedRoomState(true, hit);
            }
        }

        if (inHomeZone) // In a hero room - scanning to turn it off
        {
            if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            transform.TransformDirection(Vector3.down), out hit, raycastDist, ZoneLayer))
            {
                SetUpdatedRoomState(false, hit);
            }
        }        
    }

    /// <summary>
    /// Functionality for when the state of the HeroZone changes (player moves on/off the zone)
    /// </summary>
    /// <param name="active">True: If the stat panel should be drawn, False: If it should be hidden</param>
    /// <param name="hit">The object hit by the raycast</param>
    void SetUpdatedRoomState(bool active, RaycastHit hit)
    {
        if (active)
        {
            heroManager = hit.collider.gameObject.GetComponent<HeroHomeZone>().heroManager;
            if (heroManager != null)
            {
                SetHeroVals(heroManager);

                zoneUIHandler.SetScheduleHeroManager(heroManager);

                inHomeZone = true;
            }
        } else
        {
            inHomeZone = false;
            zoneUIHandler.ShowPanel(false);
        }
    }

    /// <summary>
    /// Called when the hero stats should be displayed to the player
    /// </summary>
    /// <param name="hero"></param>
    void SetHeroVals(HeroManager heroManager)
    {
        // set UI stuff with hero vals
        zoneUIHandler.DrawHeroStatsToPanel(heroManager);

        zoneUIHandler.ShowPanel(true);
    }
}

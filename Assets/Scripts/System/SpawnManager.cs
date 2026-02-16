using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Purpose: Used to manage the player and heroes being forcibly placed at a point in the world (like resetting position when going to next week and transitioning scenes)
// Directions: Attach to the [SpawnManager] GameObject
// Other notes: The functionality on here is very sensitive and the project has broken several times for seemingly no reason.  Be careful changing things here.

public class SpawnManager : MonoBehaviour
{
    Vector3 playerSpawnPosition;
    public Vector3 GetPlayerSpawnPosition() { return playerSpawnPosition; }
    public void SetPlayerSpawnPosition(Vector3 playerSpawnPosition) { this.playerSpawnPosition = playerSpawnPosition; }

    GameObject player;

    public static SpawnManager i;

    bool gameSet;

    [SerializeField] Transform heroesTransform;

    bool transitioning;

    PartyFollow partyFollow;

    void Awake()
    {
        if (i == null || !i.gameSet) // called when game is initiated
        {
            Singleton();                                  

            i.player = GameObject.FindWithTag("Player");

            i.playerSpawnPosition = i.player.transform.position;                     

        } else // moved in from another scene
        {
            if (!i.transitioning)
            {
                switch (SceneInfo.i.GetSceneMode())
                {
                    case EnumHandler.SceneMode.FIELD:
                        
                        break;
                    case EnumHandler.SceneMode.HOME:
                        
                        break;
                }

                PostSceneTransition();
            }            
        }

        partyFollow = FindFirstObjectByType<PartyFollow>();
    }

    void Singleton()
    {
        if (i != null)
            Destroy(gameObject);
        else
            i = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called in Awake - used to transition the player and heroes into the new scene when transitioning scenes.  This should be used in conjunction with BaseScriptedEvent.TransitionToScene().
    /// </summary>
    void PostSceneTransition()
    {
        MovePlayerToSpawnPosition();

        StartCoroutine(FadeAndEnableMovement());
    }

    private void Start()
    {
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.FIELD:

                break;
            case EnumHandler.SceneMode.HOME:
                
                break;
        }

        if (!i.gameSet)
        {
            InitializeHeroes();

            i.gameSet = true;
        }
    }

    /// <summary>
    /// Initializes the hero objects using the heroes configured in NewGameSetup.GetActiveHeroObjects()
    /// Super sensitive.  Project keeps breaking here.  If anything breaks in this method, everything will halt until it's fixed, as heroes will not spawn.
    /// </summary>
    void InitializeHeroes()
    {
        // instantiate the heroes here
        //Debug.Log("Instantiate these here");
        GameSettings.ClearIdleHeroes();

        foreach (GameObject heroObject in NewGameSetup.i.GetActiveHeroObjects()) // For each hero in NewGameSetup.i.GetActiveHeroObjects()
        {
            GameObject newHeroObject = Instantiate(heroObject, heroesTransform); // Create a new object using the prefab assigned in NewGameSetup

            // -- Confusion starts here
            // Step 1.  We need to get the home zone before we can allow the hero to do any pathing.  They will try to path on awake, right away.  We can try to disable that and set the hero pathing to RANDOM below here if we tweak things.
            HeroHomeZone homeZone = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>(); // Gets the current home zone in the scene for the hero (Required for HeroPathing)
            //Debug.Log("Home zone for " + newHeroObject.GetComponent<HeroManager>().Hero().GetName() + ": " + homeZone.transform.position);

            // Step 2. Set the home zone's hero manager and vice versa.  These are needed for pathing.
            homeZone.SetHeroManager(newHeroObject.GetComponent<HeroManager>()); // Sets the hero manager of the new object instantiated to the hero's home zone
            newHeroObject.GetComponent<HeroManager>().SetHomeZone(homeZone); // Sets the Home Zone for the hero to the hero manager of the new object instantiated

            // Step 3. Set the hero pathing's collider.  We have to set it before the hero can path in the home zone.
            newHeroObject.GetComponent<HeroPathing>().SetCollider();

            // Step 4. Disables the nav mesh agent - this is so that the transform's position can be manipulated without interference.
            heroObject.GetComponent<HeroPathing>().ToggleNavMeshAgent(false);                             

            // Step 5. Sets the transform's position
            newHeroObject.transform.position = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).GetSpawnPosition();

            // Step 6. Re-enable the nav mesh agent so the hero can move
            heroObject.GetComponent<HeroPathing>().ToggleNavMeshAgent(true);

            //heroObject.GetComponent<HeroPathing>().SetPathMode(EnumHandler.pathModes.RANDOM); // re-enable this if it's removed in Awake() in HeroPathing.

            // Step 7. Initializes the Idle Heroes values in GameSettings
            GameSettings.AddToIdleHeroes(newHeroObject.GetComponent<HeroManager>());

            GameSettings.SetAllHeroes();

            // Step 8. Initializes Hero Objects and HeroManagers for active heroes.  Not sure if the HeroManager will be needed, but when I removed it, everything blew up.  Shrug.
            ActiveHeroesSystem.i.SetHeroObject(heroObject.GetComponent<HeroManager>().GetID(), heroObject);
            ActiveHeroesSystem.i.SetHeroManager(heroObject.GetComponent<HeroManager>());

            // -- Hopefully no more confusion.
        }
    }

    /// <summary>
    /// Ran Immediately after the scene is loaded, and if the scene being loaded is a Home scene.
    /// This does 2 things:
    /// 1. Loads the home zone in the newly loaded scene and sets it to all idle and party heroes.
    /// 2. Updates the GameSettings HeroManagers for idle and party heroes after they've had the home zone set.
    /// 
    /// This is necessary for hero pathing so they are able to access the newly loaded home zones.  Note the steps are the same for both idle and party heroes.  If updating one of them, check to update both.
    /// </summary>
    void HeroHomeSetup()
    {
        List<HeroManager> tempHeroManagers = new List<HeroManager>(); // Used as a temporary list for resetting the GameSettings hero lists.  Not sure if it's truly needed to be set again after they are initialized?  More doing it for safety.

        // --- Idle Heroes
        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes()) // For each Idle Hero in GameSettings
        {
            //Debug.Log("Setting up Idle Hero: " + heroManager.Hero().GetName());
                        
            // Step 1. Get the home zone.
            HeroHomeZone homeZone = GetHomeZone(heroManager.GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();

            // Step 2. Set it to the HeroManager and vice versa
            homeZone.SetHeroManager(heroManager);
            heroManager.SetHomeZone(homeZone);

            // Step 3. Add it to the temp list for resetting
            tempHeroManagers.Add(heroManager);
        }

        // --- Reset Idle Heroes
        //Debug.Log("Clearing idle heroes");
        GameSettings.ClearIdleHeroes();        

        foreach (HeroManager heroManager in tempHeroManagers)
        {
            //Debug.Log("And adding idle hero back again: " + heroManager);
            GameSettings.AddToIdleHeroes(heroManager);            
        }

        tempHeroManagers.Clear(); // re-use it for party heroes

        // --- Heroes In Party
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty()) // For each hero in party in GameSettings
        {
            //Debug.Log("Setting up Party Hero: " + heroManager.Hero().GetName());

            // Step 1. Get the home zone.
            HeroHomeZone homeZone = GetHomeZone(heroManager.GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();

            // Step 2. Set it to the HeroManager and vice versa
            homeZone.SetHeroManager(heroManager);
            heroManager.SetHomeZone(homeZone);

            // Step 3. Add it ot the temp list for resetting
            tempHeroManagers.Add(heroManager);
        }

        // --- Reset Party Heroes
        GameSettings.ClearParty();

        foreach (HeroManager heroManager in tempHeroManagers)
        {
            GameSettings.AddToParty(heroManager);
        }

        // --- Instantiate Training Equip Prefabs
        // for each hero in idle
        // check each training equip slot. if equipped, instantiate it into the necessary slot
        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
        {
            heroManager.HomeZone().InstantiateTrainingEquipmentPrefabs();
        }

        // do the same for each hero in party
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            heroManager.HomeZone().InstantiateTrainingEquipmentPrefabs();
        }        
    }

    /// <summary>
    /// Waits until the scene is fully loaded
    /// Once the scene is loaded, it will unload the last scene and prepare the heroes for navmeshagent movement in the newly loaded scene.
    /// </summary>
    IEnumerator FadeAndEnableMovement()
    {
        i.transitioning = true;

        while (GameSettings.GetSceneTransitionStarted()) // Wait until scene transition is complete (scene is fully loaded)
        {
            yield return new WaitForEndOfFrame();
        }

        // Unload the last scene
        DebugManager.i.SystemDebugOut("TransitionToScene", "------------And here.  Keep on rocking in the free world, and doot doola doot doo.-------------");

        DebugManager.i.SystemDebugOut("SpawnManager", "Unloading Scene: " + GameSettings.GetUnloadSceneIndex());
        SceneManager.UnloadSceneAsync(GameSettings.GetUnloadSceneIndex());        

        // set Heroes up
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.HOME:
                HeroHomeSetup();
                break;
            case EnumHandler.SceneMode.FIELD:

                break;
        }

        // Move them to their designated position (heroes in party to their party anchors, idle heroes to their home position)
        MoveHeroesToPosition();

        // Allow the player to move again
        //PlayerMovement.i.ToggleMovement(true);

        // And any last minute stuff that should happen
        // --------------------------------------------
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.HOME:

                break;
            case EnumHandler.SceneMode.FIELD:

                break;
        }

        MenuProcessingHandler.i = FindFirstObjectByType<MenuProcessingHandler>();
        // ---------------------------------------------

        // Fade the black back out so the user can see again
        StartCoroutine(UIManager.i.FadeToBlack(false));

        i.transitioning = false;
    }

    /// <summary>
    /// Checks all homezone objects in the scene and returns the one with the same ID as the hero
    /// </summary>
    /// <param name="ID">ID of the hero to get the homezone object</param>
    /// <returns>BaseHomeZone attached to the home zone object designated for the given Hero ID</returns>
    BaseHomeZone GetHomeZone(int ID)
    {
        foreach (GameObject homeZoneObject in GameObject.FindGameObjectsWithTag("HomeZone"))
        {
            BaseHomeZone homeZone = homeZoneObject.GetComponent<BaseHomeZone>();
            if (homeZone.GetHeroID() == ID)
            {
                //Debug.Log("Returning homezone " + homeZone.gameObject.name);
                return homeZone;
            }
        }

        return null;
    }

    /// <summary>
    /// Moves the heroes to their designated position.  
    /// Heroes in party will be moved to their party anchors. 
    /// If at home, idle heroes are placed at their homeZone positions.  If in the field, idle heroes are hidden and placed at position 0,0,0.
    /// </summary>
    void MoveHeroesToPosition()
    {
        switch (SceneInfo.i.GetSceneMode())
        {
            // -----------------------------------------------------------------
            // -- FIELD --

            case EnumHandler.SceneMode.FIELD:
                //Debug.Log("Moving heroes by field");

                // -- IDLE HEROES --
                foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
                {
                    // This is where we can turn off pathing and hide the object and whatever is needed to get them to chill if their scripts are freaking out.
                    // set their scale to 0?  Unless we think of a better method in the future.

                    if (ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.localScale.x != 0) // if they haven't been tweaked already
                    {
                        heroManager.HeroPathing().ToggleNavMeshAgent(false); // turn off nav mesh agent so it is quiet.
                        ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = new Vector3(0, 0, 0); // sets the position of the hero to 0,0,0.
                        ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.localScale = new Vector3(0, 0, 0); // sets the scale to 0,0,0 to hide them.
                    }
                }

                // HEROES IN PARTY --
                foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
                {
                    // disable navmeshagent
                    heroManager.HeroPathing().ToggleNavMeshAgent(false);

                    // Move these objects to their anchor point
                    //Debug.Log("Move " + heroManager.Hero().GetName() + " to " + heroManager.HeroParty().GetPartyAnchor().GetPosition());
                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = heroManager.HeroParty().GetPartyAnchor().GetPosition();

                    // enable navmeshagent again
                    heroManager.HeroPathing().ToggleNavMeshAgent(true);

                    // Set the pathing mode to party follow so they are attached to the anchor as the player moves
                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.PARTYFOLLOW);
                }

                // And set the party follow state.
                partyFollow.SetPartyFollowState(EnumHandler.PartyFollowStates.FOLLOW);                     

                break;

            // -----------------------------------------------------------------
            // -- HOME --

            case EnumHandler.SceneMode.HOME:
                // -- IDLE HEROES --
                foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
                {
                    // Debug.Log(heroManager.Hero().GetName() + " should be moved to home zone: " + GetHomeZone(heroManager.GetID()).GetSpawnPosition());

                    // These heroes are already idle, so their navmeshagent has already been turned off.  We can safely move them.
                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = GetHomeZone(heroManager.GetID()).GetSpawnPosition(); // moves the hero object to their home zone spawn position

                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.localScale = new Vector3(1, 1, 1); // Scales them back to their original size

                    // Set the collider of the home zone - needed for pathing
                    heroManager.HeroPathing().SetCollider();

                    // As they are in their home zone, we don't want them running around.  Only allow walking.
                    heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.WALK);

                    // Set the path mode to random. This will need to be changed in the future when random isn't the default option.
                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.RANDOM);

                    // enable navmeshagent again for pathing
                    heroManager.HeroPathing().ToggleNavMeshAgent(true);                    
                }

                // -- HEROES IN PARTY --
                partyFollow.SetAnchoredHeroesList();

                foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
                {
                    // Debug.Log(heroManager.Hero().GetName() + " should be moved to party anchor: " + heroManager.HeroParty().GetPartyAnchor().GetPosition());

                    // Turn off their nav mesh agent to move them safely
                    heroManager.HeroPathing().ToggleNavMeshAgent(false);

                    // Move them to their party anchor position
                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = heroManager.HeroParty().GetPartyAnchor().GetPosition();                    

                    // Set the collider of the home zone - needed for pathing
                    heroManager.HeroPathing().SetCollider();                    

                    // Sets the path mode to party follow for the heroPathing
                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.PARTYFOLLOW);

                    // Enable navmeshagent again for pathing to work
                    heroManager.HeroPathing().ToggleNavMeshAgent(true);
                }

                if (GameSettings.GetHeroesInParty().Count > 0)
                {
                    // set the temporary Follow list
                    partyFollow.SetTempFollowList();

                    // And sets the party follow to follow
                    switch (SceneInfo.i.GetSceneMode())
                    {
                        case EnumHandler.SceneMode.FIELD:
                            partyFollow.SetPartyFollowState(EnumHandler.PartyFollowStates.FOLLOW);
                            break;
                        case EnumHandler.SceneMode.HOME:
                            partyFollow.SetPartyFollowState(EnumHandler.PartyFollowStates.FOLLOWINBASE);
                            break;
                    }                    
                }                
                break;
        }        
    }

    /// <summary>
    /// Sets the player's position to the position when the game was launched
    /// </summary>
    public void MovePlayerToSpawnPosition()
    {
        //Debug.Log("Move player to " + i.playerSpawnPosition);
        i.player.transform.position = i.playerSpawnPosition;
    }
}

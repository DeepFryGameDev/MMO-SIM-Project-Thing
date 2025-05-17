using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Purpose: Used to manage the player being forcibly placed at a point in the world (like resetting position when going to next week)
// Directions: Attach to the [System] GameObject
// Other notes: 

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
        if (i == null || !i.gameSet)
        {
            Singleton();                                  

            i.player = GameObject.FindWithTag("Player");

            i.playerSpawnPosition = i.player.transform.position;

                     

        } else // moved in from another scene
        {
            if (!i.transitioning)
            {
                switch (SceneInfo.i.GetMenuMode())
                {
                    case EnumHandler.MenuMode.FIELD:
                        
                        break;
                    case EnumHandler.MenuMode.HOME:
                        
                        break;
                }

                PostSceneTransition();
            }            
        }

        partyFollow = FindFirstObjectByType<PartyFollow>();
    }

    private void Start()
    {
        switch (SceneInfo.i.GetMenuMode())
        {
            case EnumHandler.MenuMode.FIELD:

                break;
            case EnumHandler.MenuMode.HOME:
                
                break;
        }

        if (!i.gameSet)
        {
            InitializeHeroes();

            i.gameSet = true;
        }
    }

    void InitializeHeroes()
    {
        // instantiate the heroes here
        Debug.Log("Instantiate these here");
        GameSettings.ClearIdleHeroes();

        foreach (GameObject heroObject in NewGameSetup.i.GetActiveHeroObjects())
        {
            GameObject newHeroObject = Instantiate(heroObject, heroesTransform);

            HeroHomeZone homeZone = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
            //Debug.Log("Home zone for " + newHeroObject.GetComponent<HeroManager>().Hero().GetName() + ": " + homeZone.transform.position);
            homeZone.SetHeroManager(newHeroObject.GetComponent<HeroManager>());
            newHeroObject.GetComponent<HeroManager>().SetHomeZone(homeZone);

            newHeroObject.GetComponent<HeroPathing>().SetCollider();

            heroObject.GetComponent<HeroPathing>().ToggleNavMeshAgent(false);                             

            GameSettings.AddToIdleHeroes(newHeroObject.GetComponent<HeroManager>());

            newHeroObject.transform.position = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).GetSpawnPosition();

            heroObject.GetComponent<HeroPathing>().ToggleNavMeshAgent(true);

            //heroObject.GetComponent<HeroPathing>().SetPathMode(EnumHandler.pathModes.RANDOM);

            ActiveHeroesSystem.i.SetHeroObject(heroObject.GetComponent<HeroManager>().GetID(), heroObject);
            ActiveHeroesSystem.i.SetHeroManager(heroObject.GetComponent<HeroManager>());
        }
    }

    void HeroHomeSetup()
    {
        List<HeroManager> tempHeroManagers = new List<HeroManager>();

        // idle heroes
        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
        {
            //Debug.Log("Setting up Idle Hero: " + heroManager.Hero().GetName());
                        
            HeroHomeZone homeZone = GetHomeZone(heroManager.GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
            homeZone.SetHeroManager(heroManager);

            heroManager.SetHomeZone(homeZone);

            tempHeroManagers.Add(heroManager);
        }

        // Reset idle heroes
        GameSettings.ClearIdleHeroes();
        //Debug.Log("Clearing idle heroes");

        foreach (HeroManager heroManager in tempHeroManagers)
        {
            //Debug.Log("And adding idle hero back again: " + heroManager);
            GameSettings.AddToIdleHeroes(heroManager);            
        }

        tempHeroManagers.Clear(); // re-use it for party heroes

        // in party
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            Debug.Log("Setting up Party Hero: " + heroManager.Hero().GetName());
            HeroHomeZone homeZone = GetHomeZone(heroManager.GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
            homeZone.SetHeroManager(heroManager);

            heroManager.SetHomeZone(homeZone);

            tempHeroManagers.Add(heroManager);
        }

        GameSettings.ClearParty();

        foreach (HeroManager heroManager in tempHeroManagers)
        {
            GameSettings.AddToParty(heroManager);
        }
    }

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

    void Singleton()
    {
        if (i != null)
            Destroy(gameObject);
        else
            i = this;

        DontDestroyOnLoad(gameObject);
    }

    void PostSceneTransition()
    {
        MovePlayerToSpawnPosition();

        StartCoroutine(FadeAndEnableMovement());
    }

    void MoveHeroesToPosition()
    {
        switch (SceneInfo.i.GetMenuMode())
        {
            case EnumHandler.MenuMode.FIELD:
                Debug.Log("Moving heroes by field");

                // Idle Heroes
                foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
                {
                    // This is where we can turn off pathing and hide the object and whatever is needed to get them to chill.
                    // set their scale to 0?  Unless we think of a better method in the future.

                    if (ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.localScale.x != 0)
                    {
                        heroManager.HeroPathing().ToggleNavMeshAgent(false);
                        ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = new Vector3(0, 0, 0);
                        ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.localScale = new Vector3(0, 0, 0);
                    }
                }

                // Heroes in Party
                foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
                {
                    // disable navmeshagent
                    heroManager.HeroPathing().ToggleNavMeshAgent(false);

                    // Move these objects to their anchor point
                    Debug.Log("Move " + heroManager.Hero().GetName() + " to " + heroManager.HeroParty().GetPartyAnchor().GetPosition());
                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = heroManager.HeroParty().GetPartyAnchor().GetPosition();

                    // enable navmeshagent again
                    heroManager.HeroPathing().ToggleNavMeshAgent(true);

                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.PARTYFOLLOW);
                }

                partyFollow.SetPartyFollowState(EnumHandler.PartyFollowStates.FOLLOW);                     

                break;
            case EnumHandler.MenuMode.HOME:
                // Idle Heroes
                foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
                {
                    // Debug.Log(heroManager.Hero().GetName() + " should be moved to home zone: " + GetHomeZone(heroManager.GetID()).GetSpawnPosition());

                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = GetHomeZone(heroManager.GetID()).GetSpawnPosition();

                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.localScale = new Vector3(1, 1, 1);

                    // if working, enable navmeshagent again
                    heroManager.HeroPathing().SetCollider();
                    heroManager.HeroPathing().SetRunMode(EnumHandler.pathRunMode.WALK);

                    heroManager.HeroPathing().ToggleNavMeshAgent(true);

                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.RANDOM);
                }

                // Heroes in Party
                partyFollow.SetAnchoredHeroesList();

                foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
                {
                    // Debug.Log(heroManager.Hero().GetName() + " should be moved to party anchor: " + heroManager.HeroParty().GetPartyAnchor().GetPosition());

                    heroManager.HeroPathing().ToggleNavMeshAgent(false);
                    ActiveHeroesSystem.i.GetHeroObject(heroManager.GetID()).transform.position = heroManager.HeroParty().GetPartyAnchor().GetPosition();                    

                    // if working, enable navmeshagent again
                    heroManager.HeroPathing().SetCollider();                    

                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.PARTYFOLLOW);

                    heroManager.HeroPathing().ToggleNavMeshAgent(true);
                }
                
                partyFollow.SetPartyFollowState(EnumHandler.PartyFollowStates.FOLLOWINBASE);
                break;
        }        
    }

    IEnumerator FadeAndEnableMovement()
    {
        i.transitioning = true;

        yield return new WaitForSeconds(1.5f); // need to figure out a way to wait until the scene is fully loaded before we can unload the next scene.  For now we will artifically wait 1.5 seconds.

        // Unload the last scene
        SceneManager.UnloadSceneAsync(GameSettings.GetUnloadSceneIndex());

        // set up heroes
        switch (SceneInfo.i.GetMenuMode())
        {
            case EnumHandler.MenuMode.HOME:
                HeroHomeSetup();
                break;
            case EnumHandler.MenuMode.FIELD:

                break;
        }

        MoveHeroesToPosition();

        PlayerMovement.i.ToggleMovement(true);

        StartCoroutine(UIManager.i.FadeToBlack(false));

        i.transitioning = false;
    }

    /// <summary>
    /// Sets the player's position to the position when the game was launched
    /// </summary>
    public void MovePlayerToSpawnPosition()
    {
        Debug.Log("Move player to " + i.playerSpawnPosition);
        i.player.transform.position = i.playerSpawnPosition;
    }
}

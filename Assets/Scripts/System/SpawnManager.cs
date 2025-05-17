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

    void Awake()
    {
        if (i == null || !i.gameSet)
        {
            Singleton();

            i.gameSet = true;                        

            i.player = GameObject.FindWithTag("Player");

            i.playerSpawnPosition = i.player.transform.position;

            InitializeHeroes();            

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
            homeZone.SetHeroManager(newHeroObject.GetComponent<HeroManager>());

            newHeroObject.GetComponent<HeroManager>().SetHomeZone(homeZone);

            GameSettings.AddToIdleHeroes(newHeroObject.GetComponent<HeroManager>());

            newHeroObject.transform.position = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).GetSpawnPosition();

            GameSettings.SetHeroObject(heroObject.GetComponent<HeroManager>().GetID(), heroObject);
        }
    }

    void HeroHomeSetup()
    {
        Debug.Log("Setting up idle heroes");

        List<HeroManager> tempHeroManagers = new List<HeroManager>();

        // idle heroes
        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
        {
            Debug.Log("Instantiate " + heroManager.Hero().GetName());

            GameObject newHeroObject = Instantiate(GameSettings.GetHeroObject(heroManager.GetID()), heroesTransform); // this is the base object. it needs heroManager applied to it.   

            HeroHomeZone homeZone = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
            homeZone.SetHeroManager(newHeroObject.GetComponent<HeroManager>());

            newHeroObject.GetComponent<HeroManager>().SetHomeZone(homeZone);

            newHeroObject.transform.position = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).GetSpawnPosition();

            tempHeroManagers.Add(heroManager);
        }

        // Reset idle heroes
        GameSettings.ClearIdleHeroes();

        foreach (HeroManager heroManager in tempHeroManagers)
        {
            GameSettings.AddToIdleHeroes(transform.GetComponent<HeroManager>());
        }

        tempHeroManagers.Clear(); // re-use it for party heroes

        // in party
        Debug.Log("And party heroes");
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            Debug.Log("Instantiate " + heroManager.Hero().GetName());
            // instantiate them each under the [Heroes] transform.  This will be instantiating the base object
            // set their position to their anchor point
            // Set HeroManager values so changes between scenes are saved
        }
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
                foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
                {
                    // Instantiate objects and set them under Heroes transform, then set them to partyfollow. maybe that will work idk.
                    GameObject newHeroObj = Instantiate(GameSettings.GetHeroObject(heroManager.Hero().GetID()), heroManager.HeroParty().GetPartyAnchor().transform.position, Quaternion.identity);
                    newHeroObj.transform.SetParent(heroesTransform);
                }

                GameSettings.ClearParty();

                foreach (Transform transform in heroesTransform)
                {
                    GameSettings.AddToParty(transform.GetComponent<HeroManager>());
                }

                PartyFollow.i = FindFirstObjectByType<PartyFollow>();

                for (int i=0; i < GameSettings.GetHeroesInParty().Count; i++)
                {
                    PartyFollow.i.SetFollowAnchor(GameSettings.GetHeroesInParty()[i], i);

                    GameSettings.GetHeroesInParty()[i].HeroPathing().SetRunMode(EnumHandler.pathRunMode.CANRUN);
                }                

                break;
            case EnumHandler.MenuMode.HOME:
                /*Debug.Log("Moving heroes by home");
                foreach (HeroManager idleHero in GameSettings.GetIdleHeroes())
                {
                    Debug.Log("Should move " + idleHero + " to " + GetHomeZone(idleHero.GetID()).GetSpawnPosition());
                    GameObject newHeroObj = Instantiate(GameSettings.GetHeroObject(idleHero.GetID()), heroesTransform);

                    HeroHomeZone homeZone = GetHomeZone(idleHero.GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
                    homeZone.SetHeroManager(newHeroObj.GetComponent<HeroManager>());
                    newHeroObj.GetComponent<HeroManager>().SetHomeZone(homeZone);

                    newHeroObj.transform.position = GetHomeZone(idleHero.GetID()).GetSpawnPosition();
                }*/
                break;
        }        
    }

    BaseHomeZone GetHomeZone(int ID)
    {
        foreach (GameObject homeZoneObject in GameObject.FindGameObjectsWithTag("HomeZone"))
        {
            BaseHomeZone homeZone = homeZoneObject.GetComponent<BaseHomeZone>();
            if (homeZone.GetHeroID() == ID)
            {
                return homeZone;
            }
        }

        return null;
    }

    IEnumerator FadeAndEnableMovement()
    {
        i.transitioning = true;

        yield return new WaitForSeconds(1.5f); // need to figure out a way to wait until the scene is fully loaded before we can unload the next scene.  For now we will artifically wait 1.5 seconds.

        // Unload the last scene
        Debug.Log("Unloading " + GameSettings.GetUnloadSceneIndex());
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

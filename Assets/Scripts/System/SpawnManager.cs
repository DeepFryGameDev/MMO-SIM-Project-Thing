using System;
using System.Collections;
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

    bool runningFade;

    public int sceneToUnload; // change to private later

    [SerializeField] Transform heroesTransform;

    void Awake()
    {
        if (i == null || !i.gameSet)
        {
            Singleton();

            i.gameSet = true;                        

            i.player = GameObject.FindWithTag("Player");

            i.playerSpawnPosition = i.player.transform.position;

        } else // moved in from another scene
        {
            if (!i.runningFade)
            {
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
                InitializeHeroes();

                HeroSetup();
                break;
        }
    }

    void InitializeHeroes()
    {
        // instantiate the heroes here
        foreach (GameObject heroObject in NewGameSetup.i.GetActiveHeroObjects())
        {
            GameObject newHeroObject = Instantiate(heroObject, heroesTransform);

            HeroHomeZone homeZone = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
            homeZone.SetHeroManager(newHeroObject.GetComponent<HeroManager>());

            newHeroObject.GetComponent<HeroManager>().SetHomeZone(homeZone);

            newHeroObject.transform.position = GetHomeZone(newHeroObject.GetComponent<HeroManager>().GetID()).GetSpawnPosition();
        }
    }

    void HeroSetup()
    {
        foreach (Transform transform in heroesTransform)
        {
            HeroManager heroManager = transform.GetComponent<HeroManager>();
            heroManager.Setup();

            HeroSettings.AddToIdleHeroes(heroManager);

            HeroSettings.SetHeroObject(heroManager.GetID(), heroManager.gameObject);
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
                foreach (HeroManager heroManager in HeroSettings.GetHeroesInParty())
                {
                    /*Debug.Log("Move " + heroManager.Hero().GetName() + " to " + heroManager.HeroParty().GetPartyAnchor().transform.position);
                    heroManager.HeroPathing().GetAgent().Warp(heroManager.HeroParty().GetPartyAnchor().transform.position);

                    heroManager.HeroPathing().GetAgent().ResetPath();
                    heroManager.HeroPathing().SetPathMode(EnumHandler.pathModes.PARTYFOLLOW);*/

                    // Instantiate objects and set them under Heroes transform, then set them to partyfollow. maybe that will work idk.
                }
                break;
            case EnumHandler.MenuMode.HOME:
                Debug.Log("Moving heroes by home");
                foreach (HeroManager idleHero in HeroSettings.GetIdleHeroes())
                {
                    Debug.Log("Should move " + idleHero + " to " + GetHomeZone(idleHero.GetID()).GetSpawnPosition());
                    GameObject newHeroObj = Instantiate(HeroSettings.GetHeroObject(idleHero.GetID()), heroesTransform);

                    HeroHomeZone homeZone = GetHomeZone(idleHero.GetID()).transform.Find("HomeZone").GetComponent<HeroHomeZone>();
                    homeZone.SetHeroManager(newHeroObj.GetComponent<HeroManager>());
                    newHeroObj.GetComponent<HeroManager>().SetHomeZone(homeZone);

                    newHeroObj.transform.position = GetHomeZone(idleHero.GetID()).GetSpawnPosition();
                }
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
        yield return new WaitForSeconds(1.5f); // need to figure out a way to wait until the scene is fully loaded before we can unload the next scene.  For now we will artifically wait 1.5 seconds.

        i.runningFade = true;        

        // Unload the last scene
        SceneManager.UnloadSceneAsync(sceneToUnload);

        MoveHeroesToPosition();

        PlayerMovement.i.ToggleMovement(true);

        StartCoroutine(UIManager.i.FadeToBlack(false));        
    }

    /// <summary>
    /// Sets the player's position to the position when the game was launched
    /// </summary>
    public void MovePlayerToSpawnPosition()
    {
        i.player.transform.position = i.playerSpawnPosition;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

// Purpose: Handles the processing of setting the player and camera variables for a new game
// Directions: Attach to MainMenu object on Main Menu scene.  Alternatively, attach to [Debugging] object on any scene that is intended to be tested from the editor.
// Other notes:

public class NewGameSetup : MonoBehaviour
{
    [Tooltip("Check this if starting from a scene that is not the main menu.  Player will be set up using class selected in PlayerManager on [Player] object")]
    [SerializeField] bool debugging;

    EnumHandler.PlayerClasses classToSet; // Set to the class that is either 1. Selected by the player on Main Menu, or 2. Chosen on [Player] PlayerManager dropdown when testing

    BaseScriptedEvent bse; // Used to transition to another scene (will be moved)

    PlayerManager playerManager; // Used to set the BasePlayer and AttackAnchor object to the Player Manager
    PlayerMovement playerMovement; // Used to set vars needed for player movement
    CameraManager cameraManager; // Used to set camera/cinemachine variables, such as LookAt and Follow

    UIHandler uiHandler; // Used to display the resource and EXP panels

    ThirdPersonCam thirdPersonCam; // Used to set needed variables for thirdPersonCam script as well as hide the cursor when the game starts

    GameObject playerParent; // Set to the parent anchor for the player object "[Player]"

    IEnumerator spawnPlayer; // Used as a coroutine to ensure the game is completely set before spawning the player

    // Player class prefab paths in Resources folder
    //string playerWarriorPath = "CharacterPlayers/WarriorPlayer";
    //string playerMagePath = "CharacterPlayers/MagePlayer";
    //string playerArcherPath = "CharacterPlayers/ArcherPlayer";

    public GameObject newPlayerObject;

    private void Start()
    {
        if (!GameManager.GetGameSet()) // Ensures this is only ran once, even if this script is on multiple scenes
        {
            InitialSetup();

            SceneInfo sceneInfo = FindAnyObjectByType<SceneInfo>();
        }        
    }

    /// <summary>
    /// Sets appropriate variables for this script to be able to set the rest of vars needed to start the game
    /// </summary>
    void InitialSetup()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        cameraManager = FindFirstObjectByType<CameraManager>();

        thirdPersonCam = cameraManager.GetComponentInChildren<ThirdPersonCam>();

        playerParent = GameObject.FindGameObjectWithTag("Player");

        PlayerSetup();

        GlobalSettings.SetUIState(GlobalSettings.UIStates.IDLE);
    }

    /// <summary>
    /// The overall method to process setting up needed variables for the player to be spawned into the worldfor the game to start
    /// </summary>
    void PlayerSetup()
    {
        // Debug.Log("Running player setup");

        // Set up different scripts
        CameraSetup(newPlayerObject);

        PlayerMovementSetup(newPlayerObject);

        PlayerManagerSetup(newPlayerObject);

        EquipmentManager.Startup();

        InventoryManager.Startup();

        if (!debugging) // Starting from main menu
        {

        }
        else // Starting from another scene
        {
            //StartCoroutine(spawnPoint.SetSpawn(playerParent)); // Spawns player at designated spawnPoint location
        }

        cameraManager.HideCursor();

        // Setup complete vars to true
        playerManager.SetPlayerSet(true);
        Debug.Log("thirdpersonCam set up complete");
        thirdPersonCam.SetCameraSetupComplete(true);

        // Give player starter items
        //playerManager.GiveStartupItems();

        // Set up stats for player
        //PlayerStatsSetup(newPlayerObject.GetComponent<BaseHero>());

        Debug.Log("Game set!");

        GameManager.SetGameSet(true);
    }

    // the below needs to be moved to each hero individually


    /// <summary>
    /// Sets the player's initial stats when creating the game session (Will likely be updated/removed)
    /// </summary>
    /// <param name="hero">The player to have stats set up on</param>
    void PlayerStatsSetup(BaseHero hero)
    {

    }

    /// <summary>
    /// Sets the various vars required for game creation for player movement
    /// </summary>
    void PlayerMovementSetup(GameObject newPlayerObject)
    {
        Debug.Log("Begin player movement setup");
        playerMovement.SetVars(newPlayerObject);
        Debug.Log("Player movement setup complete");
    }

    /// <summary>
    /// Sets the vars required for the PlayerManager to function during gameplay
    /// </summary>
    void PlayerManagerSetup(GameObject newPlayerObject)
    {
        playerManager.Setup(newPlayerObject);
        Debug.Log("Player manager setup complete");
    }

    /// <summary>
    /// Sets the vars required for the camera to perform the needed functions during gameplay
    /// </summary>
    void CameraSetup(GameObject newPlayerObject)
    {
        thirdPersonCam.SetPlayerParent(playerParent.transform);
        thirdPersonCam.SetPlayerObj(newPlayerObject.transform);
        thirdPersonCam.SetOrientation(newPlayerObject.transform.Find("Orientation"));
        //thirdPersonCam.SetCombatLookAt(newPlayerObject.transform.Find("Orientation/CombatLookAt"));

        //cameraManager.GetCombatCam().GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = newPlayerObject.transform.Find("Orientation/CombatLookAt");

        cameraManager.SetCamerasSet(true);
    }
}

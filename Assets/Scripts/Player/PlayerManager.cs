using UnityEngine;

// Purpose: Handles the player's resources (exp, health, stamina, etc) and UI elements to be updated with them, along with setting player stats
// Directions: Attach to Player GameObject
// Other notes: Can probably move the experience methods to the base player and leave there

public class PlayerManager : MonoBehaviour
{
    [Header("EXP")]
    [Tooltip("Index 0 is treated as level 1 - array of experience points needed to progress to the next level")]
    public int[] expToNextLevel;

    // ---
    [Header("Moving")]
    public float baseMoveSpeed = 5; // The move speed value to start calculation
    //float agilityToMoveSpeedMod = .5f; // For each point of agility, move speed is increased by this value

    //float agilityToStaminaRecoveryMod = 1.5f; // Stamina recovery rate is increased by this value for each point in agility

    // Current Experience Points
    int exp;

    // Current level
    int level;

    // Experience points needed to progress to the next level
    int currentExpToLevel;

    // Turns true if player has used up all of their stamina and needs to recover fully
    bool stamDepleted;
    public bool GetStamDepleted() { return stamDepleted; }

    // Turns true if player is not moving
    bool standingStill;
    public void SetStandingStill(bool standingStill) { this.standingStill = standingStill; }

    bool recoveringStamina; // Turns true if player is recovering stamina from sprinting
    Color baseStamBarColor; // Default value of stamina bar color

    BaseHero player; // Used to manipulate stamina and get global variable for player
    public BaseHero GetPlayer() { return player; }

    PrefabManager prefabManager; // Used to manipulate stamina bar on UI    

    bool playerSet; // Set to true when the playerManager vars have all been set up for game startup
    public void SetPlayerSet(bool set) { this.playerSet = set; }
    public bool GetPlayerSet() { return playerSet; }

    public static PlayerManager i;

    private void Awake()
    {
        Singleton();
    }

    void Singleton()
    {
        if (i != null)
            Destroy(gameObject);
        else
            i = this;

        DontDestroyOnLoad(gameObject);
    }

    void SetVars(GameObject playerParent)
    {
        prefabManager = FindFirstObjectByType<PrefabManager>();
        //baseStamBarColor = prefabManager.GetStaminaBarImage().color;
        //level = 1;

        //currentExpToLevel = expToNextLevel[level - 1];
    }

    /// <summary>
    /// Used to calculate the player's base movement speed to be used before sprint speed calculations
    /// </summary>
    /// <returns>Value calculated from adding baseMoveSpeed to the player's agility * the agilityToMoveSpeed modifier</returns>
    public float GetMoveSpeed()
    {
        return baseMoveSpeed;
    }

    /// <summary>
    /// Ran during game setup to set variables needed for playing the game
    /// </summary>
    public void Setup(GameObject playerParent)
    {
        SetVars(playerParent);
    }
}

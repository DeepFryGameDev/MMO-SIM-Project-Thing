using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Purpose: Facilitates logic for how the hero should move around the world
// Directions: Attach to each hero object
// Other notes: 

public class HeroPathing : MonoBehaviour
{
    bool whistleTargetWithinRange = false; // Turns true when the hero's object is within range (HeroSettings.whistleStoppingDistance) of the player's object when whistled
    public bool GetWhistleTargetWithinRange() { return whistleTargetWithinRange; }

    NavMeshAgent agent; // Agent to be manipulated to allow movement for hero
    public NavMeshAgent GetAgent() { return agent; }

    float moveSpeed; // Is synchronized with the hero's NavMeshAgent movement speed
    public float GetMoveSpeed() { return moveSpeed; }

    bool shouldRun; // Is set to true when the hero's running animation and run speed should be active. Essentially, when the hero should be running.
    public bool GetShouldRun() { return shouldRun; }
        
    public enum pathModes 
    {
        IDLE, // Hero is not moving
        RANDOM, // Hero is choosing random points within home zone and pathing to them
        TARGET, // Hero is moving to a designated point (ie whistle command)
        WHISTLE,
        COMMAND // Not being used yet
    }

    public pathModes pathMode; // The behavior that the pathing should follow - what is the hero currently doing?

    #region RandomPathing Vars

    bool randPathingActive; // Returns true if the hero is pathing to a random point
    bool randomWaiting; // Returns true when the hero has reached a random destination and is waiting to path to a new one    

    #endregion

    #region WhistlePathing Vars

    float distanceToWhistleTarget; // The distance from the hero's object to the whistle target (likely the player)
    Transform playerTransform; // Set to the transform of the whistle target to gather position. Might not be needed. (should always be player)

    #endregion

    BoxCollider homeZoneCollider; // Used to gather the bounds so heroes can be given random coordinates within.
                                  // This ensures random positions are only given within the hero's HomeZone bounds
    
    HeroManager heroManager; // Used to gather the other needed scripts attached to the hero

    void Start() // For scripts attached to heroes, set vars in Start so that HeroManager vars are set in Awake first.
    {
        Setup();
    }

    /// <summary>
    /// Sets variables needed for HeroPathing to function
    /// </summary>
    void Setup()
    {
        heroManager = GetComponent<HeroManager>();

        homeZoneCollider = heroManager.HomeZone().GetComponent<BoxCollider>();

        agent = heroManager.NavMeshAgent();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        agent.speed = HeroSettings.walkSpeed;
        moveSpeed = agent.speed; // just simply setting defaults to w/e the agent's speed is (which is coincidentally set from HeroSettings)
    }

    void Update()
    {
        switch (pathMode)
        {
            case pathModes.RANDOM:
                ProcessRandomPathing();
                break;
            case pathModes.WHISTLE:
                WhistlePathing();
                break;
        }    

        SyncMoveSpeed();        
    }

    /// <summary>
    /// Is run in the Update() method.  This keeps the agent's move speed synchronized with whatever we set "moveSpeed" to.
    /// </summary>
    void SyncMoveSpeed()
    {
        agent.speed = moveSpeed;
    }

    #region RandomPathing

    /// <summary>
    /// Ran in Update().  Simply rotates between pathing to random positions, and waiting until pathing again
    /// </summary>
    void ProcessRandomPathing()
    {
        if (!randPathingActive)
        {
            PathToRandomPosition();
        }
        else if (randomWaiting == false)
        {
            CheckForRandomWait();
        }
    }

    /// <summary>
    /// Begins new random pathing procedures.
    /// </summary>
    public void StartNewRandomPathing()
    {
        moveSpeed = HeroSettings.walkSpeed;

        agent.isStopped = false;

        pathMode = pathModes.RANDOM;
    }

    /// <summary>
    /// Checks if the conditions are correct for the hero to stop Random Pathing and should stop and wait.
    /// If true, begins the WaitForRandomPathing Coroutine.
    /// </summary>
    void CheckForRandomWait()
    {
        if (randPathingActive && (Vector3.Distance(agent.destination, transform.position) <= HeroSettings.stoppingDistance)) // stopping
        {
            if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: at destination. Waiting...");
            randomWaiting = true;
            StartCoroutine(WaitForRandomPathing());
        }
    }

    /// <summary>
    /// Coroutine.  After random seconds (GetRandomWaitTime(), it simply just turns off the vars used to control the random waiting.
    /// </summary>
    /// <returns>Waits for random seconds (GetRandomWaitTime() at the beginning of the Coroutine.</returns>
    IEnumerator WaitForRandomPathing()
    {
        yield return new WaitForSeconds(GetRandomWaitTime());
        if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: New Random Path starting");
        randPathingActive = false;
        randomWaiting = false;
    }

    /// <summary>
    /// Chooses a random position within the hero's HomeZone bounds and sets the NavMeshAgent destination to that position
    /// </summary>
    void PathToRandomPosition()
    {
        if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - RandomPathing: Setting new destination");
        Vector3 tryPos = GetRandomPositionInBounds();
        if (tryPos != new Vector3(0,0,0))
        {
            agent.SetDestination(GetRandomPositionInBounds());
            randPathingActive = true;
        }
    }

    /// <summary>
    /// Returns a random position within the bounds of the hero's HomeZone collider.  This is used by the hero to path to random points within it.
    /// </summary>
    /// <returns>Position within the hero's HomeZone collider</returns>
    Vector3 GetRandomPositionInBounds()
    {
        Vector3 tryPos = new Vector3(
            Random.Range(homeZoneCollider.bounds.min.x, homeZoneCollider.bounds.max.x),
            Random.Range(homeZoneCollider.bounds.min.y, homeZoneCollider.bounds.max.y),
            Random.Range(homeZoneCollider.bounds.min.z, homeZoneCollider.bounds.max.z)
            );

        // ensure it didn't hit any home zones first, then return it.  if it hit a home zone, set to 0,0,0 so we can retry.
        RaycastHit hit;
        if (Physics.Raycast(tryPos, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("PrefabZone"))
            {
                Debug.LogWarning("Hit prefab zone.  Getting a new position");
                return new Vector3(0,0,0);
            }
        }

        return tryPos;
    }

    /// <summary>
    /// Returns a value used by the hero to determine how long they should wait before starting a new random pathing.
    /// </summary>
    /// <returns>Random value between HeroSettings.minRandomWaitSeconds and HeroSettings.maxRandomWaitSeconds</returns>
    float GetRandomWaitTime()
    {
        return Random.Range(HeroSettings.minRandomWaitSeconds, HeroSettings.maxRandomWaitSeconds);
    }

    /// <summary>
    /// Stops the NavMeshAgent from moving and resets Pathing vars
    /// </summary>
    public void StopPathing()
    {
        agent.ResetPath();
        agent.isStopped = true;

        agent.velocity = new Vector3(0, 0, 0);

        randPathingActive = false;
        randomWaiting = false;

        whistleTargetWithinRange = false;

        pathMode = pathModes.IDLE;
    }

    #endregion

    #region WhistlePathing

    /// <summary>
    /// Ran in Update().  Processes when to update whistle vars for when the hero is pathing to the whistle target
    /// </summary>
    void WhistlePathing()
    {
        distanceToWhistleTarget = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToWhistleTarget <= HeroSettings.whistleStoppingDistance && distanceToWhistleTarget != 0)
        {
            whistleTargetWithinRange = true;
        }

        if (distanceToWhistleTarget > HeroSettings.whistleStoppingDistance)
        {
            if (HeroSettings.logPathingStuff) Debug.Log(gameObject.name + " - WhistlePathing: Not within stopping distance: " + distanceToWhistleTarget + " > " + HeroSettings.whistleStoppingDistance);
            whistleTargetWithinRange = false;
        }

        if (Vector3.Distance(agent.destination, transform.position) <= HeroSettings.minLogDistance && HeroSettings.logPathingStuff)
        {
            Debug.Log(gameObject.name + " - WhistlePathing: Distance to dest: " + Vector3.Distance(agent.destination, transform.position));
        }
    }

    /// <summary>
    /// Sets the destination for the NavMeshAgent and other needed Whistle vars
    /// </summary>
    /// <param name="targetTransform">Transform containing the position that the hero should path to</param>
    public void WhistleMoveToTarget()
    {
        distanceToWhistleTarget = Vector3.Distance(transform.position, playerTransform.position);

        // send agent to targetTransform pos
        agent.SetDestination(playerTransform.position);
    }

    #endregion

    /// <summary>
    /// Switches between Hero running and walking
    /// </summary>
    /// <param name="running">True if the hero should be running.  False if they should be walking.</param>
    public void ToggleRun(bool running)
    {
        if (running)
        {
            shouldRun = true;
            moveSpeed = HeroSettings.runSpeed;
        } else
        {
            shouldRun = false;
            moveSpeed = HeroSettings.walkSpeed;
        }
    }

    public void MoveToStartingPosition()
    {
        transform.position = heroManager.GetStartingPosition();
    }
}

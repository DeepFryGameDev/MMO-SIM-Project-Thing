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

    EnumHandler.pathModes pathMode; // The behavior that the pathing should follow - what is the hero currently doing?
    public void SetPathMode(EnumHandler.pathModes pathMode) { this.pathMode = pathMode; }
    public EnumHandler.pathModes GetPathMode() { return pathMode; }

    EnumHandler.pathRunMode runMode;
    public void SetRunMode(EnumHandler.pathRunMode runMode) { this.runMode = runMode; }
    public EnumHandler.pathRunMode GetRunMode() { return runMode; }

    Vector3 spawnPos = new Vector3();
    public Vector3 GetSpawnPos() { return spawnPos; }

    bool debuggingPositionSet = false;


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

    bool inBattle;
    public void SetInBattle(bool inBattle) { this.inBattle = inBattle; }
    public bool GetInBattle() { return inBattle; }

    void Awake()
    {
        Setup();
    }

    /// <summary>
    /// Sets variables needed for HeroPathing to function
    /// </summary>
    public void Setup()
    {        
        heroManager = GetComponent<HeroManager>();

        // Debug.Log("Setting agent");

        agent = GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        agent.speed = HeroSettings.walkSpeed;
        moveSpeed = agent.speed; // just simply setting defaults to w/e the agent's speed is (which is coincidentally set from HeroSettings)
        
        spawnPos = transform.position;

        SetPathMode(EnumHandler.pathModes.RANDOM);
    }

    void Update()
    {
        switch (pathMode)
        {
            case EnumHandler.pathModes.RANDOM:
                ProcessRandomPathing();
                break;
            case EnumHandler.pathModes.WHISTLE:
                WhistlePathing();
                break;
            case EnumHandler.pathModes.PARTYFOLLOW:
                ProcessPartyFollowPathing();
                break;
            case EnumHandler.pathModes.SENDTOSTARTINGPOINT:
                ProcessPartyRunHomePathing();
                break;
        }

        if (!inBattle)
        {
            HandleMoveSpeed();

            SyncMoveSpeed();
        }
    }

    /// <summary>
    /// Sets the collider set on the heroManager's HomeZone to homeZoneCollider
    /// This is used for finding positions strictly within their home zone's bounds
    /// </summary>
    public void SetCollider()
    {
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.HOME:
                // Debug.Log("Setting home zone collider");
                homeZoneCollider = heroManager.HomeZone().GetComponent<BoxCollider>();
                if (homeZoneCollider == null) { Debug.LogError("SetCollider: NULL"); }
                break;

            case EnumHandler.SceneMode.FIELD:

                break;
        }
    }

    /// <summary>
    /// Turns the navmesh agent on or off.  This is mainly used for transporting heroes between scenes smoothly.
    /// </summary>
    /// <param name="toggle">True to enable the agent, false to disable it</param>
    public void ToggleNavMeshAgent(bool toggle)
    {
        //Debug.Log("Toggling NavMeshAgent: " + toggle);
        if (agent == null) { agent = GetComponent<NavMeshAgent>(); }
        agent.enabled = toggle;
    }

    /// <summary>
    /// Sets the hero's move speed based on the current run mode and distance to their destination.
    /// </summary>
    void HandleMoveSpeed()
    {
        switch (SceneInfo.i.GetSceneMode())
        {
            case EnumHandler.SceneMode.FIELD:
                runMode = EnumHandler.pathRunMode.CANRUN;
                break;
            case EnumHandler.SceneMode.HOME:
                
                break;
        }

        switch (runMode)
        {
            case EnumHandler.pathRunMode.WALK:
                moveSpeed = HeroSettings.walkSpeed;

                break;
            case EnumHandler.pathRunMode.CANRUN:
                // if distance > run distance, run speed
                if (Mathf.Abs(Vector3.Distance(transform.position, agent.destination)) > HeroSettings.walkToTargetDistance)
                {
                    moveSpeed = HeroSettings.runSpeed;
                }
                else
                {
                    moveSpeed = HeroSettings.walkSpeed;
                }
                break;
            case EnumHandler.pathRunMode.CATCHUP:
                if (Mathf.Abs(Vector3.Distance(transform.position, agent.destination)) > HeroSettings.runToCatchupDistance)  // if distance > catchup distance, catchup run speed
                {
                    moveSpeed = HeroSettings.catchupSpeed;
                }
                else if (Mathf.Abs(Vector3.Distance(transform.position, agent.destination)) > HeroSettings.walkToTargetDistance) // else if distance > run distance, can just run now
                {
                    runMode = EnumHandler.pathRunMode.CANRUN;
                }
                break;
        }
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
            //Debug.Log("should be pathing to random pos");
            PathToRandomPosition();
        }
        else if (randomWaiting == false)
        {
            //Debug.Log("waiting");
            CheckForRandomWait();
        }
    }

    /// <summary>
    /// Begins new random pathing procedures.
    /// </summary>
    public void StartNewRandomPathing()
    {
        runMode = EnumHandler.pathRunMode.WALK;

        agent.isStopped = false;

        pathMode = EnumHandler.pathModes.RANDOM;
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
                DebugManager.i.HeroDebugOut("HeroPathing - GetRandomPositionInBounds", heroManager.Hero().GetName() + " hit prefab zone while checking for a point to path to.  Getting a new position.", true, false);
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


    #region PartyPathing

    /// <summary>
    /// Keeps the hero anchored to their anchor on the player's object
    /// </summary>
    void ProcessPartyFollowPathing()
    {
        if (heroManager.HeroParty().GetPartyAnchor() == null)
        {
            // pathing changed. uhh.. we return?
            ProcessPartyRunHomePathing();
            return;
        }
         //Debug.Log("Abs diff between " + transform.position + " and " + heroManager.HeroParty().GetPartyAnchor().GetPosition() + " is " + (Mathf.Abs(Vector3.Distance(transform.position, heroManager.HeroParty().GetPartyAnchor().GetPosition()))));


        if (Mathf.Abs(Vector3.Distance(transform.position, heroManager.HeroParty().GetPartyAnchor().GetPosition())) > HeroSettings.stoppingDistance)
        {
            if (!debuggingPositionSet && NewGameSetup.i.GetDebugging())
            {
                transform.position = heroManager.HeroParty().GetPartyAnchor().GetPosition();
                debuggingPositionSet = true;
            }
            
            agent.SetDestination(heroManager.HeroParty().GetPartyAnchor().GetPosition());
        }
    }

    /// <summary>
    /// Sends the hero back to their home point, and then starts random pathing.
    /// This will eventually be updated to show their current activity.
    /// </summary>
    void ProcessPartyRunHomePathing()
    {
        // Debug.Log("Abs diff between " + transform.position + " and " + spawnPos + " is " + (Mathf.Abs(Vector3.Distance(transform.position, spawnPos))));

        if (runMode != EnumHandler.pathRunMode.CANRUN) runMode = EnumHandler.pathRunMode.CANRUN;

        if (Mathf.Abs(transform.position.x - spawnPos.x) <= HeroSettings.stoppingDistance && Mathf.Abs(transform.position.z - spawnPos.z) <= HeroSettings.stoppingDistance) // If hero is standing at their spawn position
        {
            // set back to random

            DebugManager.i.PartyDebugOut("HeroPathing", heroManager.Hero().GetName() + ": Starting random pathing from PartyRunHomePathing()");
            StopPathing();
            StartNewRandomPathing();
        }

        if (Mathf.Abs(Vector3.Distance(transform.position, spawnPos)) > HeroSettings.stoppingDistance) // Otherwise, send them to spawn position
        {
            agent.SetDestination(spawnPos);
        }
    }

    /// <summary>
    /// Begins new party pathing procedures.
    /// </summary>
    public void StartPartyPathing()
    {
        agent.isStopped = false;

        pathMode = EnumHandler.pathModes.PARTYFOLLOW;
        // Debug.Log("Setting to PARTYFOLLOW");
    }

    /// <summary>
    /// Begins new party pathing procedures.
    /// </summary>
    public void StartPartyRunHomePathing()
    {
        agent.isStopped = false;

        pathMode = EnumHandler.pathModes.SENDTOSTARTINGPOINT;
    }

    #endregion

    /// <summary>
    /// Immediately sets the hero's position back to their starting position
    /// </summary>
    public void MoveToStartingPosition()
    {
        transform.position = heroManager.GetStartingPosition();
    }

    /// <summary>
    /// Stops the NavMeshAgent from moving and resets Pathing vars
    /// </summary>
    public void StopPathing()
    {
        DebugManager.i.HeroDebugOut("HeroPathing", heroManager.Hero().GetName() + " - Pathing stopped");

        agent.ResetPath();
        agent.isStopped = true;

        agent.velocity = new Vector3(0, 0, 0);

        randPathingActive = false;
        randomWaiting = false;

        whistleTargetWithinRange = false;

        agent.isStopped = false; // maybe wrong place for this?

        pathMode = EnumHandler.pathModes.IDLE;
    }
}

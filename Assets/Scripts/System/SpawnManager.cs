using UnityEngine;

// Purpose: Used to manage the player being forcibly placed at a point in the world (like resetting position when going to next week)
// Directions: Attach to the [System] GameObject
// Other notes: 

public class SpawnManager : MonoBehaviour
{
    Vector3 playerSpawnPosition;
    public Vector3 GetPlayerSpawnPosition() { return playerSpawnPosition; }
    public void SetPlayerSpawnPosition(Vector3 playerSpawnPosition) { this.playerSpawnPosition = playerSpawnPosition; }

    GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        playerSpawnPosition = player.transform.position;
    }

    /// <summary>
    /// Sets the player's position to the position when the game was launched
    /// </summary>
    public void MovePlayerToSpawnPosition()
    {
        player.transform.position = playerSpawnPosition;
    }
}

using NUnit.Framework;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Vector3 playerSpawnPosition;
    public Vector3 GetPlayerSpawnPosition() { return playerSpawnPosition; }
    public void SetPlayerSpawnPosition(Vector3 playerSpawnPosition) { this.playerSpawnPosition = playerSpawnPosition; }

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        playerSpawnPosition = player.transform.position;
    }

    public void MovePlayerToSpawnPosition()
    {
        player.transform.position = playerSpawnPosition;
    }
}

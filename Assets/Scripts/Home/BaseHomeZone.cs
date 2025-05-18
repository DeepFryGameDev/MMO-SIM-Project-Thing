using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class BaseHomeZone : MonoBehaviour
{
    [SerializeField] int heroID;
    public int GetHeroID() { return heroID; }

    [SerializeField] HeroHomeZone heroHomeZone;

    [SerializeField] Vector3 spawnPosition;
    public Vector3 GetSpawnPosition() { return spawnPosition; }
}

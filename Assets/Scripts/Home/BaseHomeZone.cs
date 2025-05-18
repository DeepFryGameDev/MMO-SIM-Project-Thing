using UnityEngine;

// Purpose: Used to allow interaction between heroes and their home zone.  This is what holds the values that are specific to the home zone.
// Directions: Attach it to the top level object for each home zone.
// Other notes: 

public class BaseHomeZone : MonoBehaviour
{
    [Tooltip("ID of the hero that belongs to this home zone")]
    [SerializeField] int heroID;
    public int GetHeroID() { return heroID; }

    [Tooltip("The HeroHomeZone attached to the home zone for this hero")]
    [SerializeField] HeroHomeZone heroHomeZone;

    [Tooltip("Where in this home zone should they spawn?")]
    [SerializeField] Vector3 spawnPosition;
    public Vector3 GetSpawnPosition() { return spawnPosition; }
}

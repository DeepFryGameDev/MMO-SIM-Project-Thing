using System.Collections.Generic;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class PartyManager : MonoBehaviour
{
    [Tooltip("Serialized only for easy viewing in the inspector.  Do not change these. \n These are the heroes currently at home, not in the party.")]
    [SerializeField] List<HeroManager> inactiveHeroes = new List<HeroManager>();

    [Tooltip("Serialized only for easy viewing in the inspector.  Do not change these. \n These are the heroes currently in the party and following the player.")]
    [SerializeField] List<HeroManager> activeHeroes = new List<HeroManager>();

    public static PartyManager i;

    private void Awake()
    {
        i = this;
    }
}

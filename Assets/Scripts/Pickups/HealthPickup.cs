using UnityEngine;

// Purpose: Provides a means of healing the player when they touch a floating health pack in the world
// Directions: Attach to any GameObject that should heal the player when touched
// Other notes: 

public class HealthPickup : MonoBehaviour
{
    [Tooltip("Amount of health to heal when picked up")]
    [SerializeField] int healValue;

    BaseHero player; // used to manipulate player's health

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseHero>();
    }
}

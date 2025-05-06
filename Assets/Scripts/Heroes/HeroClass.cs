using UnityEngine;

// Purpose: Houses all details related specifically to the hero's class.
// Directions: Attach to each Hero Object and reference it from the heroManager
// Other notes: 

public class HeroClass : MonoBehaviour
{
    [SerializeField] HeroClassDetails currentClass;

    public HeroClassDetails GetCurrentClass() { return currentClass; }
}

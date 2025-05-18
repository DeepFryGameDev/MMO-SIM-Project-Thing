using UnityEngine;

// Purpose: Houses all details related specifically to the hero's class.
// Directions: Attach to each Hero Object and reference it from the heroManager
// Other notes: 

public class HeroClass : MonoBehaviour
{
    int level;
    public int GetLevel() { return level; }
    public void SetLevel(int level) { this.level = level; }

    [SerializeField] HeroClassDetails currentClass;

    public HeroClassDetails GetCurrentClass() { return currentClass; }

    private void Awake()
    {
        SetLevel(1); // this will be updated to use HeroSettings or some other static class to hold the levels
    }
}

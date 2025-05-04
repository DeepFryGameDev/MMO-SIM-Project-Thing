using UnityEngine;

public class HeroClass : MonoBehaviour
{
    [SerializeField] HeroClassDetails currentClass;

    public HeroClassDetails GetCurrentClass() { return currentClass; }
}

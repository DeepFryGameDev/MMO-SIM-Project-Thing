using UnityEngine;

// Do we need this?

public class MovementManager : MonoBehaviour
{
    InputSubscription GetInput;

    private void Awake()
    {
        {
            GetInput = GetComponent<InputSubscription>();
        }
    }
}

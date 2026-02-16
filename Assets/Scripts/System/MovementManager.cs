using UnityEngine;

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

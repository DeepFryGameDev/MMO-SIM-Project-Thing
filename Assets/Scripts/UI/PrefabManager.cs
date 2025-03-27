using UnityEngine;
using UnityEngine.UI;

// Purpose: Contains prefabs to be manipulated by script
// Directions: Attach to [UI] object and call PrefabManager.i to reference prefabs
// Other notes:

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager i;

    [Tooltip("Set this to the prefab for TrainingResults to be shown when advancing weeks")]
    public GameObject TrainingResult;

    private void Awake()
    {
        Singleton();
    }

    void Singleton()
    {
        if (i == null) //check if instance exists
        {
            i = this; //if not set the instance to this
        }
        else if (i != this) //if it exists but is not this instance
        {
            Destroy(gameObject); //destroy it
        }
        DontDestroyOnLoad(gameObject); //set this to be persistable across scenes
    }
}

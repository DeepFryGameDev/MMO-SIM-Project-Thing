using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSpawner : MonoBehaviour
{
    void Start()
    {
        EventSystem sceneEventSystem = FindAnyObjectByType<EventSystem>();
        if (sceneEventSystem == null)
        {
            // Create a new EventSystem if none is found
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            // Add the new input module if using the new Input System
            // eventSystem.AddComponent<InputSystemUIInputModule>(); 
        }
        else if (sceneEventSystem.gameObject != this.gameObject)
        {
            // If another one exists, destroy this one
            Destroy(this.gameObject);
        }
    }
}

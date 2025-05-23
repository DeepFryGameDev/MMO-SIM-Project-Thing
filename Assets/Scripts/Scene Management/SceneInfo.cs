using UnityEngine;

// Purpose: This will be used to give various information about the scene so Unity knows how to handle various requests by the user (such as opening the menu).
// Directions: Create a [SceneInfo] object in the editor, and attach this to it.
// Other notes:

public class SceneInfo : MonoBehaviour
{
    public static SceneInfo i;

    [SerializeField] EnumHandler.SceneMode sceneMode;
    public EnumHandler.SceneMode GetSceneMode() { return sceneMode; }

    private void Awake()
    {
        i = this;
    }
}

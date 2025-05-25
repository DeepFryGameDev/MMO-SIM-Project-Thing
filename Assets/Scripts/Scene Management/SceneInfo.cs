using UnityEngine;

// Purpose: This will be used to give various information about the scene so Unity knows how to handle various requests by the user (such as opening the menu).
// Directions: Create a [SceneInfo] object in the editor, and attach this to it.
// Other notes:

public class SceneInfo : MonoBehaviour
{
    public static SceneInfo i;

    [SerializeField] EnumHandler.SceneMode sceneMode;
    /// <summary>
    /// If the current scene you are in is a field, this will return FIELD.  Likewise for home.
    /// </summary>
    /// <returns>FIELD or HOME</returns>
    public EnumHandler.SceneMode GetSceneMode() { return sceneMode; }
    public void SetSceneMode(EnumHandler.SceneMode sceneMode) { this.sceneMode = sceneMode; }

    private void Awake()
    {
        i = this;
    }
}

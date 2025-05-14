using System.Collections;
using UnityEngine;

// Purpose: Unsure if this is needed yet.
// Directions: Create a SceneInfo object in the editor, and attach this to it.
// Other notes:

public class SceneInfo : MonoBehaviour
{
    public static SceneInfo i;

    [SerializeField] EnumHandler.MenuMode menuMode;
    public EnumHandler.MenuMode GetMenuMode() { return menuMode; }

    private void Awake()
    {
        i = this;
    }
}

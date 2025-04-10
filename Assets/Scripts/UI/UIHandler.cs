using UnityEngine;
using UnityEngine.UI;

// Purpose: Handles various UI related processes
// Directions: Attach to [UI]
// Other notes:

public class UIHandler : MonoBehaviour
{
    void Awake()
    {

    }

    /// <summary>
    /// Toggles the given panel as active or inactive
    /// </summary>
    /// <param name="Panel">Panel to toggle</param>
    /// <param name="toggle">True: Panel is active/displayed - False: Panel is inactive/hidden</param>
    public void ToggleObject(GameObject Panel, bool toggle)
    {
        Panel.SetActive(toggle);
    }
}

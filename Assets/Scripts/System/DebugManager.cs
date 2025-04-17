using Unity.VisualScripting;
using UnityEngine;

// Purpose: Used for when debugging out to the console and other QOL functions
// Directions: Attach to the [System] GameObject.
// Other notes: 

public class DebugManager : MonoBehaviour
{
    public static DebugManager i;

    private void Awake()
    {
        i = this;
    }

    /// <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    /// <param name="warning">If the output should show a warning</param>
    /// <param name="error">If the output should show an error</param>
    public void UIDebugOut(string header, string debugOutput, bool warning, bool error)
    {
        if (error)
        {
            Debug.LogError("<#" + DebugSettings.uiDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else if (warning)
        {
            Debug.LogWarning("<#" + DebugSettings.uiDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else
        {
            Debug.Log("<#" + DebugSettings.uiDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
    }

    /// <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    public void UIDebugOut(string header, string debugOutput)
    {
        Debug.Log("<#" + DebugSettings.uiDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    /// <param name="warning">If the output should show a warning</param>
    /// <param name="error">If the output should show an error</param>
    public void ScheduleDebugOut(string header, string debugOutput, bool warning, bool error)
    {
        if (error)
        {
            Debug.LogError("<#" + DebugSettings.scheduleDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else if (warning)
        {
            Debug.LogWarning("<#" + DebugSettings.scheduleDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else
        {
            Debug.Log("<#" + DebugSettings.scheduleDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    public void ScheduleDebugOut(string header, string debugOutput)
    {
        Debug.Log("<#" + DebugSettings.scheduleDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    /// <param name="warning">If the output should show a warning</param>
    /// <param name="error">If the output should show an error</param>
    public void HeroDebugOut(string header, string debugOutput, bool warning, bool error)
    {
        if (error)
        {
            Debug.LogError("<#" + DebugSettings.heroDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else if (warning)
        {
            Debug.LogWarning("<#" + DebugSettings.heroDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else
        {
            Debug.Log("<#" + DebugSettings.heroDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    public void HeroDebugOut(string header, string debugOutput)
    {
        Debug.Log("<#" + DebugSettings.heroDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    /// <param name="warning">If the output should show a warning</param>
    /// <param name="error">If the output should show an error</param>
    public void SystemDebugOut(string header, string debugOutput, bool warning, bool error)
    {
        if (error)
        {
            Debug.LogError("<#" + DebugSettings.systemDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else if (warning)
        {
            Debug.LogWarning("<#" + DebugSettings.systemDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else
        {
            Debug.Log("<#" + DebugSettings.systemDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    public void SystemDebugOut(string header, string debugOutput)
    {
        Debug.Log("<#" + DebugSettings.systemDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    /// <param name="warning">If the output should show a warning</param>
    /// <param name="error">If the output should show an error</param>
    public void PartyDebugOut(string header, string debugOutput, bool warning, bool error)
    {
        if (error)
        {
            Debug.LogError("<#" + DebugSettings.partyDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else if (warning)
        {
            Debug.LogWarning("<#" + DebugSettings.partyDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
        else
        {
            Debug.Log("<#" + DebugSettings.partyDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
        }
    }

    // <summary>
    /// Displays a debug log with UI font colors and other customizations - NOTE: Only mark one of the bools as true - if they are both true, it will default to error
    /// </summary>
    /// <param name="debugOutput">The string to output</param>
    public void PartyDebugOut(string header, string debugOutput)
    {
        Debug.Log("<#" + DebugSettings.partyDebugColor.ToHexString() + ">[" + header + "]</color> " + debugOutput);
    }
}

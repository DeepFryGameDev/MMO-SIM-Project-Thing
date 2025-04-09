using Unity.VisualScripting;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes:

public class RestScheduleEvent : ScheduleEvent
{
    string name = "Rest";

    public RestScheduleEvent()
    {
        SetName(name);
        SetID(0);
    }
}

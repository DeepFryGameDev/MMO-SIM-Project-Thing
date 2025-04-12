
// Purpose: The class for creating a new rest event.
// Directions: Create a new RestScheduleEvent with the schedule system.  Generally used with ScheduleManager.i.CreateScheduleEventByID()
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

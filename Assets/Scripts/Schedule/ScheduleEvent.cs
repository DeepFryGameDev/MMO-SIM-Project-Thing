
// Purpose: The Base Class for any events to be scheduled - rest, training, etc.
// Directions: Create a new class for one of the derived classes of this - RestScheduleEvent or TrainingScheduleEvent.  CraftingScheduleEvent will also use this.
// Other notes:

public class ScheduleEvent
{
    string name;
    public void SetName(string name) { this.name = name; }
    public string GetName() { return name; }

    int ID;
    public void SetID(int ID) { this.ID = ID; }
    public int GetID() { return ID; }
}

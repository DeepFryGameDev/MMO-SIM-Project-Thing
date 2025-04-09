using UnityEngine;

// Purpose: 
// Directions: 
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

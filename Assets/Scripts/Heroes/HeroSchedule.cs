using JetBrains.Annotations;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroSchedule : MonoBehaviour
{
    ScheduleEvent[] scheduleEvents = new ScheduleEvent[8];
    public ScheduleEvent[] GetScheduleEvents() { return scheduleEvents; }

    ScheduleEvent currentEvent;
    public ScheduleEvent GetCurrentEvent() { return currentEvent; }

    public TrainingScheduleEvent GetCurrentEventAsTraining() { return currentEvent as TrainingScheduleEvent; }

    HeroManager heroManager;
    ScheduleManager scheduleManager;

    private void Awake()
    {
        heroManager = transform.GetComponent<HeroManager>();

        scheduleManager = FindFirstObjectByType<ScheduleManager>();
    }

    private void Start()
    {
        SetDefaultSchedule();
        SetCurrentEvent();
    }

    void SetDefaultSchedule()
    {
        // starting out, every week should be resting.

        for (int i = 0; i <= 7; i++)
        {
            RestScheduleEvent restScheduleEvent = new RestScheduleEvent();

            SetScheduleSlot(i, restScheduleEvent);
        }
    }

    public void SetScheduleSlot(int slot, ScheduleEvent scheduleEvent)
    {
        scheduleEvents[slot] = scheduleEvent;

        Debug.Log("Setting " + slot + " to " + scheduleEvent.GetName());

        if (slot == 0) SetCurrentEvent();
    }

    public void SetCurrentEvent()
    {
        currentEvent = scheduleEvents[0];
    }
}

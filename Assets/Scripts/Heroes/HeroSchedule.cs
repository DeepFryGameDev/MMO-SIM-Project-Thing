using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroSchedule : MonoBehaviour
{
    ScheduleEvent[] scheduleEvents = new ScheduleEvent[8];
    public ScheduleEvent[] GetScheduleEvents() { return scheduleEvents; }

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
    }

    void SetDefaultSchedule()
    {
        
    }
}

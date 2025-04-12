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

    RestingResultHandler tempRRH; // Used to access the Resting Result Handler script attached to the resting result for the week.
    public void SetTempRRH(RestingResultHandler rrh) { tempRRH = rrh; }
    public RestingResultHandler GetTempRRH() { return tempRRH; }

    // for testing for now.
    [SerializeField] EnumHandler.TrainingTypes defaultTraining;
    [SerializeField] bool defaultRestInstead;

    private void Awake()
    {
        heroManager = transform.GetComponent<HeroManager>();
    }

    private void Start()
    {
        SetDefaultSchedule();
        
        SetCurrentEvent();

        Debug.Log("-~*~-Default event for " + heroManager.Hero().GetName() + " is " + currentEvent.GetName() + "-~*~-");
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

        //Debug.Log("Setting " + slot + " to " + scheduleEvent.GetName());

        if (slot == 0) SetCurrentEvent();
    }

    public void SetCurrentEvent()
    {
        if (!defaultRestInstead)
        {
            switch (defaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        } else // rest
        {
            scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        currentEvent = scheduleEvents[0];
    }

    public void RollForwardSchedule()
    {
        /*Debug.Log("-----RollForwardSchedule Before-----");
        Debug.Log("RollForwardSchedule 0: " + scheduleEvents[0].GetName());
        Debug.Log("RollForwardSchedule 1: " + scheduleEvents[1].GetName());
        Debug.Log("RollForwardSchedule 2: " + scheduleEvents[2].GetName());
        Debug.Log("RollForwardSchedule 3: " + scheduleEvents[3].GetName());
        Debug.Log("RollForwardSchedule 4: " + scheduleEvents[4].GetName());
        Debug.Log("RollForwardSchedule 5: " + scheduleEvents[5].GetName());
        Debug.Log("RollForwardSchedule 6: " + scheduleEvents[6].GetName());
        Debug.Log("RollForwardSchedule 7: " + scheduleEvents[7].GetName());*/

        ScheduleEvent tempScheduleEvent = scheduleEvents[0];

        SetScheduleSlot(0, scheduleEvents[1]);
        SetCurrentEvent();

        SetScheduleSlot(1, scheduleEvents[2]);
        SetScheduleSlot(2, scheduleEvents[3]);
        SetScheduleSlot(3, scheduleEvents[4]);
        SetScheduleSlot(4, scheduleEvents[5]);
        SetScheduleSlot(5, scheduleEvents[6]);
        SetScheduleSlot(6, scheduleEvents[7]);
        SetScheduleSlot(7, tempScheduleEvent);


        /*Debug.Log("-----RollForwardSchedule After-----");
        Debug.Log("RollForwardSchedule 0: " + scheduleEvents[0].GetName());
        Debug.Log("RollForwardSchedule 1: " + scheduleEvents[1].GetName());
        Debug.Log("RollForwardSchedule 2: " + scheduleEvents[2].GetName());
        Debug.Log("RollForwardSchedule 3: " + scheduleEvents[3].GetName());
        Debug.Log("RollForwardSchedule 4: " + scheduleEvents[4].GetName());
        Debug.Log("RollForwardSchedule 5: " + scheduleEvents[5].GetName());
        Debug.Log("RollForwardSchedule 6: " + scheduleEvents[6].GetName());
        Debug.Log("RollForwardSchedule 7: " + scheduleEvents[7].GetName());*/
    }
}

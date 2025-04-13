using UnityEngine;

// Purpose: Manages the interactions between each hero and their schedule
// Directions: Attach to each hero object
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
    [SerializeField] EnumHandler.TrainingTypes week2DefaultTraining;
    [SerializeField] bool week2DefaultRestInstead;
    [SerializeField] EnumHandler.TrainingTypes week3DefaultTraining;
    [SerializeField] bool week3DefaultRestInstead;
    [SerializeField] EnumHandler.TrainingTypes week4DefaultTraining;
    [SerializeField] bool week4DefaultRestInstead;
    [SerializeField] EnumHandler.TrainingTypes week5DefaultTraining;
    [SerializeField] bool week5DefaultRestInstead;
    [SerializeField] EnumHandler.TrainingTypes week6DefaultTraining;
    [SerializeField] bool week6DefaultRestInstead;
    [SerializeField] EnumHandler.TrainingTypes week7DefaultTraining;
    [SerializeField] bool week7DefaultRestInstead;
    [SerializeField] EnumHandler.TrainingTypes week8DefaultTraining;
    [SerializeField] bool week8DefaultRestInstead;

    private void Awake()
    {
        heroManager = transform.GetComponent<HeroManager>();
    }

    private void Start()
    {
        SetDefaultSchedule();

        SetDefaultEventsForTesting();

        SetCurrentEvent();

        // Debug.Log("-~*~-Default event for " + heroManager.Hero().GetName() + " is " + currentEvent.GetName() + "-~*~-");
    }

    /// <summary>
    /// Just used for testing - in the inspector, each hero can have a default training set.
    /// </summary>
    void SetDefaultEventsForTesting()
    {
        // Week 1 (current week)
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
        }
        else // rest
        {
            scheduleEvents[0] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 2
        if (!week2DefaultRestInstead)
        {
            switch (week2DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[1] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 3
        if (!week3DefaultRestInstead)
        {
            switch (week3DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[2] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 4
        if (!week4DefaultRestInstead)
        {
            switch (week4DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[3] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 5
        if (!week5DefaultRestInstead)
        {
            switch (week5DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[4] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 6
        if (!week6DefaultRestInstead)
        {
            switch (week6DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[5] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 7
        if (!week7DefaultRestInstead)
        {
            switch (week7DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[6] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }

        // Week 8
        if (!week8DefaultRestInstead)
        {
            switch (week8DefaultTraining)
            {
                case EnumHandler.TrainingTypes.STRENGTH:
                    scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(1);
                    break;
                case EnumHandler.TrainingTypes.ENDURANCE:
                    scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(2);
                    break;
                case EnumHandler.TrainingTypes.AGILITY:
                    scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(3);
                    break;
                case EnumHandler.TrainingTypes.DEXTERITY:
                    scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(4);
                    break;
                case EnumHandler.TrainingTypes.INTELLIGENCE:
                    scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(5);
                    break;
                case EnumHandler.TrainingTypes.FAITH:
                    scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(6);
                    break;
            }
        }
        else // rest
        {
            scheduleEvents[7] = ScheduleManager.i.CreateScheduleEventByEventID(0);
        }
    }

    /// <summary>
    /// This will be adjusted in the future maybe.  For now, every week should be defaulted to resting.
    /// </summary>
    void SetDefaultSchedule()
    {
        for (int i = 0; i <= 7; i++)
        {
            RestScheduleEvent restScheduleEvent = new RestScheduleEvent();

            SetScheduleSlot(i, restScheduleEvent);
        }
    }

    /// <summary>
    /// Sets the designated schedule slot with the given schedule event.
    /// </summary>
    /// <param name="slot">Slot of the schedule that the event should be inserted</param>
    /// <param name="scheduleEvent">The schedule event to be inserted</param>
    public void SetScheduleSlot(int slot, ScheduleEvent scheduleEvent)
    {
        scheduleEvents[slot] = scheduleEvent;

        //Debug.Log("Setting " + slot + " to " + scheduleEvent.GetName());

        if (slot == 0) SetCurrentEvent();
    }


    /// <summary>
    /// Sets the current event val based on the scheduleEvents value at index 0.
    /// </summary>
    public void SetCurrentEvent()
    {
        currentEvent = scheduleEvents[0];
    }

    /// <summary>
    /// Sets all events to one week forward - used when the next week is rolled over, so all events are rolled over as well.
    /// </summary>
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

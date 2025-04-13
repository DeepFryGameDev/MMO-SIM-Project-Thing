using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose: Handles functionality of the schedule, as well as basic resting.  Maybe the rest could be moved to a new script?  It's so small right now.
// Directions: Attach to the [System] object
// Other notes: 

public class ScheduleManager : MonoBehaviour
{
    // For training gen
    string level1BasicStrengthTrainingName = "Lv. 1) Basic Strength Training";
    public string GetLevel1BasicStrengthTrainingName() { return level1BasicStrengthTrainingName; }
    string level1BasicEnduranceTrainingName = "Lv. 1) Basic Endurance Training";
    public string GetLevel1BasicEnduranceTrainingName() { return level1BasicEnduranceTrainingName; }
    string level1BasicAgilityTrainingName = "Lv. 1) Basic Agility Training";
    public string GetLevel1BasicAgilityTrainingName() { return level1BasicAgilityTrainingName; }
    string level1BasicDexterityTrainingName = "Lv. 1) Basic Dexterity Training";
    public string GetLevel1BasicDexterityTrainingName() { return level1BasicDexterityTrainingName; }
    string level1BasicIntelligenceTrainingName = "Lv. 1) Basic Intelligence Training";
    public string GetLevel1BasicIntelligenceTrainingName() { return level1BasicIntelligenceTrainingName; }
    string level1BasicFaithTrainingName = "Lv. 1) Basic Faith Training";
    public string GetLevel1BasicFaithTrainingName() { return level1BasicFaithTrainingName; }

    Transform layoutGroupTransform; // Used to add TrainingResults to the UI

    public static ScheduleManager i;

    private void Awake()
    {
        layoutGroupTransform = GameObject.Find("TrainingResultsCanvas/Holder/LayoutGroup").transform; // hacky, will need a better solution eventually

        i = this;
    }

    /// <summary>
    /// When the hero has 'rest' scheduled, this is 
    /// </summary>
    /// <param name="heroManager"></param>
    public void ProcessResting(HeroManager heroManager)
    {
        // Debug.Log("Need to rest");

        heroManager.HeroTraining().SetTempEnergy(heroManager.Hero().GetEnergy());

        // Increase energy
        heroManager.Hero().SetEnergy(heroManager.Hero().GetEnergy() + ScheduleSettings.baseEnergyRestoredFromRest);

        if (heroManager.Hero().GetEnergy() > HeroSettings.maxEnergy)
        {
            heroManager.Hero().SetEnergy(HeroSettings.maxEnergy);
        }
    }

    /// <summary>
    /// Returns which slots the hero has currently scheduled using the given event ID
    /// </summary>
    /// <param name="ID">ID of the event to check</param>
    /// <returns>A List of ints of which slots are currently set to the given event ID</returns>
    public List<int> GetTrainingSlotsFromEventID(int ID)
    {
        /*Debug.Log("Checking " + ID);

        Debug.Log("1) " + heroManager.HeroSchedule().GetScheduleEvents()[1].GetID());
        Debug.Log("2) " + heroManager.HeroSchedule().GetScheduleEvents()[2].GetID());
        Debug.Log("3) " + heroManager.HeroSchedule().GetScheduleEvents()[3].GetID());
        Debug.Log("4) " + heroManager.HeroSchedule().GetScheduleEvents()[4].GetID());
        Debug.Log("5) " + heroManager.HeroSchedule().GetScheduleEvents()[5].GetID());
        Debug.Log("6) " + heroManager.HeroSchedule().GetScheduleEvents()[6].GetID());
        Debug.Log("7) " + heroManager.HeroSchedule().GetScheduleEvents()[7].GetID());*/

        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[1] == null) { Debug.Log("Null for some reason"); }
        List<int> slots = new List<int>();

        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[1].GetID() == ID) slots.Add(1);
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[2].GetID() == ID) slots.Add(2);
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[3].GetID() == ID) slots.Add(3);
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[4].GetID() == ID) slots.Add(4);
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[5].GetID() == ID) slots.Add(5);
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[6].GetID() == ID) slots.Add(6);
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[7].GetID() == ID) slots.Add(7);

        return slots;
    }

    /// <summary>
    /// probably move to ScheduleManager later?
    /// </summary>
    /// <returns></returns>
    public List<ScheduleEvent> GetAllAvailableScheduleEvents()
    {
        List<ScheduleEvent> allAvailableScheduleEvents = new List<ScheduleEvent>();

        // build all possible events
        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(0)); // rest

        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(1)); // basic strength training
        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(2)); // basic endurance training
        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(3)); // basic agility training
        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(4)); // basic dexterity training
        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(5)); // basic intelligence training
        allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(6)); // basic faith training

        if (HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            //Debug.Log("allAvailableScheduleEvents: Adding EquipmentSlot0: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingName + " - ID: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID);
            allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID));
        }

        if (HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            //Debug.Log("allAvailableScheduleEvents: Adding EquipmentSlot1: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingName + " - ID: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().ID);
            allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().ID));
        }

        /*foreach (ScheduleEvent scheduleEvent in allAvailableScheduleEvents)
        {
            Debug.Log("allAvailableScheduleEvents: " + scheduleEvent.GetName());
        }*/

        return allAvailableScheduleEvents;
    }

    /// <summary>
    /// move this to scheduleManager too
    /// </summary>
    /// <param name="scheduleEventName"></param>
    /// <returns></returns>
    public int GetEventIDByScheduleEventName(string scheduleEventName)
    {
        // Debug.Log("Checking GetEventIDByScheduleEventName - " + scheduleEventName);

        // build full available schedule list
        List<ScheduleEvent> availableScheduleEvents = GetAllAvailableScheduleEvents();

        // need to check all available schedule options
        foreach (ScheduleEvent scheduleEvent in availableScheduleEvents)
        {
            // Debug.Log("GetEventIDByScheduleEventName() - Comparing eventName: " + scheduleEvent.GetName() + " with eventName: " + scheduleEventName);
            if (scheduleEvent.GetName() == scheduleEventName)
            {
                //Debug.LogWarning("GetEventIDByScheduleEventName() found " +  scheduleEventName + ", returning ID: " + scheduleEvent.GetID());
                return scheduleEvent.GetID();
            }
        }

        Debug.Log("GetEventIDByScheduleEventName - Returning rest");
        return 0;
    }

    /// <summary>
    /// Creates a new schedule event by the given ID.  If the given ID is from a training equipment slot, that will be returned first.
    /// <para>0: Rest</para>
    /// <para>1: Basic Strength Training</para>
    /// <para>2: Basic Endurance Training</para>
    /// <para>3: Basic Agility Training</para>
    /// <para>4: Basic Dexterity Training</para>
    /// <para>5: Basic Intelligence Training</para>
    /// <para>6. Basic Faith Training</para>
    /// </summary>
    /// <param name="ID">ID of the schedule event to be created.</param>
    /// <returns>A new schedule event by the ID given.</returns>
    public ScheduleEvent CreateScheduleEventByEventID(int ID) 
    {
        // Debug.Log("CreateScheduleEventByEventID - Checking ID: " + ID + " compared to " + heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID);
        if (HomeZoneManager.i.GetHeroManager() != null && HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null && 
            (HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID == ID))
        {
            Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: From Equipment Slot 0 - " + HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingName);
            return GetTrainingEventFromEquipmentSlot(0);
        }

        if (HomeZoneManager.i.GetHeroManager() != null && HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null && 
            (HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().ID == ID))
        {
            Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: From Equipment Slot 1 - " + HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingName);
            return GetTrainingEventFromEquipmentSlot(1);
        }
            

        switch (ID)
        {
            case 0: // REST
                //Debug.Log("returning a rest event");
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Rest event created");
                return new RestScheduleEvent();
            case 1: // BASIC STRENGTH TRAINING
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: Basic Strength Training");
                return GetBasicStrengthTrainingScheduleEvent();
            case 2: // BASIC ENDURANCE TRAINING
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: Basic Endurance Training");
                return GetBasicEnduranceTrainingScheduleEvent();
            case 3: // BASIC AGILITY TRAINING
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: Basic Agility Training");
                return GetBasicAgilityTrainingScheduleEvent();
            case 4: // BASIC DEXTERITY TRAINING
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: Basic Dexterity Training");
                return GetBasicDexterityTrainingScheduleEvent();
            case 5: // BASIC INTELLIGENCE TRAINING
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: Basic Intelligence Training");
                return GetBasicIntelligenceTrainingScheduleEvent();
            case 6: // BASIC FAITH TRAINING
                //Debug.Log("<color=blue>[ScheduleManager]</color> CreateScheduleEventByID: Event created: Basic Faith Training");
                return GetBasicFaithTrainingScheduleEvent();
            default:
                return null;
        }
    }

    /// <summary>
    /// Coroutine - Displays the training results to the player with animated fill bars
    /// </summary>
    /// <returns>Yields for DateSettings.trainingResultsFillDelaySeconds and then animates the fill bars</returns>
    public IEnumerator ShowRestingResults(HeroManager heroManager)
    {
        // Create new TrainingResult prefab and instantiate it in the LayoutGroup.
        GameObject newRestingResult = Instantiate(PrefabManager.i.RestingResult, layoutGroupTransform);

        RestingResultHandler rrh = newRestingResult.GetComponent<RestingResultHandler>();

        heroManager.HeroSchedule().SetTempRRH(rrh);

        // Set facePanel to hero face
        rrh.SetFaceImage(heroManager.GetFaceImage());

        // set EnergyFill to (hero's current energy / hero's max energy)
        float energyFillVal = (float)heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy;
        // Debug.Log(heroManager.HeroTraining().GetTempEnergy() + " / " + HeroSettings.maxEnergy + " = " + ((float)heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy));

        rrh.SetEnergyFill(energyFillVal);
        rrh.SetEnergyFillText(heroManager.HeroTraining().GetTempEnergy() + " / " + HeroSettings.maxEnergy);

        // show panel after x seconds
        StartCoroutine(TrainingManager.i.DisplayResultPanelAfterDelay());

        // wait x seconds
        yield return new WaitForSeconds(DateSettings.trainingResultsFillDelaySeconds);

        // fill bars and update TotalExpText over x seconds
        StartCoroutine(UpdateEnergyFill(heroManager));

        yield return new WaitForSeconds(DateSettings.trainingResultShowSeconds - DateSettings.trainingResultsDelaySeconds);

        TrainingManager.i.ToggleCanvasGroup(false);

        // reset stuff
        TrainingManager.i.ResetVars();
    }

    /// <summary>
    /// Coroutine - Fills the energy bars (should move this eventually..)
    /// </summary>
    /// <param name="heroManager">HeroManager for the hero to have energy bars filled</param>
    /// <returns>Fills the bar over DateSettings.trainingResultsFillSeconds</returns>
    IEnumerator UpdateEnergyFill(HeroManager heroManager)
    {
        float timer = 0;

        float startEnergy = ((float)heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy);

        //Debug.Log("StartEnergy : " + startEnergy);

        float tempFillBarEnergy = startEnergy;

        // over DateSettings.trainingResultsFillSeconds, reduce the energy bar
        while (timer < DateSettings.trainingResultsFillSeconds)
        {
            timer += Time.deltaTime;
            float fillPercent = timer / DateSettings.trainingResultsFillSeconds;

            //fillPercent = 1 - fillPercent;

            tempFillBarEnergy = startEnergy + ((ScheduleSettings.baseEnergyRestoredFromRest * .01f) * fillPercent);

            heroManager.HeroSchedule().GetTempRRH().SetEnergyFill(tempFillBarEnergy);

            float tempFillTextEnergy = ScheduleSettings.baseEnergyRestoredFromRest * fillPercent;

            int roundedTempEnergy = Mathf.RoundToInt(tempFillTextEnergy) + heroManager.HeroTraining().GetTempEnergy();
            if (roundedTempEnergy > HeroSettings.maxEnergy ) { roundedTempEnergy = HeroSettings.maxEnergy; }
            heroManager.HeroSchedule().GetTempRRH().SetEnergyFillText(roundedTempEnergy + " / " + HeroSettings.maxEnergy);

            yield return new WaitForEndOfFrame();
        }
    }


    #region DEFINE LEVEL 1 BASIC TRAININGS HERE - check ScheduleMenuHandler for IDs if needed

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the Basic Strength Training parameters.
    /// </summary>
    /// <returns>TrainingScheduleEvent for a basic strength training</returns>
    public TrainingScheduleEvent GetBasicStrengthTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetName(GetLevel1BasicStrengthTrainingName());
        trainingScheduleEvent.SetTrainingName(GetLevel1BasicStrengthTrainingName());

        trainingScheduleEvent.SetID(1);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.STRENGTH);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the Basic Endurance Training parameters.
    /// </summary>
    /// <returns>TrainingScheduleEvent for a basic endurance training</returns>
    public TrainingScheduleEvent GetBasicEnduranceTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetName(GetLevel1BasicEnduranceTrainingName());
        trainingScheduleEvent.SetTrainingName(GetLevel1BasicEnduranceTrainingName());

        trainingScheduleEvent.SetID(2);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.ENDURANCE);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the Basic Agility Training parameters.
    /// </summary>
    /// <returns>TrainingScheduleEvent for a basic agility training</returns>
    public TrainingScheduleEvent GetBasicAgilityTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetName(GetLevel1BasicAgilityTrainingName());
        trainingScheduleEvent.SetTrainingName(GetLevel1BasicAgilityTrainingName());

        trainingScheduleEvent.SetID(3);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.AGILITY);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the Basic Dexterity Training parameters.
    /// </summary>
    /// <returns>TrainingScheduleEvent for a basic dexterity training</returns>
    public TrainingScheduleEvent GetBasicDexterityTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetName(GetLevel1BasicDexterityTrainingName());
        trainingScheduleEvent.SetTrainingName(GetLevel1BasicDexterityTrainingName());

        trainingScheduleEvent.SetID(4);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.DEXTERITY);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the Basic Intelligence Training parameters.
    /// </summary>
    /// <returns>TrainingScheduleEvent for a basic intelligence training</returns>
    public TrainingScheduleEvent GetBasicIntelligenceTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetName(GetLevel1BasicIntelligenceTrainingName());
        trainingScheduleEvent.SetTrainingName(GetLevel1BasicIntelligenceTrainingName());

        trainingScheduleEvent.SetID(5);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.INTELLIGENCE);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the Basic Faith Training parameters.
    /// </summary>
    /// <returns>TrainingScheduleEvent for a basic faith training</returns>
    public TrainingScheduleEvent GetBasicFaithTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetName(GetLevel1BasicFaithTrainingName());
        trainingScheduleEvent.SetTrainingName(GetLevel1BasicFaithTrainingName());

        trainingScheduleEvent.SetID(6);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.FAITH);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    #endregion

    #region OTHER TRAINING GENS HERE

    /// <summary>
    /// Returns a new TrainingScheduleEvent using the parameters contained in the equipped Training Equipment
    /// </summary>
    /// <param name="slot">Which slot is the training equipment equipped to?</param>
    /// <returns>A new TrainingScheduleEvent using the equipment contained within the given slot</returns>
    public TrainingScheduleEvent GetTrainingEventFromEquipmentSlot(int slot)
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();
        TrainingEquipment trainingEquip = HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentBySlot(slot);

        trainingScheduleEvent.SetName("Lv. " + trainingEquip.trainingLevel + ") " + trainingEquip.trainingName);
        trainingScheduleEvent.SetTrainingName("Lv. " + trainingEquip.trainingLevel + ") " + trainingEquip.trainingName);

        trainingScheduleEvent.SetID(trainingEquip.ID);

        trainingScheduleEvent.SetTrainingLevel(trainingEquip.trainingLevel);
        trainingScheduleEvent.SetTrainingType(trainingEquip.trainingType);

        return trainingScheduleEvent;
    }

    #endregion
}

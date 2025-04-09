using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class ScheduleManager : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    TrainingManager trainingManager;

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

    private void Awake()
    {
        trainingManager = transform.GetComponent<TrainingManager>();

        layoutGroupTransform = GameObject.Find("TrainingResultsCanvas/Holder/LayoutGroup").transform; // hacky, will need a better solution eventually
    }

    public void ProcessResting(HeroManager heroManager)
    {
        Debug.Log("Need to rest");

        heroManager.HeroTraining().SetTempEnergy(heroManager.Hero().GetEnergy());

        // Increase energy
        heroManager.Hero().SetEnergy(heroManager.Hero().GetEnergy() + ScheduleSettings.BaseEnergyRestoredFromRest);

        if (heroManager.Hero().GetEnergy() > HeroSettings.maxEnergy)
        {
            heroManager.Hero().SetEnergy(HeroSettings.maxEnergy);
        }
    }

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

        if (heroManager.HeroSchedule().GetScheduleEvents()[1] == null) { Debug.Log("Null for some reason"); }
        List<int> slots = new List<int>();

        if (heroManager.HeroSchedule().GetScheduleEvents()[1].GetID() == ID) slots.Add(1);
        if (heroManager.HeroSchedule().GetScheduleEvents()[2].GetID() == ID) slots.Add(2);
        if (heroManager.HeroSchedule().GetScheduleEvents()[3].GetID() == ID) slots.Add(3);
        if (heroManager.HeroSchedule().GetScheduleEvents()[4].GetID() == ID) slots.Add(4);
        if (heroManager.HeroSchedule().GetScheduleEvents()[5].GetID() == ID) slots.Add(5);
        if (heroManager.HeroSchedule().GetScheduleEvents()[6].GetID() == ID) slots.Add(6);
        if (heroManager.HeroSchedule().GetScheduleEvents()[7].GetID() == ID) slots.Add(7);

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

        if (GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            //Debug.Log("allAvailableScheduleEvents: Adding EquipmentSlot0: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingName + " - ID: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID);
            allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID));
        }

        if (GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            //Debug.Log("allAvailableScheduleEvents: Adding EquipmentSlot1: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingName + " - ID: " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().ID);
            allAvailableScheduleEvents.Add(CreateScheduleEventByEventID(GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().ID));
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
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public ScheduleEvent CreateScheduleEventByEventID(int ID) 
    {
        // Debug.Log("CreateScheduleEventByEventID - Checking ID: " + ID + " compared to " + heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID);
        if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null && heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot0().ID == ID)
        {
            return GetTrainingEventFromEquipmentSlot(0);
        }

        if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null && heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot1().ID == ID)
            return GetTrainingEventFromEquipmentSlot(1);

        switch (ID)
        {
            case 0: // REST
                //Debug.Log("returning a rest event");
                return new RestScheduleEvent();
            case 1: // BASIC STRENGTH TRAINING
                return GetBasicStrengthTrainingScheduleEvent();
            case 2: // BASIC ENDURANCE TRAINING
                return GetBasicEnduranceTrainingScheduleEvent();
            case 3: // BASIC AGILITY TRAINING
                return GetBasicAgilityTrainingScheduleEvent();
            case 4: // BASIC DEXTERITY TRAINING
                return GetBasicDexterityTrainingScheduleEvent();
            case 5: // BASIC INTELLIGENCE TRAINING
                return GetBasicIntelligenceTrainingScheduleEvent();
            case 6: // BASIC FAITH TRAINING
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
        rrh.SetFaceImage(heroManager.faceImage);

        // set EnergyFill to (hero's current energy / hero's max energy)
        float energyFillVal = (float)heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy;
        Debug.Log(heroManager.HeroTraining().GetTempEnergy() + " / " + HeroSettings.maxEnergy + " = " + ((float)heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy));

        rrh.SetEnergyFill(energyFillVal);
        rrh.SetEnergyFillText(heroManager.HeroTraining().GetTempEnergy() + " / " + HeroSettings.maxEnergy);

        // show panel after x seconds
        StartCoroutine(trainingManager.DisplayResultPanelAfterDelay());

        // wait x seconds
        yield return new WaitForSeconds(DateSettings.trainingResultsFillDelaySeconds);

        // fill bars and update TotalExpText over x seconds
        StartCoroutine(UpdateEnergyFill(heroManager));

        yield return new WaitForSeconds(DateSettings.trainingResultShowSeconds - DateSettings.trainingResultsDelaySeconds);

        trainingManager.ToggleCanvasGroup(false);

        // reset stuff
        trainingManager.ResetVars();
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

            tempFillBarEnergy = startEnergy + ((ScheduleSettings.BaseEnergyRestoredFromRest * .01f) * fillPercent);

            heroManager.HeroSchedule().GetTempRRH().SetEnergyFill(tempFillBarEnergy);

            float tempFillTextEnergy = ScheduleSettings.BaseEnergyRestoredFromRest * fillPercent;

            int roundedTempEnergy = Mathf.RoundToInt(tempFillTextEnergy) + heroManager.HeroTraining().GetTempEnergy();
            if (roundedTempEnergy > HeroSettings.maxEnergy ) { roundedTempEnergy = HeroSettings.maxEnergy; }
            heroManager.HeroSchedule().GetTempRRH().SetEnergyFillText(roundedTempEnergy + " / " + HeroSettings.maxEnergy);

            yield return new WaitForEndOfFrame();
        }
    }


    #region DEFINE LEVEL 1 BASIC TRAININGS HERE - check ScheduleMenuHandler for IDs if needed

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

    public TrainingScheduleEvent GetTrainingEventFromEquipmentSlot(int slot)
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();
        TrainingEquipment trainingEquip = heroManager.HeroTrainingEquipment().GetTrainingEquipmentBySlot(slot);

        trainingScheduleEvent.SetName("Lv. " + trainingEquip.trainingLevel + ") " + trainingEquip.trainingName);
        trainingScheduleEvent.SetTrainingName("Lv. " + trainingEquip.trainingLevel + ") " + trainingEquip.trainingName);

        trainingScheduleEvent.SetID(trainingEquip.ID);

        trainingScheduleEvent.SetTrainingLevel(trainingEquip.trainingLevel);
        trainingScheduleEvent.SetTrainingType(trainingEquip.trainingType);

        return trainingScheduleEvent;
    }

    #endregion
}

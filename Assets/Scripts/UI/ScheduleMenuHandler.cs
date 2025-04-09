using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes:

/*
 * DROPDOWN OPTIONS - This is the option number in the dropdown, but is also set manually when generating each schedule event
 * 0 - REST
 * 1 - BASIC STRENGTH TRAINING
 * 2 - BASIC ENDURANCE TRAINING
 * 3 - BASIC AGILITY TRAINING
 * 4 - BASIC DEXTERITY TRAINING
 * 5 - BASIC INTELLIGENCE TRAINING
 * 6 - BASIC FAITH TRAINING
 * 7 - HERO TRAINING EQUIP SLOT 1
 * 8 - HERO TRAINING EQUIP SLOT 2
 */

public class ScheduleMenuHandler : MonoBehaviour
{
    [SerializeField] DateManager dateManager;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] TrainingManager trainingManager;

    [SerializeField] TextMeshProUGUI dateText;

    [SerializeField] TextMeshProUGUI currentScheduleEventLabel;
    [SerializeField] TextMeshProUGUI currentScheduleEventText;

    [SerializeField] TextMeshProUGUI slot1MonthLabel;
    [SerializeField] TextMeshProUGUI slot2MonthLabel;
    [SerializeField] TextMeshProUGUI slot3MonthLabel;
    [SerializeField] TextMeshProUGUI slot4MonthLabel;
    [SerializeField] TextMeshProUGUI slot5MonthLabel;
    [SerializeField] TextMeshProUGUI slot6MonthLabel;
    [SerializeField] TextMeshProUGUI slot7MonthLabel;

    [SerializeField] TextMeshProUGUI slot1WeekLabel;
    [SerializeField] TextMeshProUGUI slot2WeekLabel;
    [SerializeField] TextMeshProUGUI slot3WeekLabel;
    [SerializeField] TextMeshProUGUI slot4WeekLabel;
    [SerializeField] TextMeshProUGUI slot5WeekLabel;
    [SerializeField] TextMeshProUGUI slot6WeekLabel;
    [SerializeField] TextMeshProUGUI slot7WeekLabel;

    [SerializeField] TMP_Dropdown slot1Dropdown;
    [SerializeField] TMP_Dropdown slot2Dropdown;
    [SerializeField] TMP_Dropdown slot3Dropdown;
    [SerializeField] TMP_Dropdown slot4Dropdown;
    [SerializeField] TMP_Dropdown slot5Dropdown;
    [SerializeField] TMP_Dropdown slot6Dropdown;
    [SerializeField] TMP_Dropdown slot7Dropdown;

    List<String> scheduleOptions = new List<String>();

    string weekText = "Week";

    private void SetDefaultScheduleOptions()
    {
        ClearDropdowns();

        // Rest
        scheduleOptions.Add("Rest"); // change

        // Add basic training STR - FTH
        scheduleOptions.Add(trainingManager.GetLevel1BasicStrengthTrainingName());
        scheduleOptions.Add(trainingManager.GetLevel1BasicEnduranceTrainingName());
        scheduleOptions.Add(trainingManager.GetLevel1BasicAgilityTrainingName());
        scheduleOptions.Add(trainingManager.GetLevel1BasicDexterityTrainingName());
        scheduleOptions.Add(trainingManager.GetLevel1BasicIntelligenceTrainingName());
        scheduleOptions.Add(trainingManager.GetLevel1BasicFaithTrainingName());
    }

    public void GenerateDropdowns()
    {
        SetDefaultScheduleOptions();

        List<String> options = new List<String>();

        // For any equipSlot that isn't null
        if (scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            scheduleOptions.Add("Lv. " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingLevel + ") " //"Lv. [x]) "
                + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingName); // "name"
        }

        // For any equipSlot that isn't null
        if (scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            scheduleOptions.Add("Lv. " + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingLevel + ") " //"Lv. [x]) "
                + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingName); // "name"
        }

        // add the options to the dropdowns
        slot1Dropdown.AddOptions(scheduleOptions);
        slot2Dropdown.AddOptions(scheduleOptions);
        slot3Dropdown.AddOptions(scheduleOptions);
        slot4Dropdown.AddOptions(scheduleOptions);
        slot5Dropdown.AddOptions(scheduleOptions);
        slot6Dropdown.AddOptions(scheduleOptions);
        slot7Dropdown.AddOptions(scheduleOptions);

        // Set default values based on set Hero Schedule
        DisplayHeroSchedule();
    }

    public void DisplayHeroSchedule()
    {
        // Debug.Log(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[1].GetName());

        slot1Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[1]);
        slot2Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[2]);
        slot3Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[3]);
        slot4Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[4]);
        slot5Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[5]);
        slot6Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[6]);
        slot7Dropdown.value = GetOptionValByScheduleEvent(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[7]);
    }

    int GetOptionValByScheduleEvent(ScheduleEvent scheduleEvent)
    {
        int count = 0;

        bool found = false;

        if (scheduleEvent is TrainingScheduleEvent)
        {
            TrainingScheduleEvent trainingScheduleEvent = scheduleEvent as TrainingScheduleEvent;
            foreach (string option in scheduleOptions)
            {                
                if (option == trainingScheduleEvent.GetTrainingName())
                {
                    found = true;
                    return count;
                }

                count++;
            }
        } else if (scheduleEvent is RestScheduleEvent)
        {
            foreach (string option in scheduleOptions)
            {
                if (option == scheduleEvent.GetName())
                {
                    found = true;
                    return count;
                }

                count++;
            }
        }            

        if (!found) // option not found.  Likely hero unequipped the training equipment or something.
        {
            Debug.LogWarning("ScheduleMenuHandler - GetOptionValByScheduleEvent: " + scheduleEvent.GetName() + " Option not found.  Defaulting to Rest.");
            return 0; // Option for rest
        }

        return count;
    } 

    ScheduleEvent CreateScheduleEventByOptionID(int ID) // This has to be rewritten - we cannot check by ID, or it will interfere with the equip slots.
        // make a new method to get an event ID by name.  Pass the name of the value clicked in the dropdown, and use the below to set the event.
        // If the value is between 100-200 (saved for equipment slots) we can figure out which slot that was saved to and pull from that.
    {
        switch (ID)
        {
            case 0: // REST
                Debug.Log("returning a rest event");
                return new RestScheduleEvent();
            case 1: // BASIC STRENGTH TRAINING
                return trainingManager.GetBasicStrengthTrainingScheduleEvent();
            case 2: // BASIC ENDURANCE TRAINING
                return trainingManager.GetBasicEnduranceTrainingScheduleEvent();
            case 3: // BASIC AGILITY TRAINING
                return trainingManager.GetBasicAgilityTrainingScheduleEvent();
            case 4: // BASIC DEXTERITY TRAINING
                return trainingManager.GetBasicDexterityTrainingScheduleEvent();
            case 5: // BASIC INTELLIGENCE TRAINING
                return trainingManager.GetBasicIntelligenceTrainingScheduleEvent();
            case 6: // BASIC FAITH TRAINING
                return trainingManager.GetBasicFaithTrainingScheduleEvent();
            case 7: // HERO TRAINING EQUIP SLOT 1
                return trainingManager.GetTrainingEventFromEquipmentSlot(0);
            case 8: // HERO TRAINING EQUIP SLOT 2
                return trainingManager.GetTrainingEventFromEquipmentSlot(1);
            default:
                return null;
        }
    }

    void ClearDropdowns()
    {
        slot1Dropdown.ClearOptions();
        slot2Dropdown.ClearOptions();
        slot3Dropdown.ClearOptions();
        slot4Dropdown.ClearOptions();
        slot5Dropdown.ClearOptions();
        slot6Dropdown.ClearOptions();
        slot7Dropdown.ClearOptions();

        scheduleOptions.Clear();
    }

    public void SetTexts()
    {
        dateText.text = dateManager.GetMonthString(dateManager.GetCurrentMonth()) + "\n" + dateManager.GetCurrentYear().ToString();
        currentScheduleEventLabel.text = weekText + " " + dateManager.GetRealCurrentWeek() + ":";

        slot1MonthLabel.text = dateManager.GetMonthFromWeeksOut(1);
        slot2MonthLabel.text = dateManager.GetMonthFromWeeksOut(2);
        slot3MonthLabel.text = dateManager.GetMonthFromWeeksOut(3);
        slot4MonthLabel.text = dateManager.GetMonthFromWeeksOut(4);
        slot5MonthLabel.text = dateManager.GetMonthFromWeeksOut(5);
        slot6MonthLabel.text = dateManager.GetMonthFromWeeksOut(6);
        slot7MonthLabel.text = dateManager.GetMonthFromWeeksOut(7);

        slot1WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(1);
        slot2WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(2);
        slot3WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(3);
        slot4WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(4);
        slot5WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(5);
        slot6WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(6);
        slot7WeekLabel.text = weekText + " " + dateManager.GetWeekFromWeeksOut(7);
    }

    public void SetHeroScheduleEventSlot(int slot)
    {
        ScheduleEvent newScheduleEvent = null;

        switch (slot)
        {
            case 1:
                newScheduleEvent = CreateScheduleEventByOptionID(slot1Dropdown.value);
                break;
            case 2:
                newScheduleEvent = CreateScheduleEventByOptionID(slot2Dropdown.value);
                break;
            case 3:
                newScheduleEvent = CreateScheduleEventByOptionID(slot3Dropdown.value);
                break;
            case 4:
                newScheduleEvent = CreateScheduleEventByOptionID(slot4Dropdown.value);
                break;
            case 5:
                newScheduleEvent = CreateScheduleEventByOptionID(slot5Dropdown.value);
                break;
            case 6:
                newScheduleEvent = CreateScheduleEventByOptionID(slot6Dropdown.value);
                break;
            case 7:
                newScheduleEvent = CreateScheduleEventByOptionID(slot7Dropdown.value);
                break;
        }

        scheduleManager.GetHeroManager().HeroSchedule().SetScheduleSlot(slot, newScheduleEvent);
    }
}

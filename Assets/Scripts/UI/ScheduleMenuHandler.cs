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
        scheduleOptions.Add("Rest"); // change eventually. for now it is fine, rest will always be at ID 0.

        // Add basic training STR - FTH
        scheduleOptions.Add(scheduleManager.GetLevel1BasicStrengthTrainingName());
        scheduleOptions.Add(scheduleManager.GetLevel1BasicEnduranceTrainingName());
        scheduleOptions.Add(scheduleManager.GetLevel1BasicAgilityTrainingName());
        scheduleOptions.Add(scheduleManager.GetLevel1BasicDexterityTrainingName());
        scheduleOptions.Add(scheduleManager.GetLevel1BasicIntelligenceTrainingName());
        scheduleOptions.Add(scheduleManager.GetLevel1BasicFaithTrainingName());
    }

    public void GenerateDropdowns()
    {
        Debug.Log("Generating dropdowns");

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

        currentScheduleEventText.text = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[0].GetName();

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
        
        foreach (string option in scheduleOptions)
        {
            //Debug.Log("Checking " + option + " compared to " + scheduleEvent.GetName());
            if (option == scheduleEvent.GetName())
            {
                found = true;
                return count;
            }

            count++;
        }      

        if (!found) // option not found.  Likely hero unequipped the training equipment or something.
        {
            Debug.LogWarning("ScheduleMenuHandler - GetOptionValByScheduleEvent: " + scheduleEvent.GetName() + " Option not found.  Defaulting to Rest.");
            return 0; // Option for rest
        }

        return count;
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

    /// <summary>
    /// Set on the dropdowns 'OnChange()'.  The slot is which dropdown was clicked.
    /// </summary>
    /// <param name="slot"></param>
    public void SetHeroScheduleEventSlot(int slot)
    {
        ScheduleEvent newScheduleEvent = null;

        switch (slot)
        {
            case 1:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot1Dropdown.options[slot1Dropdown.value].text));                
                break;
            case 2:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot2Dropdown.options[slot2Dropdown.value].text));
                break;
            case 3:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot3Dropdown.options[slot3Dropdown.value].text));
                break;
            case 4:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot4Dropdown.options[slot4Dropdown.value].text));
                break;
            case 5:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot5Dropdown.options[slot5Dropdown.value].text));
                break;
            case 6:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot6Dropdown.options[slot6Dropdown.value].text));
                break;
            case 7:
                newScheduleEvent = scheduleManager.CreateScheduleEventByEventID(scheduleManager.GetEventIDByScheduleEventName(slot7Dropdown.options[slot7Dropdown.value].text));
                break;
        }

        Debug.LogWarning("*-*-*-* New event created: " + newScheduleEvent.GetName() + " into schedule slot " + slot + "*-*-*-*-*");

        scheduleManager.GetHeroManager().HeroSchedule().SetScheduleSlot(slot, newScheduleEvent);
    }
}

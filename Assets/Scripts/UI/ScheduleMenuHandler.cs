using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Purpose: Facilitates manipulating the Schedule Menu UI
// Directions: Attach to [UI]/ScheduleCanvas/ScheduleHolder
// Other notes:

public class ScheduleMenuHandler : MonoBehaviour
{
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

    List<String> scheduleOptions = new List<String>(); // Used to gather and set the available schedule options for a given hero

    /// <summary>
    /// Adds a schedule option for each option that should always be available to the player.
    /// </summary>
    void SetDefaultScheduleOptions()
    {
        ClearDropdowns();

        // Rest
        scheduleOptions.Add("Rest"); // change eventually. for now it is fine, rest will always be at ID 0.

        // Add basic training STR - FTH
        scheduleOptions.Add(ScheduleManager.i.GetLevel1BasicStrengthTrainingName());
        scheduleOptions.Add(ScheduleManager.i.GetLevel1BasicEnduranceTrainingName());
        scheduleOptions.Add(ScheduleManager.i.GetLevel1BasicAgilityTrainingName());
        scheduleOptions.Add(ScheduleManager.i.GetLevel1BasicDexterityTrainingName());
        scheduleOptions.Add(ScheduleManager.i.GetLevel1BasicIntelligenceTrainingName());
        scheduleOptions.Add(ScheduleManager.i.GetLevel1BasicFaithTrainingName());
    }

    /// <summary>
    /// Sets the default value for each dropdown to the event that the hero has scheduled for that week
    /// </summary>
    void DisplayDefaultDropdownValues()
    {
        // Debug.Log(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[1].GetName());

        currentScheduleEventText.text = HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[0].GetName();

        slot1Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[1]);
        slot2Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[2]);
        slot3Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[3]);
        slot4Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[4]);
        slot5Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[5]);
        slot6Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[6]);
        slot7Dropdown.value = GetOptionValByScheduleEvent(HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[7]);
    }

    /// <summary>
    /// Gets the option index value from scheduleOptions using the given scheduleEvent by comparing their names
    /// </summary>
    /// <param name="scheduleEvent">The schedule event to get the option value</param>
    /// <returns>The index value to be used for choosing the default option</returns>
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

    /// <summary>
    /// Just resets the options in the dropdowns
    /// </summary>
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

    /// <summary>
    /// Adds all of the options in scheduleOptions to the dropdowns in the UI.
    /// </summary>
    public void GenerateDropdowns()
    {
        //Debug.Log("Generating dropdowns");

        SetDefaultScheduleOptions();

        List<String> options = new List<String>();

        // For any equipSlot that isn't null
        if (HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            scheduleOptions.Add("Lv. " + HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingLevel + ") " //"Lv. [x]) "
                + HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingName); // "name"
        }

        // For any equipSlot that isn't null
        if (HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            scheduleOptions.Add("Lv. " + HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingLevel + ") " //"Lv. [x]) "
                + HomeZoneManager.i.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingName); // "name"
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
        DisplayDefaultDropdownValues();
    }

    /// <summary>
    /// Sets the text  on the objects in the Schedule UI to the appropriate values
    /// </summary>
    public void SetTexts()
    {
        dateText.text = DateManager.i.GetMonthString(DateSettings.GetCurrentMonth()) + "\n" + DateSettings.GetCurrentYear().ToString();
        currentScheduleEventLabel.text = ScheduleSettings.weekText + " " + DateSettings.GetRealCurrentWeek() + ":";

        slot1MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(1);
        slot2MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(2);
        slot3MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(3);
        slot4MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(4);
        slot5MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(5);
        slot6MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(6);
        slot7MonthLabel.text = DateManager.i.GetMonthFromWeeksOut(7);

        slot1WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(1);
        slot2WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(2);
        slot3WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(3);
        slot4WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(4);
        slot5WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(5);
        slot6WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(6);
        slot7WeekLabel.text = ScheduleSettings.weekText + " " + DateManager.i.GetWeekFromWeeksOut(7);
    }

    /// <summary>
    /// Set on the dropdowns 'OnChange()'.  The slot is which dropdown was clicked.
    /// Assigned to [UI]/ScheduleCanvas/ScheduleHolder/LayoutGroup/ScheduleOptionSlot[slot]/Slot[slot]Dropdown OnValueChanged()
    /// </summary>
    /// <param name="slot">The slot that was clicked.  Set in the inspector on each dropdown object.</param>
    public void SetHeroScheduleEventSlot(int slot)
    {
        ScheduleEvent newScheduleEvent = null;

        switch (slot)
        {
            case 1:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot1Dropdown.options[slot1Dropdown.value].text));                
                break;
            case 2:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot2Dropdown.options[slot2Dropdown.value].text));
                break;
            case 3:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot3Dropdown.options[slot3Dropdown.value].text));
                break;
            case 4:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot4Dropdown.options[slot4Dropdown.value].text));
                break;
            case 5:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot5Dropdown.options[slot5Dropdown.value].text));
                break;
            case 6:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot6Dropdown.options[slot6Dropdown.value].text));
                break;
            case 7:
                newScheduleEvent = ScheduleManager.i.CreateScheduleEventByEventID(ScheduleManager.i.GetEventIDByScheduleEventName(slot7Dropdown.options[slot7Dropdown.value].text));
                break;
        }

        //Debug.Log("<color=Green>[ScheduleMenuHandler]</color> New event created: " + newScheduleEvent.GetName() + " into schedule slot " + slot + "*-*-*-*-*");
        String debugOut = newScheduleEvent.GetName() + " set into schedule slot " + slot;
        DebugManager.i.UIDebugOut("ScheduleMenuHandler", debugOut, false, false);

        HomeZoneManager.i.GetHeroManager().HeroSchedule().SetScheduleSlot(slot, newScheduleEvent);
    }

    /// <summary>
    /// When clicking 'Force Rest' in the Schedule Menu, the current week's activity is cancelled and set to a rest event instead.
    /// Assigned to: [UI]/ScheduleCanvas/ScheduleHolder/ForceRestButton.OnClick()
    /// </summary>
    public void OnForceRestClick()
    {
        if (HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[0].GetID() != 0) // If first schedule slot is not rest already
        {
            RestScheduleEvent newRestEvent = new RestScheduleEvent();

            HomeZoneManager.i.GetHeroManager().HeroSchedule().SetScheduleSlot(0, newRestEvent);

            currentScheduleEventText.text = HomeZoneManager.i.GetHeroManager().HeroSchedule().GetScheduleEvents()[0].GetName();
        } else
        {
            // Play a Nope SE or something.
        }     
    }
}

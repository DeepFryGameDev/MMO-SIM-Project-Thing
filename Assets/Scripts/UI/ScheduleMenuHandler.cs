using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes:

public class ScheduleMenuHandler : MonoBehaviour
{
    [SerializeField] DateManager dateManager;
    [SerializeField] ScheduleManager scheduleManager;

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

    List<String> defaultScheduleOptions = new List<String>();

    string weekText = "Week";

    private void Start()
    {
        SetDefaultScheduleOptions();
    }

    private void SetDefaultScheduleOptions()
    {
        // Rest
        defaultScheduleOptions.Add("Rest");

        // Add basic training STR - FTH
        defaultScheduleOptions.Add("Lv1. Basic Strength Training");
        defaultScheduleOptions.Add("Lv1. Basic Endurance Training");
        defaultScheduleOptions.Add("Lv1. Basic Agility Training");
        defaultScheduleOptions.Add("Lv1. Basic Dexterity Training");
        defaultScheduleOptions.Add("Lv1. Basic Intelligence Training");
        defaultScheduleOptions.Add("Lv1. Basic Faith Training");
    }

    public void GenerateDropdowns()
    {
        ClearDropdowns();

        List<String> options = new List<String>();

        // For any equipSlot that isn't null
        if (scheduleManager.GetCurrentHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            options.Add("Lv" + scheduleManager.GetCurrentHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingLevel + ". " //"Lv[x]. "
                + scheduleManager.GetCurrentHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().name); // "name"
        }

        // For any equipSlot that isn't null
        if (scheduleManager.GetCurrentHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            options.Add("Lv" + scheduleManager.GetCurrentHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingLevel + ". " //"Lv[x]. "
                + scheduleManager.GetCurrentHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().name); // "name"
        }

        // add the options to the dropdowns
        slot1Dropdown.AddOptions(defaultScheduleOptions);
        slot1Dropdown.AddOptions(options);

        slot2Dropdown.AddOptions(defaultScheduleOptions);
        slot2Dropdown.AddOptions(options);

        slot3Dropdown.AddOptions(defaultScheduleOptions);
        slot3Dropdown.AddOptions(options);

        slot4Dropdown.AddOptions(defaultScheduleOptions);
        slot4Dropdown.AddOptions(options);

        slot5Dropdown.AddOptions(defaultScheduleOptions);
        slot5Dropdown.AddOptions(options);

        slot6Dropdown.AddOptions(defaultScheduleOptions);
        slot6Dropdown.AddOptions(options);

        slot7Dropdown.AddOptions(defaultScheduleOptions);
        slot7Dropdown.AddOptions(options);

        // Set default values based on set Hero Schedule
        DisplayHeroSchedule();
    }

    public void DisplayHeroSchedule()
    {
        for (int i=1; i < scheduleManager.GetCurrentHeroManager().HeroSchedule().GetScheduleEvents().Length; i++)
        {
            Debug.Log(scheduleManager.GetCurrentHeroManager().HeroSchedule().GetScheduleEvents()[i]);
        }
    }

    public void SetHeroScheduleEventSlot(int slot)
    {

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
}

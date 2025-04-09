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

    List<String> defaultScheduleOptions = new List<String>();

    string weekText = "Week";

    private void Start()
    {
        SetDefaultScheduleOptions();
    }

    private void SetDefaultScheduleOptions()
    {
        // Rest
        defaultScheduleOptions.Add(scheduleManager.GetRestEventName());

        // Add basic training STR - FTH
        defaultScheduleOptions.Add(trainingManager.GetLevel1BasicStrengthTrainingName());
        defaultScheduleOptions.Add(trainingManager.GetLevel1BasicEnduranceTrainingName());
        defaultScheduleOptions.Add(trainingManager.GetLevel1BasicAgilityTrainingName());
        defaultScheduleOptions.Add(trainingManager.GetLevel1BasicDexterityTrainingName());
        defaultScheduleOptions.Add(trainingManager.GetLevel1BasicIntelligenceTrainingName());
        defaultScheduleOptions.Add(trainingManager.GetLevel1BasicFaithTrainingName());
    }

    public void GenerateDropdowns()
    {
        ClearDropdowns();

        List<String> options = new List<String>();

        // For any equipSlot that isn't null
        if (scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            options.Add("Lv" + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingLevel + ". " //"Lv[x]. "
                + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot0().trainingName); // "name"
        }

        // For any equipSlot that isn't null
        if (scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            options.Add("Lv" + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingLevel + ". " //"Lv[x]. "
                + scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentSlot1().trainingName); // "name"
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
        // Debug.Log(scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[1].GetName());
        slot1Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[1].GetID();
        slot2Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[2].GetID();
        slot3Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[3].GetID();
        slot4Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[4].GetID();
        slot5Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[5].GetID();
        slot6Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[6].GetID();
        slot7Dropdown.value = scheduleManager.GetHeroManager().HeroSchedule().GetScheduleEvents()[7].GetID();
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

    ScheduleEvent CreateScheduleEventByOptionID(int ID)
    {
        switch (ID)
        {
            case 0: // REST
                return scheduleManager.GetDefaultRestScheduleEvent();
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

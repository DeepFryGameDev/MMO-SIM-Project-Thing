using UnityEngine;

public static class DateSettings
{
    public static float toastSeconds;

    public static float transitionSeconds;

    public static float fadeSeconds;

    public static float trainingResultShowSeconds;

    public static float trainingResultsDelaySeconds;

    public static float trainingResultsFillDelaySeconds;

    public static float trainingResultsFillSeconds;

    public static int startingYear;

    static bool datesInitialized;
    public static void SetDatesInitialized(bool toggle) { datesInitialized = toggle; }
    public static bool GetDatesInitialized() { return datesInitialized; }

    static int[] week = new int[4]; // used to progress the date
    public static void SetWeek(int weekNum, int value) { week[weekNum] = value; }
    public static int[] GetWeek() { return week; }

    static int[] month = new int[12]; // used to progress the date as well
    public static void SetMonth(int monthNum, int value) { month[monthNum] = value; }
    public static int[] GetMonth() { return month; }

    static int currentWeek; // used as the index for week[] to determine the current week
    public static void SetCurrentWeek(int week) { currentWeek = week; }
    public static int GetCurrentWeek() { return currentWeek; }
    public static int GetRealCurrentWeek() { return currentWeek + 1; }

    static int currentMonth; // used as the index for month[] to determine the current month
    public static void SetCurrentMonth(int month) { currentMonth = month; }
    public static int GetCurrentMonth() { return currentMonth; }
    public static int GetRealCurrentMonth() { return currentMonth + 1; }

    static int currentYear;
    public static int GetCurrentYear() { return currentYear; }
    public static void SetCurrentYear(int year) { currentYear = year; }
}

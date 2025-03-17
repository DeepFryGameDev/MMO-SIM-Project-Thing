using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class DateManager : MonoBehaviour
{
    int[] week = new int[4];
    int[] month = new int[12];

    int currentWeek;
    int currentMonth;

    void Awake()
    {
        
    }

    private void Start()
    {
        InitializeDates();

        ProcessBeginningOfWeek();
    }

    void InitializeDates()
    {
        for (int i=0; i <= 3; i++)
        {
            week[i] = i + 1; // Sets each week to week 1,2,3,4.
        }

        for (int i=0; i <= 11; i++)
        {
            month[i] = i + 1; // Sets each month to 1,2,3, etc to 12.
        }

        currentWeek = 0;
        currentMonth = 0;
    }

    public void RollForwardDate()
    {
        currentWeek++;

        if (currentWeek > 3)
        {
            currentWeek = 0;
            currentMonth++;
        }

        if (currentMonth > 11)
        {
            currentMonth = 0;
        }
    }

    public void ProcessBeginningOfWeek()
    {
        // darken screen

        // show graphic
        Debug.Log("Should show graphic: It is: Week " + week[currentWeek] + " of Month " + month[currentMonth]);

        // disable player movement

        // disable camera movement

        // move player to starting position

        // move all heroes to starting position

        // change all hero pathing to idle

        // wait 2 seconds

        // change all hero pathing to random again (should eventually be set to whatever the schedule has them doing)

        // enable player movement

        // enable camera movement

        // brighten screen

        // hide graphic
    }

    string GetMonth()
    {
        switch (currentMonth) 
        {
            case 0:
                return "January";
            case 1:
                return "February";
            case 2:
                return "March";
            case 3:
                return "April";
            case 4:
                return "May";
            case 5:
                return "June";
            case 6:
                return "July";
            case 7:
                return "August";
            case 8:
                return "September";
            case 9:
                return "October";
            case 10:
                return "November";
            case 11:
                return "December";
            default:
                return null;
        }
    }

    void Update()
    {
        
    }
}

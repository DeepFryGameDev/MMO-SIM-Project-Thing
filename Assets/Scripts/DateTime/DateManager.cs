using System.Collections;
using TMPro;
using UnityEngine;

// Purpose: Facilitates processing the date/time of year and displaying it
// Directions: Attach to [System] GameObject
// Other notes: 

public class DateManager : MonoBehaviour
{
    int[] week = new int[4]; // used to progress the date
    int[] month = new int[12]; // used to progress the date as well

    int currentWeek; // used as the index for week[] to determine the current week
    int currentMonth; // used as the index for month[] to determine the current month

    CanvasGroup canvasGroup;
    TextMeshProUGUI weekText;
    TextMeshProUGUI monthText;

    CanvasGroup fadeCanvasGroup;

    PlayerMovement playerMovement;

    ThirdPersonCam cam;

    SpawnManager spawnManager;

    HeroManager[] heroManagers;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        canvasGroup = GameObject.Find("DateCanvas/DatePanel").GetComponent<CanvasGroup>(); // hacky, should change these later
        weekText = GameObject.Find("DateCanvas/DatePanel/WeekText").GetComponent<TextMeshProUGUI>();
        monthText = GameObject.Find("DateCanvas/DatePanel/MonthText").GetComponent<TextMeshProUGUI>();

        fadeCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();

        playerMovement = FindFirstObjectByType<PlayerMovement>();
        cam = FindFirstObjectByType<ThirdPersonCam>();

        spawnManager = FindFirstObjectByType<SpawnManager>();

        heroManagers = FindObjectsByType<HeroManager>(FindObjectsSortMode.InstanceID);
    }

    void Start()
    {
        InitializeDates();

        StartCoroutine(ShowToast());
    }

    /// <summary>
    /// Sets the week[] and month[] arrays to the expected values (week 1-4, and month 1-12)
    /// </summary>
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

    /// <summary>
    /// Simply moves the date forward one week, taking into account rolling months forward as well.
    /// </summary>
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

    /// <summary>
    /// Quite a large Coroutine.  Split in three separate sections:
    /// pre-transition: Here are the processes that should fire before the player/heroes are moved.  The screen is faded out here.
    /// transition: Here are where objects should be reset in the world to prepare for the next week
    /// post-transition: Here are processes that should fire after the player/hero objects are moved.  The screen fades back in here.
    /// </summary>
    /// <returns>Technically there are 2 waits, one at the beginning and one at the end of the transition phase.  Split in 1/2 of 'DateSettingstransitionSeconds' for each</returns>
    public IEnumerator ProcessStartWeek()
    {
        #region pre-transition
        // keep player from opening any other menus
        GlobalSettings.SetUIState(GlobalSettings.UIStates.BLOCKED);

        // hide mouse cursor
        ToggleCursor(false);

        // darken screen
        StartCoroutine(FadeToBlack(true));

        // disable player movement
        playerMovement.ToggleMovement(false);

        // disable camera movement
        cam.ToggleCameraRotation(false);

        // change all hero pathing to idle (This is not fully resetting heroes).
        foreach (HeroManager heroManager in heroManagers)
        {
            heroManager.HeroPathing().StopPathing();
        }

        RollForwardDate();

        // show graphic
        StartCoroutine(ShowToast());

        // calculate and show training results
        foreach (HeroManager heroManager in heroManagers)
        {
            Debug.Log("-*-*- Training Logs for " + heroManager.Hero().name + "-*-*-");
            Debug.Log("Before exp: " + heroManager.HeroTraining().GetStrengthExp() + ", level: " + heroManager.Hero().GetStrength());
            heroManager.HeroTraining().ProcessTraining();
            Debug.Log("After exp: " + heroManager.HeroTraining().GetStrengthExp() + ", level: " + heroManager.Hero().GetStrength());
            Debug.Log("--------------------------------------------");
        }

        #endregion

        #region transition

        // We are cutting the transition seconds in half and running it twice, to allow smooth transition of things in the middle.

        // wait transition seconds
        yield return new WaitForSeconds(DateSettings.transitionSeconds / 2);

        // -*-*-*-*- Start here -*-*-*-*-

        // move all heroes to starting position
        foreach (HeroManager heroManager in heroManagers)
        {
            heroManager.HeroPathing().MoveToStartingPosition();
        }

        // move player to starting position
        spawnManager.MovePlayerToSpawnPosition();

        // -*-*-*-*- End here -*-*-*-*-

        // Wait again
        yield return new WaitForSeconds(DateSettings.transitionSeconds / 2);

        #endregion

        #region post-transition

        // change all hero pathing to random again (should eventually be set to whatever the schedule has them doing)
        foreach (HeroManager heroManager in heroManagers)
        {
            heroManager.HeroPathing().StartNewRandomPathing();
        }

        // enable player movement
        playerMovement.ToggleMovement(true);

        // enable camera movement
        cam.ToggleCameraRotation(true);

        // brighten screen
        StartCoroutine(FadeToBlack(false));

        GlobalSettings.SetUIState(GlobalSettings.UIStates.IDLE);

        #endregion
    }

    /// <summary>
    /// Shows/hides the cursor
    /// </summary>
    /// <param name="toggle">True to show the cursor, false to hide it</param>
    void ToggleCursor(bool toggle)
    {
        if (toggle)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Coroutine to display the Date Toast to the user
    /// </summary>
    /// <returns>Waits for toast seconds between fade in/out</returns>
    IEnumerator ShowToast()
    {
        Debug.Log("<color=blue>[DateManager] Starting week: " + week[currentWeek] + " of Month " + month[currentMonth] + "</color>");
        SetToastText();
        ToggleToast(true);

        yield return new WaitForSeconds(DateSettings.toastSeconds);

        ToggleToast(false);
    }

    /// <summary>
    /// Coroutine to process fade in/out. Used by adjusting the alpha of the fadeCanvasGroup.
    /// </summary>
    /// <param name="fadeIn"></param>
    /// <returns>Waits for end of frame using DateSettings.fadeSettings as the timer</returns>
    IEnumerator FadeToBlack(bool fadeIn)
    {
        float timer;

        switch (fadeIn)
        {
            case true:
                timer = 0; // Should go from 0 to 1

                while (timer < DateSettings.fadeSeconds)
                {
                    timer += Time.deltaTime;
                    fadeCanvasGroup.alpha = timer / DateSettings.fadeSeconds; // setting alpha from 0 to 1

                    yield return new WaitForEndOfFrame();
                }
                break;
            case false:
                timer = DateSettings.fadeSeconds; // Should go from 1 to 0

                while (timer > 0)
                {
                    timer -= Time.deltaTime;
                    fadeCanvasGroup.alpha = timer / DateSettings.fadeSeconds; // setting alpha from 1 to 0

                    yield return new WaitForEndOfFrame();
                }
                break;
        }
    }

    /// <summary>
    /// Sets the text objects on the toast UI to the current week and month
    /// </summary>
    void SetToastText()
    {
        weekText.text = week[currentWeek].ToString();
        monthText.text = GetMonth();
    }

    /// <summary>
    /// Displays and hides the Date toast.
    /// </summary>
    /// <param name="toggle">True to display the toast, false to hide it</param>
    void ToggleToast(bool toggle)
    {
        if (toggle)
        {
            canvasGroup.alpha = 1;
        } else
        {
            canvasGroup.alpha = 0;
        }
    }

    /// <summary>
    /// Just used to display the month in text form
    /// </summary>
    /// <returns>String of the current month in text form</returns>
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
}

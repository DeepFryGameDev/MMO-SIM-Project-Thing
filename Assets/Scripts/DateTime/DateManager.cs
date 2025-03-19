using System;
using System.Collections;
using TMPro;
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
        canvasGroup = GameObject.Find("DateCanvas/DatePanel").GetComponent<CanvasGroup>(); // hacky, should change these later
        weekText = GameObject.Find("DateCanvas/DatePanel/WeekText").GetComponent<TextMeshProUGUI>();
        monthText = GameObject.Find("DateCanvas/DatePanel/MonthText").GetComponent<TextMeshProUGUI>();

        fadeCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();

        playerMovement = FindFirstObjectByType<PlayerMovement>();
        cam = FindFirstObjectByType<ThirdPersonCam>();

        spawnManager = FindFirstObjectByType<SpawnManager>();

        heroManagers = FindObjectsByType<HeroManager>(FindObjectsSortMode.InstanceID);
    }

    private void Start()
    {
        InitializeDates();

        StartCoroutine(ShowToast());
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

    public IEnumerator ProcessStartWeek()
    {
        #region pre-transition

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

        // move all heroes to starting position
        foreach (HeroManager heroManager in heroManagers)
        {
            heroManager.HeroPathing().MoveToStartingPosition();
        }

        RollForwardDate();

        // show graphic
        StartCoroutine(ShowToast());

        #endregion

        #region transition

        // We are cutting the transition seconds in half and running it twice, to allow smooth transition of things in the middle.

        // wait transition seconds
        yield return new WaitForSeconds(DateSettings.transitionSeconds / 2);

        // move player to starting position
        spawnManager.MovePlayerToSpawnPosition();

        // change all hero pathing to random again (should eventually be set to whatever the schedule has them doing)
        foreach (HeroManager heroManager in heroManagers)
        {
            heroManager.HeroPathing().pathMode = HeroPathing.pathModes.RANDOM;
        }

        // Wait again
        yield return new WaitForSeconds(DateSettings.transitionSeconds / 2);

        #endregion

        #region post-transition

        // enable player movement
        playerMovement.ToggleMovement(true);

        // enable camera movement
        cam.ToggleCameraRotation(true);

        // brighten screen
        StartCoroutine(FadeToBlack(false));

        #endregion
    }

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

    IEnumerator ShowToast()
    {
        Debug.Log("<color=blue>[DateManager] Starting week: " + week[currentWeek] + " of Month " + month[currentMonth] + "</color>");
        SetToastText();
        ToggleToast(true);

        yield return new WaitForSeconds(DateSettings.toastSeconds);

        ToggleToast(false);
    }

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

    void SetToastText()
    {
        weekText.text = week[currentWeek].ToString();
        monthText.text = GetMonth();
    }

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

using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class ScheduleManager : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    RestScheduleEvent defaultRestScheduleEvent = new RestScheduleEvent();
    public RestScheduleEvent GetDefaultRestScheduleEvent() { return defaultRestScheduleEvent; }

    string restEventName = "Rest";
    public string GetRestEventName() { return restEventName; }

    private void Start()
    {
        SetDefaultRestScheduleEvent();
    }

    void SetDefaultRestScheduleEvent()
    {
        defaultRestScheduleEvent.SetName(restEventName);
        defaultRestScheduleEvent.SetID(0); // check ScheduleMenuHandler for IDs

        // set rest stuff here if needed
    }

    public void ProcessResting()
    {
        // Increase energy

        // Will need to show energy being replenished since the training IEnumerator will not be firing
    }
}

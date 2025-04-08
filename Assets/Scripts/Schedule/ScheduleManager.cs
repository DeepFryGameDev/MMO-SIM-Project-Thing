using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class ScheduleManager : MonoBehaviour
{
    HeroManager currentHeroManager;
    public void SetCurrentHeroManager(HeroManager heroManager) { currentHeroManager = heroManager; }
    public HeroManager GetCurrentHeroManager() { return currentHeroManager; }

    RestScheduleEvent defaultRestScheduleEvent;
    public RestScheduleEvent GetDefaultRestScheduleEvent() { return defaultRestScheduleEvent; }

    void SetDefaultRestScheduleEvent()
    {
        defaultRestScheduleEvent = new RestScheduleEvent();
    }
}

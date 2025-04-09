using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class ScheduleManager : MonoBehaviour
{
    HeroManager heroManager;
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    public void ProcessResting()
    {
        // Increase energy

        // Will need to show energy being replenished since the training IEnumerator will not be firing
    }

    public List<int> GetTrainingSlotsFromEventID(int ID)
    {
        /*Debug.Log("Checking " + ID);

        Debug.Log("1) " + heroManager.HeroSchedule().GetScheduleEvents()[1].GetID());
        Debug.Log("2) " + heroManager.HeroSchedule().GetScheduleEvents()[2].GetID());
        Debug.Log("3) " + heroManager.HeroSchedule().GetScheduleEvents()[3].GetID());
        Debug.Log("4) " + heroManager.HeroSchedule().GetScheduleEvents()[4].GetID());
        Debug.Log("5) " + heroManager.HeroSchedule().GetScheduleEvents()[5].GetID());
        Debug.Log("6) " + heroManager.HeroSchedule().GetScheduleEvents()[6].GetID());
        Debug.Log("7) " + heroManager.HeroSchedule().GetScheduleEvents()[7].GetID());*/

        if (heroManager.HeroSchedule().GetScheduleEvents()[1] == null) { Debug.Log("Null for some reason"); }
        List<int> slots = new List<int>();

        if (heroManager.HeroSchedule().GetScheduleEvents()[1].GetID() == ID) slots.Add(1);
        if (heroManager.HeroSchedule().GetScheduleEvents()[2].GetID() == ID) slots.Add(2);
        if (heroManager.HeroSchedule().GetScheduleEvents()[3].GetID() == ID) slots.Add(3);
        if (heroManager.HeroSchedule().GetScheduleEvents()[4].GetID() == ID) slots.Add(4);
        if (heroManager.HeroSchedule().GetScheduleEvents()[5].GetID() == ID) slots.Add(5);
        if (heroManager.HeroSchedule().GetScheduleEvents()[6].GetID() == ID) slots.Add(6);
        if (heroManager.HeroSchedule().GetScheduleEvents()[7].GetID() == ID) slots.Add(7);

        return slots;
    }
}

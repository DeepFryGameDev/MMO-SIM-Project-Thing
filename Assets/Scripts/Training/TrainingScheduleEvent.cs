using UnityEngine;

// Purpose: 
// Directions: 
// Other notes:

public class TrainingScheduleEvent : ScheduleEvent
{
    BaseTraining training;
    public void SetTraining(BaseTraining training) { this.training = training; }
    public BaseTraining GetTraining() { return training; }
}

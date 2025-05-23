using UnityEngine;

// Purpose: The class for creating a new training event.
// Directions: Create a new TrainingScheduleEvent with the schedule system.  Generally used with ScheduleManager.i.CreateScheduleEventByID()
// Other notes:

public class TrainingScheduleEvent : ScheduleEvent
{
    [Tooltip("Level of the training event. Pick 1-5.  Default = 1.")]
    int trainingLevel = 1;

    /// <summary>
    /// Just gets the level of the training event.
    /// </summary>
    /// <returns>The level of the training event.</returns>
    public int GetTrainingLevel() { return trainingLevel; }

    /// <summary>
    /// Set the level for the training event.  Default = 1.
    /// </summary>
    /// <param name="level">Level that the event should be set to.</param>
    public void SetTrainingLevel(int level)
    {
        trainingLevel = level;
    }

    /// <summary>
    /// Currently set to the same as the Event Name.  This may have some use later.
    /// </summary>
    string trainingName;
    public string GetTrainingName() { return trainingName; }
    public void SetTrainingName(string trainingName) { this.trainingName = trainingName; }

    /// <summary>
    /// The type of training that should be performed
    /// </summary>
    EnumHandler.TrainingTypes trainingType;
    public EnumHandler.TrainingTypes GetTrainingType() { return trainingType; }
    public void SetTrainingType(EnumHandler.TrainingTypes trainingType) { this.trainingType = trainingType; }

    [Tooltip("Effectiveness of the training event. Default = 1")]
    float effectiveness = 1;

    /// <summary>
    /// The effectiveness of the training
    /// </summary>
    /// <param name="effectiveness"></param>
    public void SetEffectiveness(float effectiveness)
    {
        this.effectiveness = effectiveness;
    }

    /// <summary>
    /// Simply just gets the effectiveness val.
    /// </summary>
    /// <returns>Effectiveness val after calculation</returns>
    public float GetEffectiveness() { return effectiveness; }
}

using UnityEngine;

// Purpose: Contains the vars needed for all training equipment to be made
// Directions: It's a scriptable object, so just Create > Training > Equipment
// Other notes: 

[CreateAssetMenu(fileName = "TrainingEquipment", menuName = "Training/Equipment")]
public class TrainingEquipment : Equipment
{
    [Tooltip("Name of the training that this will generate by scheduling it")]
    public string trainingName;

    [Tooltip("Give it a unique ID.  We can start with 100")]
    public int ID;

    [Tooltip("Affects the result of training while having this equipped by a hero.  Leave at 0 for a 'basic' training that does not require any physical objects to interact with the hero.")]
    [Range(1, 5)] public int trainingLevel;

    [Tooltip("The stat that should be increased when training with this.")]
    public EnumHandler.TrainingTypes trainingType;

    [Tooltip("The GameObject that should be instantiated into the world when this is equipped by a hero.")]
    public GameObject worldPrefab;

    [Tooltip("The icon to be used in various menus and UI")]
    public Sprite icon;

    /* can delete later if no issues come up
    public TrainingScheduleEvent ScheduleEvent()
    {
        TrainingScheduleEvent newTrainingEvent = new TrainingScheduleEvent();

        newTrainingEvent.SetTrainingName(trainingName);
        newTrainingEvent.SetID(ID);

        newTrainingEvent.SetTrainingLevel(trainingLevel);
        newTrainingEvent.SetTrainingType(trainingType);

        return newTrainingEvent;
    } */
}

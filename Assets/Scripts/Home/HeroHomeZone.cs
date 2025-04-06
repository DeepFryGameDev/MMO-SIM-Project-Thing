using UnityEngine;

// Purpose: Used to display the attached heroes status/attributes to the player and provide some basic functionality for the player to interact with the hero
// Directions: Attach to each home zone object that should be assigned to each hero
// Other notes: 

public class HeroHomeZone : MonoBehaviour 
{
    public HeroManager heroManager; // Used to set the home zone in HeroManager to this.  The HomeZone inside heroManager isn't being used right now, but this will allow access.

    Transform prefabZones; // Used to instantiate the prefabs from training equipment

    private void Awake()
    {
        heroManager.SetHomeZone(this);

        prefabZones = transform.parent.Find("[TrainingEquipmentPrefabZones]").transform;
    }

    void Start()
    {
        LogErrors();
    }

    /// <summary>
    /// Just making sure the heroManager is set
    /// </summary>
    void LogErrors()
    {
        if (heroManager == null)
        {
            Debug.LogWarning("heroManager null on HeroHomeZone!");
        }
    }

    /// <summary>
    /// Sets the prefabs from equipped TrainingEquipment into the hero's home zone
    /// </summary>
    public void InstantiateTrainingEquipmentPrefabs()
    {
        ClearPrefabZones();

        if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot0() != null)
        {
            Instantiate(heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot0().worldPrefab, prefabZones.Find("Slot0/[Temp]").transform);
        }

        if (heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot1() != null)
        {
            Instantiate(heroManager.HeroTrainingEquipment().GetTrainingEquipmentSlot1().worldPrefab, prefabZones.Find("Slot1/[Temp]").transform);
        }
    }

    /// <summary>
    /// Simply just clears any prefabs from training equipment before instantiating new ones.
    /// </summary>
    void ClearPrefabZones()
    {
        foreach (Transform transform in prefabZones)
        {
            foreach (Transform t in transform.Find("[Temp]").transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}


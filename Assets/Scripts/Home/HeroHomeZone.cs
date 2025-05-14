using UnityEngine;

// Purpose: Used to display the attached heroes status/attributes to the player and provide some basic functionality for the player to interact with the hero
// Directions: Attach to each home zone object that should be assigned to each hero
// Other notes: 

public class HeroHomeZone : MonoBehaviour 
{
    HeroManager heroManager; // Used to set the home zone in HeroManager to this.  The HomeZone inside heroManager isn't being used right now, but this will allow access.
    public void SetHeroManager(HeroManager heroManager) { this.heroManager = heroManager; }
    public HeroManager GetHeroManager() { return heroManager; }

    Transform prefabZones; // Used to instantiate the prefabs from training equipment

    private void Start()
    {
        prefabZones = transform.parent.Find("[TrainingEquipmentPrefabZones]").transform;
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


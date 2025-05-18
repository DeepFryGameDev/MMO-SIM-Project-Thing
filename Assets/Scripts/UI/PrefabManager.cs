using UnityEngine;

// Purpose: Contains prefabs to be manipulated by script
// Directions: Attach to [UI] object and call PrefabManager.i to reference prefabs
// Other notes:

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager i;

    private void Awake()
    {
        Debug.Log("Prefab manager setting i");
        i = this;
    }

    [Tooltip("Set this to the prefab for TrainingResults to be shown when advancing weeks")]
    public GameObject TrainingResult;
    [Tooltip("Set this to the prefab for RestingResults to be shown when advancing weeks")]
    public GameObject RestingResult;

    [Tooltip("Set this to the prefab for TrainingEquipmentButtons to be instantiated when clicking the 'Training Equipment' button in the HeroZone Menu.")]
    public GameObject TrainingEquipmentButton;
    [Tooltip("Set this to the prefab for TrainingEquipmentListButtons to be instantiated when clicking the one of the TrainingEquipmentButtons")]
    public GameObject TrainingEquipmentListButton;

    [Tooltip("Set this to the prefab to show inactive heroes in the Party switch menu")]
    public GameObject PartyInactiveHeroFrame;
    [Tooltip("Set this to the prefab to show active heroes in the Party switch menu")]
    public GameObject PartyActiveHeroFrame;

    [Tooltip("Set this to the prefab to the frame for the party HUD")]
    public GameObject PartyHUDFrame;

    [Tooltip("Set this to the prefab to display each item in the hero's inventory menu")]
    public GameObject HeroInventoryButton;

    [Tooltip("Set this to the prefab to display each equipment item in the hero's equipment menu")]
    public GameObject EquipToHeroButton;

    [Tooltip("Set this to the button to be used for the inventory context menu for 'Give To x Hero' button")]
    public GameObject ContextMenuItemHeroGiveToHeroButton;

    public GameObject HeroCommandFieldMenuPanel;
}

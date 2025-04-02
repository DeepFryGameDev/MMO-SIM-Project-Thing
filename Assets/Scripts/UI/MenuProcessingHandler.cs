using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class MenuProcessingHandler : MonoBehaviour
{
    public static MenuProcessingHandler i;

    EnumHandler.HeroCommandMenuStates heroCommandMenuState;
    public void SetHeroCommandMenuState(EnumHandler.HeroCommandMenuStates menuState) { heroCommandMenuState = menuState; }
    public EnumHandler.HeroCommandMenuStates GetHeroCommandMenuState() { return heroCommandMenuState; }

    EnumHandler.HeroCommandMenuStates tempHeroCommandMenuState; // Used so UI is only updated once.

    // will need access to every canvas group
    [SerializeField] CanvasGroup TrainingEquipmentMenuCanvasGroup;
    [SerializeField] CanvasGroup TrainingEquipmentListCanvasGroup;

    void Awake()
    {
        i = this;        
    }

    void Start()
    {
        tempHeroCommandMenuState = EnumHandler.HeroCommandMenuStates.IDLE;
    }

    void Update()
    {
        if (tempHeroCommandMenuState != heroCommandMenuState) ProcessHeroCommandMenu();
    }

    void ProcessHeroCommandMenu()
    {
        tempHeroCommandMenuState = heroCommandMenuState;

        switch (heroCommandMenuState)
        {
            case EnumHandler.HeroCommandMenuStates.IDLE: // Hide the hero command menu

                break;
            case EnumHandler.HeroCommandMenuStates.ROOT: // Display root contents of hero command menu

                break;
            case EnumHandler.HeroCommandMenuStates.TRAININGEQUIP: // Display training equipment menu

                break;
        }
    }

    void ToggleUI (bool toggle)
    {

    }
}

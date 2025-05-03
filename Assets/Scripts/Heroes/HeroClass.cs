using UnityEngine;

public class HeroClass : MonoBehaviour
{
    [SerializeField] EnumHandler.HeroClasses currentClass;
    public EnumHandler.HeroClasses GetCurrentClass() { return currentClass; }
    public void SetCurrentClass(EnumHandler.HeroClasses heroClass) { this.currentClass = heroClass; }

    public EnumHandler.ArmorClasses GetArmorClass()
    {
        return ClassManager.i.GetArmorClassByHeroClass(currentClass);
    }
}

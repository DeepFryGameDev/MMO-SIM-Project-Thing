using System;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public static ClassManager i;

    private void Awake()
    {
        i = this;
    }

    public EnumHandler.ArmorClasses GetArmorClassByHeroClass(EnumHandler.HeroClasses heroClass)
    {
        switch (heroClass)
        {
            case EnumHandler.HeroClasses.RECRUIT:
                return EnumHandler.ArmorClasses.LEATHER;
            case EnumHandler.HeroClasses.FIGHTER:
                return EnumHandler.ArmorClasses.MAIL;
            case EnumHandler.HeroClasses.ARCHER:
                return EnumHandler.ArmorClasses.LEATHER;
            case EnumHandler.HeroClasses.MAGE:
                return EnumHandler.ArmorClasses.CLOTH;
            case EnumHandler.HeroClasses.CLERIC:
                return EnumHandler.ArmorClasses.MAIL;
            case EnumHandler.HeroClasses.KNIGHT:
                return EnumHandler.ArmorClasses.PLATE;
            default:
                DebugManager.i.ClassDebugOut("ClassManager", "Hero class not found in GetArmorClassByHeroClass().  Likely a new class was added and needs to be added here. Returning cloth by default", true, false);
                return EnumHandler.ArmorClasses.CLOTH;
        }
    }
}

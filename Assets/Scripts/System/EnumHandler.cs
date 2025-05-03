// Purpose: Contains all enums for the project to keep them in one organized location
// Directions: Simply call EnumHandler.enum when looking for them in another script
// Other notes:

public static class EnumHandler
{
    /// <summary>
    /// Used in many different functions for the hero's class
    /// </summary>
    public enum HeroClasses
    {
        RECRUIT,
        FIGHTER,
        ARCHER,
        MAGE,
        CLERIC,
        KNIGHT
    }

    /// <summary>
    /// TOPDOWN is not being used.  BASIC offers full camera control (used while in non-combat scenes).  COMBAT will force the camera to look in the direction the player is facing.
    /// </summary>
    public enum CameraModes
    {
        BASIC,
        COMBAT,
        TOPDOWN
    }

    // --- EQUIPMENT ---
    public enum EquipmentArmorSlots
    {
        HEAD,
        CHEST,
        HANDS,
        LEGS,
        FEET
    }

    public enum EquipmentHandSlots
    {
        MAINHAND,
        OFFHAND
    }

    public enum ArmorClasses
    {
        CLOTH,
        LEATHER,
        MAIL,
        PLATE
    }

    public enum WeaponClasses
    {
        SWORD,
        BOW,
        MACE,
        LANCE,
        STAFF
    }

    public enum ShieldClasses
    {
        TARGE,
        BUCKLER,
        HEATER,
        PAVISE
    }

    // --- INVENTORY ---

    /// <summary>
    /// Not really being used for anything important yet.
    /// </summary>
    public enum InventoryRarities
    {
        JUNK,
        COMMON, // (For training Equipment, there is no physical object the hero interacts with, prefab may be null.)
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    // --- TRAINING ---

    /// <summary>
    /// Types of training that can be used by heros
    /// </summary>
    public enum TrainingTypes
    {
        STRENGTH,
        ENDURANCE,
        DEXTERITY,
        AGILITY,
        INTELLIGENCE,
        FAITH
    }

    // --- PARTY ---

    /// <summary>
    /// Used for hero pathing when in a party
    /// </summary>
    public enum PartyFollowStates
    {
        IDLE, // In the home zone, not following the player
        FOLLOWINBASE, // Following the player, but in base
        FOLLOW // Following the player outside of base
    }

    // --- PATHING ---

    /// <summary>
    /// Used for various logic in the HeroPathing script
    /// </summary>
    public enum pathModes
    {
        IDLE, // Hero is not moving
        RANDOM, // Hero is choosing random points within home zone and pathing to them
        TARGET, // Hero is moving to a designated point (ie whistle command)
        WHISTLE,
        PARTYFOLLOW,
        SENDTOSTARTINGPOINT,
        COMMAND // Not being used yet
    }

    /// <summary>
    /// Used to determine how the hero's move speed and animation should be handled.
    /// </summary>
    public enum pathRunMode
    {
        WALK, // If the hero should only be walking around
        CANRUN, // If the hero should be able to run
        CATCHUP // If the hero needs to catch up to their anchor on the player
    }


    // --- UI ---

    /// <summary>
    /// Used to navigate through the hero command menu.
    /// </summary>
    public enum HeroCommandMenuStates
    {
        IDLE,
        ROOT,
        INVENTORY,
        EQUIP,
        TRAININGEQUIP,
        TRAININGEQUIPLIST,
        SCHEDULE
    }

    /// <summary>
    /// Used to navigate through the player command menu. Not yet implemented.
    /// </summary>
    public enum PlayerCommandMenuStates
    {
        IDLE,
        PARTY,
        ROOT
    }
}

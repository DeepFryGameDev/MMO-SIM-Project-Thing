// Purpose: Contains all enums for the project to keep them in one organized location
// Directions: Simply call EnumHandler.enum when looking for them in another script
// Other notes:

public static class EnumHandler
{
    /// <summary>
    /// Used in many different functions to determine which class the player chose
    /// </summary>
    public enum PlayerClasses
    {
        WARRIOR,
        ARCHER,
        MAGE
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
    
    public enum EquipmentRarities
    {
        COMMON, // (For training Equipment, there is no physical object the hero interacts with, prefab may be null.)
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    public enum TrainingTypes
    {
        STRENGTH,
        ENDURANCE,
        DEXTERITY,
        AGILITY,
        INTELLIGENCE,
        FAITH
    }


    // --- UI ---
    public enum HeroCommandMenuStates
    {
        IDLE,
        ROOT,
        TRAININGEQUIP,
        TRAININGEQUIPLIST
    }

    public enum PlayerCommandMenuStates
    {
        IDLE,
        ROOT
    }
}

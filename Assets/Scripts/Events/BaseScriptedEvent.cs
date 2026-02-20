using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Purpose: Provides the basis for all common events that can be called via script in the world
// Directions: This should be instantiated on any object that should contain an event.  When adding a new type of event interaction, use BaseInteractOnTouch as an example.
// Other notes: This script was taken from an old project - most of it will be rewritten

public class BaseScriptedEvent : MonoBehaviour
{
    //public string method; //name of the method to be run

    //GameMenu menu;

    //DIFFERENT FUNCTIONS THAT CAN BE RUN BY ANY EVENT SCRIPT

    #region ---MOVEMENT---

    /// <summary>
    /// Changes default move speed for player
    /// </summary>
    /// <param name="newMoveSpeed">Move speed to be set (higher = faster)</param>
    public void ChangeDefaultMoveSpeed(float newMoveSpeed)
    {
        
    }

    /// <summary>
    /// Disables player's movement
    /// </summary>
    public void DisablePlayerMovement()
    {

    }

    /// <summary>
    /// Enables player's movement
    /// </summary>
    public void EnablePlayerMovement()
    {

    }

    #endregion

    #region ---BATTLE MANAGEMENT---

    #endregion
    /// <summary>
    /// Moves the player along with any heroes in party to the given scene.
    /// Ensure this is always ran as a coroutine (StartCoroutine)
    /// </summary>
    /// <param name="sceneIndex">The build index of the scene to be loaded</param>
    /// <param name="spawnPosition">The position in the world in the given scene where the player should load</param>
    public IEnumerator BattleTransition(int sceneIndex)
    {
        DebugManager.i.BattleDebugOut("BattleTransition", "Loading Scene - Build Index: " + sceneIndex);
        DebugManager.i.BattleDebugOut("BattleTransition", "Warnings may occur between these lines.  You should be safe to ignore them as they are caused to two scenes existing at once, but double check.");
        DebugManager.i.BattleDebugOut("BattleTransition", "---------------------------------------------------------------------------------------");

        GameSettings.SetSceneTransitionStarted(true);

        GameSettings.SetUnloadSceneIndex(SceneManager.GetActiveScene().buildIndex); // SpawnManager will unload this scene after it detects that it is done loading.

        PlayerMovement.i.ToggleMovement(false); // Disables the player's movement during transition

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty()) // For all heroes in party
        {
            //Debug.Log("Stop " + heroManager.Hero().GetName());
            heroManager.HeroPathing().StopPathing(); // Stop moving and set to idle

            ActiveHeroesSystem.i.SetHeroObject(heroManager.GetID(), heroManager.gameObject); // may not be needed.  They are set once during Awake() in SpawnManager.
            ActiveHeroesSystem.i.SetHeroManager(heroManager); // may not be needed.  They are set once during Awake() in SpawnManager.
        }

        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes()) // For all idle heroes
        {
            //Debug.Log("Stop " + heroManager.Hero().GetName());
            if (heroManager.HeroPathing().GetPathMode() != EnumHandler.pathModes.IDLE) // Set pathing to idle if it isn't already
            {
                heroManager.HeroPathing().StopPathing(); // and stop moving
            }

            ActiveHeroesSystem.i.SetHeroObject(heroManager.GetID(), heroManager.gameObject); // may not be needed.  They are set once during Awake() in SpawnManager.
            ActiveHeroesSystem.i.SetHeroManager(heroManager); // may not be needed.  They are set once during Awake() in SpawnManager.
        }

        yield return UIManager.i.FadeToBlack(true); // The fade to black starts here and runtime will continue once it is done fading.  Should eventually be replaced with a battle transition animation.

        Vector3 spawnPosition = new Vector3(21.6f, -0.09154558f, 29.95f); // <--- i think we can just remove this and have it set up in BattleSetup.
        SpawnManager.i.SetPlayerSpawnPosition(spawnPosition); // <--- this should be set to the middle heroBattleSpawnPoint in the new battle scene.  I think we will eventually just use one template battle scene and use the same positions every time.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive); // And finally load the scene

        while (!asyncLoad.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        DebugManager.i.BattleDebugOut("BattleTransition", "Battle Scene Loaded: " + sceneIndex);
        GameSettings.SetSceneTransitionStarted(false);
    }
    #region ---SCENE MANAGEMENT---

    /// <summary>
    /// Moves the player along with any heroes in party to the given scene.
    /// Ensure this is always ran as a coroutine (StartCoroutine)
    /// </summary>
    /// <param name="sceneIndex">The build index of the scene to be loaded</param>
    /// <param name="spawnPosition">The position in the world in the given scene where the player should load</param>
    public IEnumerator TransitionToScene(int sceneIndex, Vector3 spawnPosition)
    {
        DebugManager.i.SystemDebugOut("TransitionToScene", "Loading Scene - Build Index: " + sceneIndex);
        DebugManager.i.SystemDebugOut("TransitionToScene", "Warnings may occur between these lines.  You should be safe to ignore them as they are caused to two scenes existing at once, but double check.");
        DebugManager.i.SystemDebugOut("TransitionToScene", "---------------------------------------------------------------------------------------");

        GameSettings.SetSceneTransitionStarted(true);

        GameSettings.SetUnloadSceneIndex(SceneManager.GetActiveScene().buildIndex); // SpawnManager will unload this scene after it detects that it is done loading.

        PlayerMovement.i.ToggleMovement(false); // Disables the player's movement during transition

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty()) // For all heroes in party
        {
            //Debug.Log("Stop " + heroManager.Hero().GetName());
            heroManager.HeroPathing().StopPathing(); // Stop moving and set to idle

            ActiveHeroesSystem.i.SetHeroObject(heroManager.GetID(), heroManager.gameObject); // may not be needed.  They are set once during Awake() in SpawnManager.
            ActiveHeroesSystem.i.SetHeroManager(heroManager); // may not be needed.  They are set once during Awake() in SpawnManager.
        }

        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes()) // For all idle heroes
        {
            //Debug.Log("Stop " + heroManager.Hero().GetName());
            if (heroManager.HeroPathing().GetPathMode() != EnumHandler.pathModes.IDLE) // Set pathing to idle if it isn't already
            {
                heroManager.HeroPathing().StopPathing(); // and stop moving
            }
            
            ActiveHeroesSystem.i.SetHeroObject(heroManager.GetID(), heroManager.gameObject); // may not be needed.  They are set once during Awake() in SpawnManager.
            ActiveHeroesSystem.i.SetHeroManager(heroManager); // may not be needed.  They are set once during Awake() in SpawnManager.
        }

        yield return UIManager.i.FadeToBlack(true); // The fade to black starts here and runtime will continue once it is done fading

        SpawnManager.i.SetPlayerSpawnPosition(spawnPosition); // Sets the player to the new spawn position
                
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive); // And finally load the scene
        
        while (!asyncLoad.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        DebugManager.i.SystemDebugOut("TransitionToScene", "Scene Loaded: " + sceneIndex);
        GameSettings.SetSceneTransitionStarted(false);
    }

    //void OpenSave

    //void GameOver

    //void ReturnToTitle

    #endregion

    #region ---GAME MANAGEMENT---

    /// <summary>
    /// Changes switch value (not yet implemented)
    /// </summary>
    /// <param name="whichObject">GameObject with the switch</param>
    /// <param name="whichEvent">Switch's event index</param>
    /// <param name="whichSwitch">The switch to be changed</param>
    /// <param name="whichBool">To change the switch to true or false</param>
    public void ChangeSwitch(GameObject whichObject, int whichEvent, int whichSwitch, bool whichBool)
    {
        /*
        BaseDialogueEvent e = whichObject.GetComponent<DialogueEvents>().eventOrDialogue[whichEvent];

        if (whichSwitch == 1)
        {
            e.switch1 = whichBool;
        }
        else if (whichSwitch == 2)
        {
            e.switch2 = whichBool;
        }*/
    }

    /// <summary>
    /// Returns if switch is true or false on given object (not yet fully implemented)
    /// </summary>
    /// <param name="whichObject">GameObject with the switch</param>
    /// <param name="whichEvent">Switch's event index</param>
    /// <param name="whichSwitch">The switch to be returned</param>
    public bool GetSwitchBool(GameObject whichObject, int whichEvent, int whichSwitch)
    {/*
        BaseDialogueEvent e = whichObject.GetComponent<DialogueEvents>().eventOrDialogue[whichEvent];

        if (whichSwitch == 1)
        {
            return e.switch1;
        }
        else if (whichSwitch == 2)
        {
            return e.switch2;
        }
        else
        {
            Debug.Log("GetSwitchBool - invalid switch: " + whichSwitch);
            return false;
        }*/
        return false;
    }

    /// <summary>
    /// Changes value of global event bools
    /// </summary>
    /// <param name="index">Index of the global bool</param>
    /// <param name="boolean">Change bool to true or false</param>
    public void ChangeGlobalBool(int index, bool boolean)
    {
        //GlobalBoolsDB.instance.globalBools[index] = boolean;
    }

    #endregion

    #region ---MUSIC/SOUNDS---

    /// <summary>
    /// Plays given sound effect once
    /// </summary>
    /// <param name="SE">Sound effect to play</param>
    void PlaySE(AudioClip SE)
    {
        // audioSource.PlayOneShot(SE);
    }

    //void PlayBGM

    //ienumerator FadeOutBGM

    //ienumerator FadeInBGM

    //void PlayBGS

    //ienumerator FadeOutBGS

    //void StopBGM

    //void StopSE

    #endregion

    #region ---TIMING---

    /// <summary>
    /// Halt processing for given seconds
    /// </summary>
    /// <param name="waitTime">Time to move in seconds</param>
    public IEnumerator WaitForSeconds(float waitTime) //pause for period of time
    {
        yield return new WaitForSeconds(waitTime);
    }

    #endregion

    #region ---DIALOGUE---

    #endregion

    #region ---QUESTS---

    /// <summary>
    /// Adds given quest to active quests list
    /// </summary>
    /// <param name="ID">ID of quest in QuestDB</param>
    public void StartQuest(int ID)
    {

    }

    /// <summary>
    /// Marks given quest as complete
    /// </summary>
    /// <param name="ID">Quest to check</param>
    public void CompleteQuest(int ID)
    {
        //QuestDB.instance.CompleteQuest(quest);
    }

    /// <summary>
    /// Marks bool of given quest as given value if quest type is 'bool'
    /// </summary>
    /// <param name="ID">Quest to check</param>
    /// <param name="index">Index of bool in quest</param>
    /// <param name="value">To mark the bool as true or false</param>
    public void MarkQuestBool(int ID, int index, bool value)
    {
        
    }

    /// <summary>
    /// Returns given quest and index bool value
    /// </summary>
    /// <param name="ID">Quest to check</param>
    /// <param name="index">Index of bool in quest</param>
    public bool QuestBool(int ID, int index)
    {
        return false;
    }

    /// <summary>
    /// Returns Quest by ID   	
    /// </summary>
    /// <param name="type">Use DB, Active, or Complete</param>
    /// <param name="ID">ID of quest</param>
    /*public BaseQuest GetQuestByID(string type, int ID)
    {
        if (type == "DB")
        {
            foreach (BaseQuest quest in QuestDB.instance.quests)
            {
                if (quest.ID == ID)
                {
                    return quest;
                }
            }
        }

        if (type == "Active")
        {
            foreach (BaseQuest quest in GameManager.instance.activeQuests)
            {
                if (quest.ID == ID)
                {
                    return quest;
                }
            }
        }

        if (type == "Complete")
        {
            foreach (BaseQuest quest in GameManager.instance.completedQuests)
            {
                if (quest.ID == ID)
                {
                    return quest;
                }
            }
        }

        return null;
    }*/

    /// <summary>
    /// Returns given quest by ID from QuestDB
    /// </summary>
    /// <param name="ID">ID of quest in QuestDB</param>
    /*public BaseQuest GetQuest(int ID)
    {
        foreach (BaseQuest quest in QuestDB.instance.quests)
        {
            if (quest.ID == ID)
            {
                return quest;
            }
        }

        return null;
    }*/

    #endregion

    #region ---SYSTEM SETTINGS---

    //void ChangeBattleBGM

    /// <summary>
    /// Enables or disables ability to open menu
    /// </summary>
    /// <param name="canOpen">If true, menu can be opened</param>
    public void ChangeMenuAccess(bool canOpen)
    {

    }

    #endregion

    #region ---SPRITES---

    //void ChangeGraphic

    //void ChangeOpacity

    //void AddSprite

    //void RemoveSprite

    #endregion

    #region ---ACTORS---

    /// <summary>
    /// Fully restores HP and MP of all active heroes
    /// </summary>
    public void FullHeal()
    {
        
    }

    /// <summary>
    /// Adds or subtracts current HP of given hero
    /// </summary>
    /// <param name="ID">Hero to modify HP</param>
    /// <param name="hp">CurrentHP to add/subtract (subtract with negative value)</param>
    public void ChangeHP(int ID, int hp)
    {
        
    }

    /// <summary>
    /// Sets current HP or given hero to given value
    /// </summary>
    /// <param name="ID">Hero to modify HP</param>
    /// <param name="hp">HP value to set current HP</param>
    public void SetHP(int ID, int hp)
    {

    }

    /// <summary>
    /// Adds or subtracts current MP of given hero
    /// </summary>
    /// <param name="hero">Hero to modify MP</param>
    /// <param name="mp">MP value to set current MP</param>
    public void ChangeMP(int ID, int mp)
    {
       
    }

    /// <summary>
    /// Sets current MP of given hero to given value
    /// </summary>
    /// <param name="ID">Hero to modify MP</param>
    /// <param name="mp">MP value to set current MP</param>
    public void SetMP(int ID, int mp)
    {

    }

    /// <summary>
    /// Adds or subtracts current EXP of given hero (de-leveling not yet supported)
    /// </summary>
    public void ChangeEXP(int ID, int exp)
    {
    }

    /// <summary>
    /// Adds or subtracts given base stat by given value to given hero
    /// </summary>
    /// <param name="ID">Hero to modify parameter</param>
    /// <param name="parameter">Use: "Strength", "Stamina", "Agility", "Dexterity", "Intelligence" or "Spirit"</param>
    /// <param name="paramChange">Value to be added/subtracted</param>
    public void ChangeParameter(int ID, string parameter, int paramChange)
    {
        
    }

    //void AddSkill

    //void RemoveSkill

    /// <summary>
    /// Equip given equipment for given hero
    /// </summary>
    /// <param name="ID">Hero to change equipment</param>
    /// <param name="equipName">Name of the equipment to be equipped</param>
    public void ChangeEquipment(int ID, string equipName)
    {
        
    }

    /// <summary>
    /// Change name of given hero with given string
    /// </summary>
    /// <param name="ID">Hero to modify name</param>
    /// <param name="name">New name of the given hero</param>
    public void ChangeName(int ID, string name)
    {
        
    }

    #endregion

    #region ---PARTY---

    /// <summary>
    /// Adds or subtracts given gold
    /// </summary>
    /// <param name="gold">Number of gold to be added/subtracted</param>
    public void ChangeGold(int gold)
    {
        GameManager.ChangeGold(gold);
    }

    /// <summary>
    /// Adds given item to inventory
    /// </summary>
    /// <param name="ID">ID of item from ItemDB to add</param>
    /// <param name="numberToAdd">Number of the given item to add to inventory</param>
    public void AddItem(int ID, int numberToAdd)
    {
    }

    /// <summary>
    /// Adds 1 of given item to inventory
    /// </summary>
    /// <param name="ID">ID of item from ItemDB to add</param>
    public void AddItem(int ID)
    {

    }

    /// <summary>
    /// Removes given item from inventory
    /// </summary>
    /// <param name="ID">ID of item from ItemDB to remove</param>
    /// <param name="numberToRemove">Number of the given item to remove from inventory</param>
    public void RemoveItem(int ID, int numberToRemove)
    {

    }

    /// <summary>
    /// Removes 1 of given item from inventory
    /// </summary>
    /// <param name="ID">ID of item from ItemDB to remove</param>
    public void RemoveItem(int ID)
    {

    }

    /// <summary>
    /// Adds given equipment to inventory
    /// </summary>
    /// <param name="ID">ID of equipment from EquipmentDB to add</param>
    /// <param name="numberToAdd">Number of the given equipment to add to inventory</param>
    public void AddEquipment(int ID, int numberToAdd)
    {

    }

    /// <summary>
    /// Adds 1 of given equipment to inventory
    /// </summary>
    /// <param name="ID">ID of equipment from EquipmentDB to add</param>
    public void AddEquipment(int ID)
    {

    }

    /// <summary>
    /// Removes given equipment from inventory
    /// </summary>
    /// <param name="ID">ID of equipment from EquipmentDB to add</param>
    /// <param name="numberToRemove">Number of the given equipment to add to inventory</param>
    public void RemoveEquipment(int ID, int numberToRemove) //removes equipment from inventory
    {
        
    }

    /// <summary>
    /// Removes 1 of given equipment from inventory
    /// </summary>
    /// <param name="ID">ID of equipment from EquipmentDB to add</param>
    public void RemoveEquipment(int ID) //removes equipment from inventory
    {

    }

    //void ChangePartyMember

    #endregion

    #region ---IMAGES---

    //void ShowPicture

    //void MovePicture

    //void RotatePicture

    //void TintPicture

    //void RemovePicture

    #endregion

    #region ---WEATHER/EFFECTS---

    //void FadeInScreen

    //void FadeOutScreen

    //void TintScreen

    //void FlashScreen

    //void ShakeScreen

    #endregion

    #region ---TOOLS FOR EVENTS---

    #endregion
}


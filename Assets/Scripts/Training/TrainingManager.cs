using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose: This script handles all training based calculations and scripting
// Directions: Just attach to [System]
// Other notes: 

public class TrainingManager : MonoBehaviour
{
    [HideInInspector] public List<HeroManager> heroManagers = new List<HeroManager>(); // Used to manage all active heroes
    public void AddToHeroManagers(HeroManager heroManager) { heroManagers.Add(heroManager); }

    ScheduleManager scheduleManager;

    Transform layoutGroupTransform; // Used to add TrainingResults to the UI

    CanvasGroup canvasGroup; // Used to hide/show the Training Results UI.

    // For training gen
    string level1BasicStrengthTrainingName = "Lv. 1) Basic Strength Training";
    public string GetLevel1BasicStrengthTrainingName() { return level1BasicStrengthTrainingName; }
    string level1BasicEnduranceTrainingName = "Lv. 1) Basic Endurance Training";
    public string GetLevel1BasicEnduranceTrainingName() { return level1BasicEnduranceTrainingName; }
    string level1BasicAgilityTrainingName = "Lv. 1) Basic Agility Training";
    public string GetLevel1BasicAgilityTrainingName() { return level1BasicAgilityTrainingName; }
    string level1BasicDexterityTrainingName = "Lv. 1) Basic Dexterity Training";
    public string GetLevel1BasicDexterityTrainingName() { return level1BasicDexterityTrainingName; }
    string level1BasicIntelligenceTrainingName = "Lv. 1) Basic Intelligence Training";
    public string GetLevel1BasicIntelligenceTrainingName() { return level1BasicIntelligenceTrainingName; }
    string level1BasicFaithTrainingName = "Lv. 1) Basic Faith Training";
    public string GetLevel1BasicFaithTrainingName() { return level1BasicFaithTrainingName; }

    private void Awake()
    {
        layoutGroupTransform = GameObject.Find("TrainingResultsCanvas/Holder/LayoutGroup").transform; // hacky, will need a better solution eventually
        canvasGroup = GameObject.Find("TrainingResultsCanvas/Holder").GetComponent<CanvasGroup>(); // ^

        scheduleManager = transform.GetComponent<ScheduleManager>();
    }

    /// <summary>
    /// When the week is progressed, all active heroes have this called to process the training exp for the week.  This will likely be a large method eventually.
    /// </summary>
    /// <param name="heroManager">The HeroManager for the hero in which to process their training.</param>
    public void ProcessTraining(HeroManager heroManager)
    {
        // 1. Always run first.
        CacheExpAndEnergy(heroManager);

        // 2. Calculate the training and stuff.
        CalculateTrainingResult(heroManager);

        // 3. Increase EXP, Decrease energy
        IncreaseEXP(heroManager);
        DecreaseEnergy(heroManager);

        // 4. Post training stuff. 
    }

    /// <summary>
    /// Before training is processed, this will set temporary values to be used to display training progress.
    /// </summary>
    /// <param name="heroManager">The HeroManager of the hero to cache</param>
    void CacheExpAndEnergy(HeroManager heroManager)
    {
        // current exp
        heroManager.HeroTraining().SetTempExp(GetHeroExpByType(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager));

        // current energy
        heroManager.HeroTraining().SetTempEnergy(heroManager.Hero().GetEnergy());

        //set the temp effectiveness
        SetTempEffectiveness(heroManager);
    }

    /// <summary>
    /// For caching.  Simply just sets the tempEffectiveness val on the given heroManager
    /// </summary>
    /// <param name="heroManager">HeroManager for the hero to set temp effectiveness</param>
    void SetTempEffectiveness(HeroManager heroManager)
    {
        heroManager.HeroTraining().SetTempEffectiveness(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetEffectiveness());
    }

    /// <summary>
    /// Processes all the different methods to spit out the exp for training.  The result is saved to the hero's HeroManager TrainingResult
    /// </summary>
    /// <param name="heroManager">HeroManager of the Hero to calulate and assign the Training Result</param>
    public void CalculateTrainingResult(HeroManager heroManager)
    {
        // -- EXP gathering

        // get random value between the expVariance around the levelToExpMod and training level
        float randomExp = UnityEngine.Random.Range(
            (TrainingSettings.trainingLevelToExpBaseMod * heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingLevel()) - TrainingSettings.trainingExpVariance,
            (TrainingSettings.trainingLevelToExpBaseMod * heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingLevel()) + TrainingSettings.trainingExpVariance);
        Debug.Log("CalculateTrainingResult() -  exp before result: " + randomExp);

        // take energy into account - if lower than 25% energy, effectiveness goes down
        Debug.Log("Effectiveness before: " + heroManager.HeroSchedule().GetCurrentEventAsTraining().GetEffectiveness());
        float effectiveness = GetEffectivenessFromEnergy(heroManager);
        Debug.Log("Effectiveness after: " + effectiveness);
        // ---- here is where buffs from players, like items or skills or something applied to heroes can increase the exp gained by training - just tweak the effectiveness.

        // Apply effectiveness to final result.  Should aim to get between 6-13ish exp.
        float tempExp = randomExp * effectiveness;

        tempExp *= TrainingSettings.focusExpMod; // This is always 1 until focus is added.

        tempExp *= TrainingSettings.levelGapExpMod; // This is always 1 until levelGapExp is added

        int roundedExp = Mathf.RoundToInt(tempExp); // Rounds to int

        heroManager.HeroTraining().SetTrainingResult(roundedExp);

        Debug.Log("Training Result: " + heroManager.HeroTraining().GetTrainingResult());
    }


    /// <summary>
    /// Coroutine - Displays the training results to the player with animated fill bars
    /// </summary>
    /// <returns>Yields for DateSettings.trainingResultsFillDelaySeconds and then animates the fill bars</returns>
    public IEnumerator ShowTrainingResults()
    {
        // for each heroManager in heroManagers {
        foreach (HeroManager heroManager in heroManagers)
        {
            // Create new TrainingResult prefab and instantiate it in the LayoutGroup.
            GameObject newTrainingResult = Instantiate(PrefabManager.i.TrainingResult, layoutGroupTransform);

            TrainingResultHandler trh = newTrainingResult.GetComponent<TrainingResultHandler>();

            heroManager.HeroTraining().SetTempTRH(trh);

            // Set facePanel to hero face
            trh.SetFaceImage(heroManager.faceImage);

            // check if leveling up.  If so, set previous level vals as we already leveled up.
            if (heroManager.HeroTraining().GetLevelingUp())
            {
                // should be tempExp / getExpRequiredForPriorLevelup
                float expFillVal = heroManager.HeroTraining().GetTempExp() / GetExpRequiredForPriorLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);
                trh.SetExpFill(expFillVal);

                string curExp = heroManager.HeroTraining().GetTempExp() + " / " + GetExpRequiredForPriorLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);
                trh.SetExpFillText(curExp);
            }
            else
            {
                // if not leveling up:
                // Set fill to curExp/to level
                float expFillVal = heroManager.HeroTraining().GetTempExp() / GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);
                trh.SetExpFill(expFillVal);

                // set TotalExpText to hero's current EXP + / + hero's next exp levelup
                string curExp = heroManager.HeroTraining().GetTempExp() + " / " + GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);
                trh.SetExpFillText(curExp);
            }

            // set EnergyFill to (hero's current energy / hero's max energy)
            float energyFillVal = (float)heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy;
            trh.SetEnergyFill(energyFillVal);

            // set StatText to leveled stat
            trh.SetStatText(GetStatPrefixByType(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType()));

            // set expGainedText to training result exp
            trh.SetExpText("+" + heroManager.HeroTraining().GetTrainingResult());
        }

        // show panel after x seconds
        StartCoroutine(DisplayResultPanelAfterDelay());

        // wait x seconds
        yield return new WaitForSeconds(DateSettings.trainingResultsFillDelaySeconds);

        // fill bars and update TotalExpText over x seconds
        foreach (HeroManager heroManager in heroManagers)
        {
            StartCoroutine(UpdateExpFill(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager));
            StartCoroutine(UpdateEnergyFill(heroManager));
        }

        yield return new WaitForSeconds(DateSettings.trainingResultShowSeconds - DateSettings.trainingResultsDelaySeconds);

        ToggleCanvasGroup(false);

        // reset stuff
        ResetVars();
    }

    /// <summary>
    /// Resets needed vars to be used on the next training.  Add as needed
    /// </summary>
    void ResetVars()
    {
        foreach (HeroManager heroManager in heroManagers)
        {
            heroManager.HeroTraining().SetTempTRH(null);
            heroManager.HeroTraining().SetLevelingUp(false);
        }

        heroManagers.Clear();

        foreach (Transform transform in layoutGroupTransform)
        {
            Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// Coroutine - simply just waits for DateSettings.trainingResultsDelaySeconds and then displays the Training Results panel.
    /// </summary>
    /// <returns>Just waits for DateSettings.trainingResultsDelaySeconds</returns>
    IEnumerator DisplayResultPanelAfterDelay()
    {
        yield return new WaitForSeconds(DateSettings.trainingResultsDelaySeconds);
        ToggleCanvasGroup(true);
    }

    /// <summary>
    /// Coroutine - Fills the exp bars and sets the text for each Training Results UI object
    /// </summary>
    /// <param name="trainingType">The training type that should be displayed</param>
    /// <param name="heroManager">The HeroManager for the Hero to be displayed</param>
    /// <returns>Fills the bar over DateSettings.trainingResultsFillSeconds</returns>
    IEnumerator UpdateExpFill(EnumHandler.TrainingTypes trainingType, HeroManager heroManager)
    {
        float timer = 0;

        float tempFillTextExp = 0;

        float startExp = heroManager.HeroTraining().GetTempExp() / GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);
        float endExp = (heroManager.HeroTraining().GetTempExp() +
            heroManager.HeroTraining().GetTrainingResult()) / GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);

        // check for levelup to determine which case to use.

        float levelUpGap = GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager) - heroManager.HeroTraining().GetTempExp();

        // Debug.Log("Bar should start at " + startExp + " and should end at " + endExp);

        float tempFillBarExp = startExp;
        // need 2 cases.  If leveling up, we need to show the 2 different sets to account for spillover.
        if (heroManager.HeroTraining().GetLevelingUp())
        {
            float startBeforeSpillExp = heroManager.HeroTraining().GetTempExp() / GetExpRequiredForPriorLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager);

            // Need to fill the first part - use tempExp until that exceeds the priorExpNeeded

            // Then start from 0 and fill in the second part
            bool spillOver = false;

            while (timer < DateSettings.trainingResultsFillSeconds)
            {
                timer += Time.deltaTime;
                float fillPercent = timer / DateSettings.trainingResultsFillSeconds;         
               
                if (!spillOver) // before spillover - show info from before levelup
                {
                    // Text - this works by simply muliplying the % of the timer to the total needed exp gap, and adding that to the starting point.
                    tempFillTextExp = heroManager.HeroTraining().GetTrainingResult() * fillPercent;
                    int roundedTempExp = Mathf.RoundToInt(tempFillTextExp) + heroManager.HeroTraining().GetTempExp();

                    heroManager.HeroTraining().GetTempTRH().SetExpFillText(roundedTempExp + " / " +
                        GetExpRequiredForPriorLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager));

                    // Bar fill seems to be working?
                    tempFillBarExp = startBeforeSpillExp + (endExp - startExp) * fillPercent;

                    heroManager.HeroTraining().GetTempTRH().SetExpFill(tempFillBarExp);

                    if (roundedTempExp >= GetExpRequiredForPriorLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager))
                    {
                        spillOver = true;
                        heroManager.HeroTraining().GetTempTRH().ToggleLevelUpLabel(true);
                    }

                } else // show info from after levelup
                {
                    // Text - this works by simply muliplying the % of the timer to the total needed exp gap, and adding that to the starting point.
                    tempFillTextExp = heroManager.HeroTraining().GetTrainingResult() * fillPercent;
                    int roundedTempExp = Mathf.RoundToInt(tempFillTextExp) + heroManager.HeroTraining().GetTempExp();

                    roundedTempExp = (Mathf.RoundToInt(GetExpRequiredForPriorLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager) - roundedTempExp));
                    roundedTempExp = Mathf.Abs(roundedTempExp);
                    // Debug.Log("roundedTempExp: " + roundedTempExp);

                    heroManager.HeroTraining().GetTempTRH().SetExpFillText(roundedTempExp + " / " +
                        GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager));

                    // Bar fill seems to be working?
                    tempFillBarExp = (roundedTempExp / GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager)) * fillPercent;

                    heroManager.HeroTraining().GetTempTRH().SetExpFill(tempFillBarExp);
                }                    

                yield return new WaitForEndOfFrame();
            }
        }
        else  // This is for when there is no levelup involved, will run most of the time.
        {          
            while (timer < DateSettings.trainingResultsFillSeconds) // (timer / trainingResultsFillSeconds) will return the % of the fill time.
            {
                timer += Time.deltaTime;
                float fillPercent = timer / DateSettings.trainingResultsFillSeconds;

                // Text - this works by simply muliplying the % of the timer to the total needed exp gap, and adding that to the starting point.
                tempFillTextExp = heroManager.HeroTraining().GetTrainingResult() * fillPercent;
                int roundedTempExp = Mathf.RoundToInt(tempFillTextExp) + heroManager.HeroTraining().GetTempExp(); // the GetStrengthExp will need to be tweaked later for the current training type

                heroManager.HeroTraining().GetTempTRH().SetExpFillText(roundedTempExp + " / " +
                    GetExpRequiredForLevelUp(heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType(), heroManager));

                // Bar Fill            
                tempFillBarExp = startExp + (endExp - startExp) * fillPercent;

                heroManager.HeroTraining().GetTempTRH().SetExpFill(tempFillBarExp);

                yield return new WaitForEndOfFrame();
            }
        }            
    }

    /// <summary>
    /// Coroutine - Fills the energy bars
    /// </summary>
    /// <param name="heroManager">HeroManager for the hero to have energy bars filled</param>
    /// <returns>Fills the bar over DateSettings.trainingResultsFillSeconds</returns>
    IEnumerator UpdateEnergyFill(HeroManager heroManager)
    {
        float timer = 0;

        float startEnergy = ((float) heroManager.HeroTraining().GetTempEnergy() / HeroSettings.maxEnergy);
        float endEnergy = ((float)(heroManager.HeroTraining().GetTempEnergy() - GetEnergyCost(heroManager)) / HeroSettings.maxEnergy);

        float tempFillBarEnergy = startEnergy;

        // over DateSettings.trainingResultsFillSeconds, reduce the energy bar
        while (timer < DateSettings.trainingResultsFillSeconds)
        {
            timer += Time.deltaTime;
            float fillPercent = timer / DateSettings.trainingResultsFillSeconds;

            //fillPercent = 1 - fillPercent;

            tempFillBarEnergy = startEnergy - ((GetEnergyCost(heroManager) * .01f) * fillPercent);

            heroManager.HeroTraining().GetTempTRH().SetEnergyFill(tempFillBarEnergy);

            yield return new WaitForEndOfFrame();
        }

    }

    /// <summary>
    /// Just returns the hero's stat exp by the given training type
    /// </summary>
    /// <param name="trainingType">Stat to return</param>
    /// <param name="heroManager">HeroManager of the Hero in question</param>
    /// <returns>Experience points of given stat</returns>
    int GetHeroExpByType(EnumHandler.TrainingTypes trainingType, HeroManager heroManager)
    {
        switch (trainingType)
        {
            case EnumHandler.TrainingTypes.STRENGTH:
                return heroManager.HeroTraining().GetStrengthExp();
            case EnumHandler.TrainingTypes.ENDURANCE:
                return heroManager.HeroTraining().GetEnduranceExp();
            case EnumHandler.TrainingTypes.AGILITY:
                return heroManager.HeroTraining().GetAgilityExp();
            case EnumHandler.TrainingTypes.DEXTERITY:
                return heroManager.HeroTraining().GetDexterityExp();
            case EnumHandler.TrainingTypes.INTELLIGENCE:
                return heroManager.HeroTraining().GetIntelligenceExp();
            case EnumHandler.TrainingTypes.FAITH:
                return heroManager.HeroTraining().GetFaithExp();
            default:
                Debug.LogWarning("TrainingManager: GetHeroExpByType - no trainingType found");
                return 0;
        }
    }

    /// <summary>
    /// Simply just multiplies the hero's trained stat by the modifier in TrainingSettings
    /// </summary>
    /// <returns>Stat level * TrainingSettings.heroStatExpFromTrainingMod</returns>
    public float GetExpRequiredForLevelUp(EnumHandler.TrainingTypes trainingType, HeroManager heroManager)
    {
        switch (trainingType)
        {
            case EnumHandler.TrainingTypes.STRENGTH:
                //Debug.Log("Hero strength: " + heroManager.Hero().GetStrength() + " * " + TrainingSettings.heroStatExpFromTrainingMod + " = "
                //    + heroManager.Hero().GetStrength() * TrainingSettings.heroStatExpFromTrainingMod);

                return heroManager.Hero().GetBaseStrength() * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.ENDURANCE:
                return heroManager.Hero().GetBaseEndurance() * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.AGILITY:
                return heroManager.Hero().GetBaseAgility() * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.DEXTERITY:
                return heroManager.Hero().GetBaseDexterity() * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.INTELLIGENCE:
                return heroManager.Hero().GetBaseIntelligence() * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.FAITH:
                return heroManager.Hero().GetBaseFaith() * TrainingSettings.heroStatExpFromTrainingMod;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Used to get the last level's exp required
    /// </summary>
    /// <returns>Stat level * TrainingSettings.heroStatExpFromTrainingMod</returns>
    float GetExpRequiredForPriorLevelUp(EnumHandler.TrainingTypes trainingType, HeroManager heroManager)
    {
        switch (trainingType)
        {
            case EnumHandler.TrainingTypes.STRENGTH:
                //Debug.Log("Hero strength: " + heroManager.Hero().GetStrength() + " * " + TrainingSettings.heroStatExpFromTrainingMod + " = "
                //    + heroManager.Hero().GetStrength() * TrainingSettings.heroStatExpFromTrainingMod);

                return (heroManager.Hero().GetBaseStrength() - 1) * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.ENDURANCE:
                return (heroManager.Hero().GetBaseEndurance() - 1) * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.AGILITY:
                return (heroManager.Hero().GetBaseAgility() - 1) * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.DEXTERITY:
                return (heroManager.Hero().GetBaseDexterity() - 1) * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.INTELLIGENCE:
                return (heroManager.Hero().GetBaseIntelligence() - 1) * TrainingSettings.heroStatExpFromTrainingMod;
            case EnumHandler.TrainingTypes.FAITH:
                return (heroManager.Hero().GetBaseFaith() - 1) * TrainingSettings.heroStatExpFromTrainingMod;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Returns the prefix of the stat by the given training type
    /// </summary>
    /// <param name="trainingType">Stat to return the prefix</param>
    /// <returns>Prefix in 3 letter string format</returns>
    string GetStatPrefixByType(EnumHandler.TrainingTypes trainingType)
    {
        switch (trainingType)
        {
            case EnumHandler.TrainingTypes.STRENGTH:
                return "STR";
            case EnumHandler.TrainingTypes.ENDURANCE:
                return "END";
            case EnumHandler.TrainingTypes.AGILITY:
                return "AGI";
            case EnumHandler.TrainingTypes.DEXTERITY:
                return "DEX";
            case EnumHandler.TrainingTypes.INTELLIGENCE:
                return "INT";
            case EnumHandler.TrainingTypes.FAITH:
                return "FTH";
            default:
                return "NA";
        }
    }

    /// <summary>
    /// Increases the hero's experience points based on their Training Result.
    /// </summary>
    /// <param name="heroManager">HeroManager for the hero to increase EXP</param>
    void IncreaseEXP(HeroManager heroManager)
    {
        switch (heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingType())
        {
            case EnumHandler.TrainingTypes.STRENGTH:
                // increase strength exp
                heroManager.HeroTraining().SetStrengthExp(heroManager.HeroTraining().GetStrengthExp() + heroManager.HeroTraining().GetTrainingResult());

                // if strength exp >= GetExpRequiredForLevelUp()
                if (heroManager.HeroTraining().GetStrengthExp() >= GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager))
                {
                    // get difference from tempExp and to next level
                    float originLevelExp = GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager) - heroManager.HeroTraining().GetTempExp(); // this is the exp given before levelup
                    // subtract it from the total result
                    float spillOverExp = heroManager.HeroTraining().GetTrainingResult() - originLevelExp; // This is the exp given after levelup

                    // process strength stat level up
                    StatLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager);

                    // Set exp to spill over
                    heroManager.HeroTraining().SetStrengthExp(Mathf.RoundToInt(spillOverExp));
                }

                Debug.Log(heroManager.Hero().name + "'s strength EXP is: " + heroManager.HeroTraining().GetStrengthExp() + "/" + GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.STRENGTH, heroManager)
                    + " and strength level is: " + heroManager.Hero().GetStrength());

                break;
            case EnumHandler.TrainingTypes.ENDURANCE:
                // increase endurance exp
                heroManager.HeroTraining().SetStrengthExp(heroManager.HeroTraining().GetEnduranceExp() + heroManager.HeroTraining().GetTrainingResult());

                // if endurance exp >= GetExpRequiredForLevelUp()
                if (heroManager.HeroTraining().GetEnduranceExp() >= GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager))
                {
                    // process endurance stat level up
                    StatLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager);

                    // reset endurance exp to 0 (should be spill over value to go into the next level.  That logic should come soon.)
                    heroManager.HeroTraining().SetEnduranceExp(0);
                }

                Debug.Log(heroManager.Hero().name + "'s endurance EXP is: " + heroManager.HeroTraining().GetEnduranceExp() + "/" + GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.ENDURANCE, heroManager)
                    + " and endurance level is: " + heroManager.Hero().GetEndurance());
                break;
            case EnumHandler.TrainingTypes.AGILITY:
                // increase agility exp
                heroManager.HeroTraining().SetAgilityExp(heroManager.HeroTraining().GetAgilityExp() + heroManager.HeroTraining().GetTrainingResult());

                // if agility exp >= GetExpRequiredForLevelUp()
                if (heroManager.HeroTraining().GetAgilityExp() >= GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager))
                {
                    // process agility stat level up
                    StatLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager);

                    // reset agility exp to 0 (should be spill over value to go into the next level.  That logic should come soon.)
                    heroManager.HeroTraining().SetAgilityExp(0);
                }

                Debug.Log(heroManager.Hero().name + "'s agility EXP is: " + heroManager.HeroTraining().GetAgilityExp() + "/" + GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.AGILITY, heroManager)
                    + " and agility level is: " + heroManager.Hero().GetAgility());
                break;
            case EnumHandler.TrainingTypes.DEXTERITY:
                // increase Dexterity exp
                heroManager.HeroTraining().SetDexterityExp(heroManager.HeroTraining().GetDexterityExp() + heroManager.HeroTraining().GetTrainingResult());

                // if agility exp >= GetExpRequiredForLevelUp()
                if (heroManager.HeroTraining().GetDexterityExp() >= GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager))
                {
                    // process agility stat level up
                    StatLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager);

                    // reset agility exp to 0 (should be spill over value to go into the next level.  That logic should come soon.)
                    heroManager.HeroTraining().SetDexterityExp(0);
                }

                Debug.Log(heroManager.Hero().name + "'s dexterity EXP is: " + heroManager.HeroTraining().GetDexterityExp() + "/" + GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.DEXTERITY, heroManager)
                    + " and dexterity level is: " + heroManager.Hero().GetDexterity());
                break;
            case EnumHandler.TrainingTypes.INTELLIGENCE:
                // increase intelligence exp
                heroManager.HeroTraining().SetIntelligenceExp(heroManager.HeroTraining().GetIntelligenceExp() + heroManager.HeroTraining().GetTrainingResult());

                // if intelligence exp >= GetExpRequiredForLevelUp()
                if (heroManager.HeroTraining().GetIntelligenceExp() >= GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager))
                {
                    // process intelligence stat level up
                    StatLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager);

                    // reset intelligence exp to 0 (should be spill over value to go into the next level.  That logic should come soon.)
                    heroManager.HeroTraining().SetIntelligenceExp(0);
                }

                Debug.Log(heroManager.Hero().name + "'s intelligence EXP is: " + heroManager.HeroTraining().GetIntelligenceExp() + "/" + GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.INTELLIGENCE, heroManager)
                    + " and intelligence level is: " + heroManager.Hero().GetIntelligence());
                break;
            case EnumHandler.TrainingTypes.FAITH:
                // increase faith exp
                heroManager.HeroTraining().SetFaithExp(heroManager.HeroTraining().GetFaithExp() + heroManager.HeroTraining().GetTrainingResult());

                // if faith exp >= GetExpRequiredForLevelUp()
                if (heroManager.HeroTraining().GetFaithExp() >= GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager))
                {
                    // process faith stat level up
                    StatLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager);

                    // reset faith exp to 0 (should be spill over value to go into the next level.  That logic should come soon.)
                    heroManager.HeroTraining().SetFaithExp(0);
                }

                Debug.Log(heroManager.Hero().name + "'s faith EXP is: " + heroManager.HeroTraining().GetFaithExp() + "/" + GetExpRequiredForLevelUp(EnumHandler.TrainingTypes.FAITH, heroManager)
                    + " and faith level is: " + heroManager.Hero().GetFaith());

                break;
        }
    }

    /// <summary>
    /// When a hero's stat should level up, this function is called
    /// </summary>
    /// <param name="trainingType">The stat that should be leveled up</param>
    /// <param name="heroManager">HeroManager for the hero that should have their stat leveled up</param>
    void StatLevelUp(EnumHandler.TrainingTypes trainingType, HeroManager heroManager)
    {
        Debug.Log(heroManager.Hero().name + " - LEVELUP");
        heroManager.HeroTraining().SetLevelingUp(true);

        switch (trainingType)
        {
            case EnumHandler.TrainingTypes.STRENGTH:
                heroManager.Hero().SetBaseStrength(heroManager.Hero().GetBaseStrength() + 1);
                break;
            case EnumHandler.TrainingTypes.ENDURANCE:
                heroManager.Hero().SetBaseEndurance(heroManager.Hero().GetBaseEndurance() + 1);
                break;
            case EnumHandler.TrainingTypes.AGILITY:
                heroManager.Hero().SetBaseAgility(heroManager.Hero().GetBaseAgility() + 1);
                break;
            case EnumHandler.TrainingTypes.DEXTERITY:
                heroManager.Hero().SetBaseDexterity(heroManager.Hero().GetBaseDexterity() + 1);
                break;
            case EnumHandler.TrainingTypes.INTELLIGENCE:
                heroManager.Hero().SetBaseIntelligence(heroManager.Hero().GetBaseIntelligence() + 1);
                break;
            case EnumHandler.TrainingTypes.FAITH:
                heroManager.Hero().SetBaseFaith(heroManager.Hero().GetBaseFaith() + 1);
                break;
        }
    }

    /// <summary>
    /// If the hero's energy is lower than 25%, the effectiveness is reduced.  This can be expanded on later.
    /// </summary>
    /// <returns>tempEffectiveness * lowEnergyResultDecay</returns>
    float GetEffectivenessFromEnergy(HeroManager heroManager)
    {
        if (heroManager.Hero().GetEnergy() <= (HeroSettings.maxEnergy * HeroSettings.lowEnergyThreshold))
        {
            Debug.Log("Hero energy low. Decreasing effectiveness to " + heroManager.HeroTraining().GetTempEffectiveness() * TrainingSettings.lowEnergyResultDecay);
            return heroManager.HeroTraining().GetTempEffectiveness() * TrainingSettings.lowEnergyResultDecay;
        }
        else
        {
            return heroManager.HeroTraining().GetTempEffectiveness();
        }
    }

    /// <summary>
    /// Decreases energy from the given hero
    /// </summary>
    /// <param name="heroManager">HeroManager for the hero to have energy reduced</param>
    void DecreaseEnergy(HeroManager heroManager)
    {
        float energy = heroManager.Hero().GetEnergy();
        energy -= GetEnergyCost(heroManager);

        heroManager.Hero().SetEnergy(Mathf.RoundToInt(energy));

        Debug.Log("Hero's energy: " + heroManager.Hero().GetEnergy());
    }

    /// <summary>
    /// Calculates how much energy is reduced from a given training using trainingSettings.energyDecayFromTrainingMod modifier
    /// </summary>
    /// <param name="heroManager">HeroManager for the needed hero to have energy checked</param>
    /// <returns>Energy cost calculated from the current training and TrainingSettings.energyDecayFromTrainingMod</returns>
    float GetEnergyCost(HeroManager heroManager)
    {
        return (heroManager.HeroSchedule().GetCurrentEventAsTraining().GetTrainingLevel() * TrainingSettings.energyDecayFromTrainingMod);
    }

    /// <summary>
    /// Just enables/disables the canvas group for Training Results. Move this eventually probably.
    /// </summary>
    /// <param name="toggle">True to show the Training Results canvas group.  False to hide it.</param>
    void ToggleCanvasGroup(bool toggle)
    {
        if (toggle)
        {
            canvasGroup.alpha = 1;
        } else
        {
            canvasGroup.alpha = 0;
        }
    }

    #region DEFINE LEVEL 1 BASIC TRAININGS HERE - check ScheduleMenuHandler for IDs if needed

    public TrainingScheduleEvent GetBasicStrengthTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetTrainingName(GetLevel1BasicStrengthTrainingName());

        trainingScheduleEvent.SetID(1);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.STRENGTH);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    public TrainingScheduleEvent GetBasicEnduranceTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetTrainingName(GetLevel1BasicEnduranceTrainingName());

        trainingScheduleEvent.SetID(2);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.ENDURANCE);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    public TrainingScheduleEvent GetBasicAgilityTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetTrainingName(GetLevel1BasicAgilityTrainingName());

        trainingScheduleEvent.SetID(3);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.AGILITY);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    public TrainingScheduleEvent GetBasicDexterityTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetTrainingName(GetLevel1BasicDexterityTrainingName());

        trainingScheduleEvent.SetID(4);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.DEXTERITY);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    public TrainingScheduleEvent GetBasicIntelligenceTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetTrainingName(GetLevel1BasicIntelligenceTrainingName());

        trainingScheduleEvent.SetID(5);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.INTELLIGENCE);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    public TrainingScheduleEvent GetBasicFaithTrainingScheduleEvent()
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();

        trainingScheduleEvent.SetTrainingName(GetLevel1BasicFaithTrainingName());

        trainingScheduleEvent.SetID(6);

        trainingScheduleEvent.SetTrainingType(EnumHandler.TrainingTypes.FAITH);

        trainingScheduleEvent.SetTrainingLevel(1);

        return trainingScheduleEvent;
    }

    #endregion

    #region OTHER TRAINING GENS HERE

    public TrainingScheduleEvent GetTrainingEventFromEquipmentSlot(int slot)
    {
        TrainingScheduleEvent trainingScheduleEvent = new TrainingScheduleEvent();
        TrainingEquipment trainingEquip = scheduleManager.GetHeroManager().HeroTrainingEquipment().GetTrainingEquipmentBySlot(slot);

        trainingScheduleEvent.SetTrainingName("Lv. " + trainingEquip.trainingLevel + ") " + trainingEquip.trainingName);

        trainingScheduleEvent.SetID(trainingEquip.ID);

        trainingScheduleEvent.SetTrainingLevel(trainingEquip.trainingLevel);
        trainingScheduleEvent.SetTrainingType(trainingEquip.trainingType);

        return trainingScheduleEvent;
    }

    #endregion
}

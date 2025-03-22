using NUnit.Framework;
using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroTraining : MonoBehaviour
{
    BaseTraining currentTraining;
    public void SetCurrentTraining(BaseTraining training) { currentTraining = training; }
    public BaseTraining GetCurrentTraining() { return currentTraining; }

    float trainingResult;
    public float GetTrainingResult() { return trainingResult; }

    float resultMod = 1; // lowered for each level higher the trained stat is than the player's level. Not put into effect yet.

    float strengthExp, enduranceExp, agilityExp, dexterityExp, intelligenceExp, faithExp;
    public float GetStrengthExp() { return strengthExp; }

    HeroManager heroManager;

    float tempEffectiveness;

    void Start()
    {
        heroManager = GetComponent<HeroManager>();

        // For debugging purposes.  Once scheduling is built in, this can be removed.
        BaseTraining testTraining = new BaseTraining();
        testTraining.SetTrainingType(BaseTraining.TrainingTypes.STRENGTH);
        testTraining.SetTrainingLevel(1);
        testTraining.SetEffectiveness(10f); // This will probably need a lot of tweaking

        currentTraining = testTraining;
    }

    public void ProcessTraining()
    {
        if (currentTraining == null)
        {
            Debug.LogError("No current training for hero " + heroManager.Hero().name);
        } else
        {
            CalculateTrainingResult();

            IncreaseEXP();

            DecreaseEnergy();

            ResetTraining();
        }            
    }

    private void ResetTraining()
    {
        currentTraining = null;

        // For debugging purposes.  Once scheduling is built in, this can be removed.
        BaseTraining testTraining = new BaseTraining();
        testTraining.SetTrainingType(BaseTraining.TrainingTypes.STRENGTH);
        testTraining.SetTrainingLevel(1);
        testTraining.SetEffectiveness(10f); // This will probably need a lot of tweaking

        currentTraining = testTraining;
    }

    public void CalculateTrainingResult()
    {
        SetTempEffectiveness();

        // get random value between minRandomTrainingExp and maxRandomTrainingExp. Should roughly be between 6-12?
        float randomStartingExp = Random.Range(TrainingSettings.minRandomTrainingExp, TrainingSettings.maxRandomTrainingExp);

        // take energy into account - if lower than 25% energy, effectiveness goes down
        float effectiveness = GetEffectivenessFromEnergy();

        // ---- here is where buffs from players, like items or skills or something applied to heroes can increase the exp gained by training - just tweak the effectiveness.

        // Apply effectiveness to final result.  Should aim to get between 6-13ish exp.
        trainingResult = effectiveness * resultMod; // resultMod is always 1 until we add that sytem in, so this doesn't do anything right now.
    }

    void SetTempEffectiveness()
    {
        tempEffectiveness = currentTraining.GetEffectiveness();
    }

    /// <summary>
    /// If the hero's energy is lower than 25%, the effectiveness is reduced.  This can be expanded on later.
    /// </summary>
    /// <returns>tempEffectiveness * lowEnergyResultDecay</returns>
    float GetEffectivenessFromEnergy()
    {
        if (heroManager.Hero().GetEnergy() <= (HeroSettings.maxEnergy * .25f))
        {
            Debug.Log("Hero energy low. Decreasing effectiveness");
            return tempEffectiveness * TrainingSettings.lowEnergyResultDecay;
        }
        else
        {
            return tempEffectiveness;
        }
    }

    void DecreaseEnergy()
    {
        float energy = heroManager.Hero().GetEnergy();
        energy -= currentTraining.GetTrainingLevel() * TrainingSettings.energyDecayFromTrainingMod;

        heroManager.Hero().SetEnergy(energy);

        Debug.Log("Hero's energy: " + heroManager.Hero().GetEnergy());
    }

    void IncreaseEXP()
    {
        float randomVariance = Random.Range(trainingResult - TrainingSettings.trainingExpVariance, trainingResult + TrainingSettings.trainingExpVariance);
        
        if (randomVariance <= 0)
        {
            randomVariance = TrainingSettings.minTrainingExpResult;
            Debug.LogWarning("(HeroTraining)RandomVariance is 0, exp was: " + trainingResult + " - granting hero " + TrainingSettings.minTrainingExpResult + " exp.");
        }

        trainingResult = randomVariance;

        switch (currentTraining.GetTrainingType())
        {
            case BaseTraining.TrainingTypes.STRENGTH:
                // increase strength exp
                strengthExp += trainingResult;

                // if strength exp >= GetExpRequiredForLevelUp()
                if (strengthExp >= GetExpRequiredForLevelUp(BaseTraining.TrainingTypes.STRENGTH))
                {
                    // process strength stat level up
                    StatLevelUp(BaseTraining.TrainingTypes.STRENGTH);

                    // reset strength exp to 0 (should be spill over value to go into the next level)
                    strengthExp = 0;
                }

                Debug.Log("Hero's strength EXP is: " + strengthExp + "/" + GetExpRequiredForLevelUp(BaseTraining.TrainingTypes.STRENGTH) 
                    + " and strength level is: " + heroManager.Hero().GetStrength());

                break;
            case BaseTraining.TrainingTypes.ENDURANCE:

                break;
            case BaseTraining.TrainingTypes.AGILITY:

                break;
            case BaseTraining.TrainingTypes.DEXTERITY:

                break;
            case BaseTraining.TrainingTypes.INTELLIGENCE:

                break;
            case BaseTraining.TrainingTypes.FAITH:

                break;
        }    
    }

    void StatLevelUp(BaseTraining.TrainingTypes trainingType)
    {
        switch (trainingType)
        {
            case BaseTraining.TrainingTypes.STRENGTH:
                heroManager.Hero().SetStrength(heroManager.Hero().GetStrength() + 1);
                break;
            case BaseTraining.TrainingTypes.ENDURANCE:

                break;
            case BaseTraining.TrainingTypes.AGILITY:

                break;
            case BaseTraining.TrainingTypes.DEXTERITY:

                break;
            case BaseTraining.TrainingTypes.INTELLIGENCE:

                break;
            case BaseTraining.TrainingTypes.FAITH:

                break;
        }
    }

    /// <summary>
    /// Simply just multiplies the hero's trained stat by the modifier in TrainingSettings
    /// </summary>
    /// <returns>Stat level * TrainingSettings.heroStatExpFromTrainingMod</returns>
    float GetExpRequiredForLevelUp(BaseTraining.TrainingTypes trainingType)
    {
        switch (trainingType)
        {
            case BaseTraining.TrainingTypes.STRENGTH:
                return heroManager.Hero().GetStrength() * TrainingSettings.heroStatExpFromTrainingMod;
            case BaseTraining.TrainingTypes.ENDURANCE:
                return heroManager.Hero().GetEndurance() * TrainingSettings.heroStatExpFromTrainingMod;
            case BaseTraining.TrainingTypes.AGILITY:
                return heroManager.Hero().GetAgility() * TrainingSettings.heroStatExpFromTrainingMod;
            case BaseTraining.TrainingTypes.DEXTERITY:
                return heroManager.Hero().GetDexterity() * TrainingSettings.heroStatExpFromTrainingMod;
            case BaseTraining.TrainingTypes.INTELLIGENCE:
                return heroManager.Hero().GetIntelligence() * TrainingSettings.heroStatExpFromTrainingMod;
            case BaseTraining.TrainingTypes.FAITH:
                return heroManager.Hero().GetFaith() * TrainingSettings.heroStatExpFromTrainingMod;
            default:
                return 0;
        }
    }
}

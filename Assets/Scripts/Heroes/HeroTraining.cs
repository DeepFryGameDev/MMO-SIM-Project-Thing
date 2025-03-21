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

    HeroManager heroManager;

    void Start()
    {
        heroManager = GetComponent<HeroManager>();
    }

    public void CalculateTrainingResult()
    {
        // do math
        float math = 1f;

        // get random value between new min and max trainingResultCalc

        // take energy into account - if lower than 25% energy, effectiveness goes down

        // take effectiveness into account

        // Should aim to get between 6-13ish exp.
        trainingResult = math * resultMod; // resultMod is always 1 until we add that sytem in, so this doesn't do anything right now.
    }

    void DecreaseEnergy()
    {
        float energy = heroManager.Hero().GetEnergy();
        energy -= currentTraining.GetTrainingLevel() * TrainingSettings.energyDecayFromTrainingMod;

        heroManager.Hero().SetEnergy(energy);
    }

    void IncreaseEXP()
    {
        float randomVariance = Random.Range(trainingResult - TrainingSettings.trainingExpVariance, trainingResult + TrainingSettings.trainingExpVariance);
        
        if (randomVariance <= 0)
        {
            randomVariance = TrainingSettings.minimumTrainingExp;
            Debug.LogWarning("(HeroTraining)RandomVariance is 0, exp was: " + trainingResult + " - granting hero " + TrainingSettings.minimumTrainingExp + " exp.");
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

                    // reset strength exp to 0
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

    public void ProcessTraining()
    {
        CalculateTrainingResult();

        DecreaseEnergy();
    }
}

using UnityEngine;

// Purpose: This script contains all necessary vars and functions for individual heroes for training
// Directions: 
// Other notes: 

public class HeroTraining : MonoBehaviour
{
    HeroManager heroManager;

    BaseTraining currentTraining;
    public void SetCurrentTraining(BaseTraining training) { currentTraining = training; }
    public BaseTraining GetCurrentTraining() { return currentTraining; }

    int trainingResult;
    public int GetTrainingResult() { return trainingResult; }
    public void SetTrainingResult(int trainingResult) { this.trainingResult = trainingResult; }

    int strengthExp, enduranceExp, agilityExp, dexterityExp, intelligenceExp, faithExp;
    public int GetStrengthExp() { return strengthExp; }
    public void SetStrengthExp(int exp) { strengthExp = exp; }
    public int GetEnduranceExp() { return enduranceExp; }
    public void SetEnduranceExp(int exp) { enduranceExp = exp; }
    public int GetAgilityExp() { return agilityExp; }
    public void SetAgilityExp(int exp) { agilityExp = exp; }
    public int GetDexterityExp() { return dexterityExp; }
    public void SetDexterityExp(int exp) { dexterityExp = exp; }
    public int GetIntelligenceExp() { return intelligenceExp; }
    public void SetIntelligenceExp(int exp) { intelligenceExp = exp; }
    public int GetFaithExp() { return faithExp; }
    public void SetFaithExp(int exp) { faithExp = exp; }

    int tempExp;
    public int GetTempExp() { return tempExp; }
    public void SetTempExp(int exp) { tempExp = exp; }

    int tempEnergy;
    public int GetTempEnergy() { return tempEnergy; }
    public void SetTempEnergy(int energy) { tempEnergy = energy; }

    float tempEffectiveness;
    public float GetTempEffectiveness() { return tempEffectiveness; }
    public void SetTempEffectiveness(float tempEffectiveness) { this.tempEffectiveness = tempEffectiveness; }

    TrainingResultHandler tempTRH;
    public void SetTempTRH(TrainingResultHandler trh) { tempTRH = trh; }
    public TrainingResultHandler GetTempTRH() { return tempTRH; }

    bool levelingUp; // used as a flag if the hero is leveling up during the week progression
    public bool GetLevelingUp() { return levelingUp; }
    public void SetLevelingUp(bool levelingUp) { this.levelingUp = levelingUp; }

    void Start()
    {
        heroManager = GetComponent<HeroManager>();

        // For debugging purposes.  Once scheduling is built in, this can be removed.
        BaseTraining testTraining = new BaseTraining();
        testTraining.SetTrainingType(BaseTraining.TrainingTypes.STRENGTH);
        testTraining.SetTrainingLevel(1);

        currentTraining = testTraining;

        //SetStrengthExp(240); // testing levelup
        //heroManager.Hero().SetEnergy(25); // Testing energy/effectiveness
    }

    
}

using UnityEngine;

// Purpose: This script contains all necessary vars and functions for individual heroes for training
// Directions: Assign to each hero object
// Other notes: 

public class HeroTraining : MonoBehaviour
{
    HeroManager heroManager;

    BaseTraining currentTraining; // The training that the hero is performing for the week
    public void SetCurrentTraining(BaseTraining training) { currentTraining = training; }
    public BaseTraining GetCurrentTraining() { return currentTraining; }

    int trainingResult; // The exp value that is set when training has calculated.  Exp gained is defined in TrainingManager.CalculateTrainingResult
    public int GetTrainingResult() { return trainingResult; }
    public void SetTrainingResult(int trainingResult) { this.trainingResult = trainingResult; }

    int strengthExp, enduranceExp, agilityExp, dexterityExp, intelligenceExp, faithExp; // The experience value for each hero's stats
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

    int tempExp; // Used as a temporary holder to keep track of experience before calculation is performed.  This is used in the Training Result UI to animate the fill bar.
    public int GetTempExp() { return tempExp; }
    public void SetTempExp(int exp) { tempExp = exp; }

    int tempEnergy; // Like tempExp, this is used as a temporary holder to keep track of the hero's energy before the training was performed.
    public int GetTempEnergy() { return tempEnergy; }
    public void SetTempEnergy(int energy) { tempEnergy = energy; }

    float tempEffectiveness; // Used as a holder to track the effectiveness of training for the week.  This is assigned here so that it can be manipulated and used during calculation.
    public float GetTempEffectiveness() { return tempEffectiveness; }
    public void SetTempEffectiveness(float tempEffectiveness) { this.tempEffectiveness = tempEffectiveness; }

    TrainingResultHandler tempTRH; // Used to access the Training Result Handler script attached to the training result for the week.
    public void SetTempTRH(TrainingResultHandler trh) { tempTRH = trh; }
    public TrainingResultHandler GetTempTRH() { return tempTRH; }

    bool levelingUp; // Used as a temporary flag if the hero is leveling up during the week progression
    public bool GetLevelingUp() { return levelingUp; }
    public void SetLevelingUp(bool levelingUp) { this.levelingUp = levelingUp; }

    void Start()
    {
        heroManager = GetComponent<HeroManager>();

        TestThings();
    }

    /// <summary>
    /// Uncomment and as needed - these are simply to set values for testing purposes
    /// </summary>
    void TestThings()
    {
        // For debugging purposes.  Once scheduling is built in, this can be removed.
        BaseTraining testTraining = new BaseTraining();
        testTraining.SetTrainingType(EnumHandler.TrainingTypes.STRENGTH);
        testTraining.SetTrainingLevel(1);

        currentTraining = testTraining;

        //SetStrengthExp(240); // testing levelup
        //heroManager.Hero().SetEnergy(25); // Testing energy/effectiveness
    }
}

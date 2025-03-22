using UnityEngine;

public class TrainingSettingsEditor : MonoBehaviour
{
    [Tooltip("For every stat level, exp needed to level is increased by this value.  A higher value will result in more exp needed for each stat levelup.")]
    public float heroStatExpFromTrainingMod = 50;

    [Tooltip("For every level of training exercise, energy is decayed by this amount.")]
    public float energyDecayFromTrainingMod = 15;

    [Tooltip("The exp gained from training every week will be randomly increased or decreased by this amount.")]
    [Range(1f, 50f)] public float trainingExpVariance = 5f;

    [Tooltip("The exp gained if the hero would otherwise roll a 0 during the math checks")]
    public float minTrainingExpResult = 1;

    [Tooltip("The minimum value that the random experience gained after training will start with before calculation.")]
    public float minRandomTrainingExp = 6; // will be changed eventually when we have a formula

    [Tooltip("The maximum value that the random experience gained after training will start with before calculation.")]
    public float maxRandomTrainingExp = 12; // will be changed eventually when we have a formula

    [Tooltip("When the player has low energy, effectiveness % of the training is reduced by this * the effectiveness.")]
    public float lowEnergyResultDecay = .75f;

    private void Awake()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        TrainingSettings.heroStatExpFromTrainingMod = heroStatExpFromTrainingMod;

        TrainingSettings.energyDecayFromTrainingMod = energyDecayFromTrainingMod;

        TrainingSettings.trainingExpVariance = trainingExpVariance;

        TrainingSettings.minTrainingExpResult = minTrainingExpResult;

        TrainingSettings.minRandomTrainingExp = minRandomTrainingExp;

        TrainingSettings.maxRandomTrainingExp = maxRandomTrainingExp;

        TrainingSettings.lowEnergyResultDecay = lowEnergyResultDecay;
    }
}

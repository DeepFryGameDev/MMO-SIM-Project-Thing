using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TrainingSettingsEditor : MonoBehaviour
{
    [Header("-----EXP Calculation-----")]
    [Tooltip("For every stat level, exp needed to level is increased by this value.  A higher value will result in more exp needed for each stat levelup.")]
    public float heroStatExpFromTrainingMod = 50;

    [Tooltip("The exp gained from training every week will be randomly increased or decreased by this amount.")]
    [Range(1f, 20f)] public float trainingExpVariance = 5f;

    [Tooltip("The base exp that will be received for each level of training being performed.")]
    [Range(5f, 50f)] public float trainingLevelToExpBaseMod = 12f;

    [Tooltip("The exp gained if the hero would otherwise roll a 0 during the math checks")]
    public float minTrainingExpResult = 1;   

    [Space(10)]
    [Header("-----Energy Management-----")]

    [Tooltip("For every level of training exercise, energy is decayed by this amount.")]
    public float energyDecayFromTrainingMod = 15;

    [Tooltip("When the player has low energy, effectiveness % of the training is set to this %")]
    public float lowEnergyResultDecay = .25f;

    [Space(10)]
    [Header("-----Not Yet Implemented-----")]

    [Tooltip("For every level higher a trained stat is than the player's level, the effectiveness of training will go down.  Not yet implemented.")]
    public float levelGapExpMod = 1;

    [Tooltip("Focus goes down the longer a hero continuously trains.  Not yet implemented.")]
    public float focusExpMod = 1;

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

        TrainingSettings.lowEnergyResultDecay = lowEnergyResultDecay;

        TrainingSettings.trainingLevelToExpBaseMod = trainingLevelToExpBaseMod;

        TrainingSettings.levelGapExpMod = levelGapExpMod;

        TrainingSettings.focusExpMod = focusExpMod;
    }
}

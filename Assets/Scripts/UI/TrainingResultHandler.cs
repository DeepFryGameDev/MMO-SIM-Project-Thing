using UnityEngine;
using UnityEngine.UI;

// Purpose: Holds the objects needed for manipulating training results
// Directions: Just attach to the prefab and assign all necessary fields.
// Other notes: If there are any tweaks to the Training Result prefab, adjustments may be needed here.

public class TrainingResultHandler : MonoBehaviour
{
    [Tooltip("Assign to the face graphic image")]
    [SerializeField] Image faceImage;
    public void SetFaceImage(Sprite image) { faceImage.sprite = image; }

    [Tooltip("Assign to the energy fill bar")]
    [SerializeField] Image energyFill;
    public void SetEnergyFill(float fillAmount) { energyFill.fillAmount = fillAmount; }

    [Tooltip("Assign to the exp fill bar")]
    [SerializeField] Image expFill;
    public void SetExpFill(float fillAmount) { expFill.fillAmount = fillAmount; }

    [Tooltip("Assign to the stat text label")]
    [SerializeField] TMPro.TextMeshProUGUI statText;
    public void SetStatText(string text) { statText.text = text; }

    [Tooltip("Assign to the earnedExp text label")]
    [SerializeField] TMPro.TextMeshProUGUI earnedExpText;
    public void SetExpText(string text) { earnedExpText.text = text; }

    [Tooltip("Assign to the exp fill label (on the fill bar)")]
    [SerializeField] TMPro.TextMeshProUGUI expFillText;
    public void SetExpFillText(string text) { expFillText.text = text; }

    [Tooltip("Assign to the level up text label")]
    [SerializeField] CanvasGroup levelUpLabel;
    public void ToggleLevelUpLabel(bool toggle) { if (toggle) levelUpLabel.alpha = 1; else levelUpLabel.alpha = 0; }
}

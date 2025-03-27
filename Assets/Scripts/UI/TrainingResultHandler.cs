using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingResultHandler : MonoBehaviour
{
    [SerializeField] Image faceImage;
    public void SetFaceImage(Sprite image) { faceImage.sprite = image; }

    [SerializeField] Image energyFill;
    public void SetEnergyFill(float fillAmount) { energyFill.fillAmount = fillAmount; }

    [SerializeField] Image expFill;
    public void SetExpFill(float fillAmount) { expFill.fillAmount = fillAmount; }

    [SerializeField] TMPro.TextMeshProUGUI statText;
    public void SetStatText(string text) { statText.text = text; }

    [SerializeField] TMPro.TextMeshProUGUI earnedExpText;
    public void SetExpText(string text) { earnedExpText.text = text; }

    [SerializeField] TMPro.TextMeshProUGUI expFillText;
    public void SetExpFillText(string text) { expFillText.text = text; }

    [SerializeField] CanvasGroup levelUpLabel;
    public void ToggleLevelUpLabel(bool toggle) { if (toggle) levelUpLabel.alpha = 1; else levelUpLabel.alpha = 0; }
}

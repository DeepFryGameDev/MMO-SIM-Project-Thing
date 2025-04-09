using UnityEngine;
using UnityEngine.UI;

// Purpose: Holds the objects needed for manipulating resting results
// Directions: Just attach to the prefab and assign all necessary fields.
// Other notes: If there are any tweaks to the Training Result prefab, adjustments may be needed here.

public class RestingResultHandler : MonoBehaviour
{
    [Tooltip("Assign to the face graphic image")]
    [SerializeField] Image faceImage;
    public void SetFaceImage(Sprite image) { faceImage.sprite = image; }

    [Tooltip("Assign to the energy fill bar")]
    [SerializeField] Image energyFill;
    public void SetEnergyFill(float fillAmount) { energyFill.fillAmount = fillAmount; }

    [Tooltip("Assign to the energy fill label (on the fill bar)")]
    [SerializeField] TMPro.TextMeshProUGUI energyFillText;
    public void SetEnergyFillText(string text) { energyFillText.text = text; }
}

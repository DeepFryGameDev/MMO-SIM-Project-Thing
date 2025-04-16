using UnityEngine;
using UnityEngine.UI;

// Purpose: Handles manipulating the UI elements on the Party HUD frame
// Directions: Attach to the PartyHUDFrame prefab
// Other notes: 

public class PartyHUDFrameHandler : MonoBehaviour
{
    HeroManager heroManager;

    [SerializeField] Image faceImage;
    [SerializeField] Image expBarFill;
    [SerializeField] Image hpBarFill;
    [SerializeField] Image mpBarFill;
    [SerializeField] Image energyBarFill;

    void Update()
    {
        energyBarFill.fillAmount = GetEnergy();
    }

    /// <summary>
    /// Simply returns the % of the hero's energy to be used for the fill bar
    /// </summary>
    /// <returns>Percent 0-1 of the hero's energy</returns>
    float GetEnergy()
    {
        return (float)heroManager.Hero().GetEnergy() / (float)HeroSettings.maxEnergy;
    }

    /// <summary>
    /// Sets the basic interface UI elements
    /// </summary>
    /// <param name="heroManager">Hero to have UI built from</param>
    public void SetUI(HeroManager heroManager)
    {
        this.heroManager = heroManager;

        faceImage.sprite = heroManager.GetFaceImage();

        expBarFill.fillAmount = 1; // this will be updated later and put into Update()
        hpBarFill.fillAmount = 1; // ''
        mpBarFill.fillAmount = 1; // ''
    }
}

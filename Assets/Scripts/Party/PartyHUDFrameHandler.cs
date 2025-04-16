using System;
using UnityEngine;
using UnityEngine.UI;

public class PartyHUDFrameHandler : MonoBehaviour
{
    HeroManager heroManager;

    [SerializeField] Image faceImage;
    [SerializeField] Image expBarFill;
    [SerializeField] Image hpBarFill;
    [SerializeField] Image mpBarFill;
    [SerializeField] Image energyBarFill;

    private float GetEnergy()
    {
        return (float)heroManager.Hero().GetEnergy() / (float)HeroSettings.maxEnergy;
    }

    void Update()
    {
        energyBarFill.fillAmount = GetEnergy();

        Debug.Log(heroManager.Hero().GetEnergy() + " / " + HeroSettings.maxEnergy + " = " + GetEnergy());
    }

    public void SetUI(HeroManager heroManager)
    {
        this.heroManager = heroManager;

        faceImage.sprite = heroManager.GetFaceImage();

        expBarFill.fillAmount = 1; // this will be updated later
        hpBarFill.fillAmount = 1; // ''
        mpBarFill.fillAmount = 1; // ''
    }
}

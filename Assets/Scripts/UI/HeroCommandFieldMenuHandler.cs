using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Purpose: 
// Directions: 
// Other notes: 

public class HeroCommandFieldMenuHandler : MonoBehaviour
{
    int heroID;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI classText;

    [SerializeField] Image faceImage;

    [SerializeField] Image expFill;
    [SerializeField] TextMeshProUGUI expFillText;

    [SerializeField] Image hpFill;
    [SerializeField] TextMeshProUGUI hpFillText;

    [SerializeField] Image mpFill;
    [SerializeField] TextMeshProUGUI mpFillText;

    public void SetValues(HeroManager heroManager)
    {
        heroID = heroManager.GetID();

        nameText.SetText(heroManager.Hero().GetName());

        levelText.SetText("Lv. " + heroManager.HeroClass().GetLevel().ToString());
        classText.SetText(heroManager.HeroClass().GetCurrentClass().name);

        faceImage.sprite = heroManager.GetFaceImage();

        expFill.fillAmount = 1f; // these will obviously need to be updated
        expFillText.SetText("1/1");

        hpFill.fillAmount = 1f;
        hpFillText.SetText("1/1");

        mpFill.fillAmount = 1f;
        mpFillText.SetText("1/1");
    }

    public void StatusButtonOnClick()
    {

    }

    public void OnInventoryButtonOnClick()
    {

    }

    public void OnEquipButtonOnClick()
    {

    }
}

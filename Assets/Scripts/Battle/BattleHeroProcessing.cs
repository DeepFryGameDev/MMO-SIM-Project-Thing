using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleHeroProcessing : MonoBehaviour
{
    HeroManager heroManager;

    [SerializeField] Image faceImage;
    [SerializeField] TMPro.TextMeshProUGUI hpText;
    [SerializeField] Image ATBBar;

    float currentATBVal = 0;
    public void ResetATB() { currentATBVal = 0; }

    bool ready = false;

    private void Update()
    {
        if (!ready || currentATBVal >= BattleSettings.maxATBVal) return;

        // get dex and clamp
        float dex = Mathf.Max(0f, heroManager.Hero().GetDexterity());

        // map dex -> time to fill (seconds). Lerp gives predictable linear scaling.
        float t = Mathf.Clamp01(dex / BattleSettings.maxAtbSpeed);
        float timeToFill = Mathf.Lerp(BattleSettings.maxFillTime, BattleSettings.minFillTime, t);

        // fill rate in ATB units per second
        float fillRatePerSecond = BattleSettings.maxATBVal / timeToFill;

        // advance bar
        currentATBVal += fillRatePerSecond * Time.deltaTime;
        if (currentATBVal > BattleSettings.maxATBVal) currentATBVal = BattleSettings.maxATBVal;

        UpdateBar();

        if (ATBBar.fillAmount >= 1f)
        {
            DebugManager.i.BattleDebugOut("BattleHeroProcessing", heroManager.Hero().GetName() + " is ready to act!");
            BattleManager.i.AddToHeroTurnQueue(heroManager);
        }
    }

    void UpdateBar()
    {
        ATBBar.fillAmount = currentATBVal / BattleSettings.maxATBVal;
    }

    public void SetHeroManager(HeroManager heroManager)
    {
        this.heroManager = heroManager;
        this.heroManager.SetBattleHeroProcessing(this);
    }

    public void SetValues()
    {
        faceImage.GetComponent<Image>().sprite = heroManager.GetFaceImage();
        hpText.GetComponent<TMPro.TextMeshProUGUI>().text = heroManager.Hero().GetCurrentHP() + " / " + heroManager.Hero().GetMaxHP();

        ATBBar.fillAmount = GetATBStartingVal() * 0.01f;
        currentATBVal = ATBBar.fillAmount * BattleSettings.maxATBVal;

        DebugManager.i.BattleDebugOut("BattleHeroProcessing", "Starting " + heroManager.Hero().GetName() + "'s ATB at: " + currentATBVal);
        ready = true;
    }

    float GetATBStartingVal()
    {
        float rand = UnityEngine.Random.Range(BattleSettings.minATBStartingVal, BattleSettings.maxATBStartingVal);
        //Debug.Log("Random ATB Starting Value for " + heroManager.Hero().GetName() + ": " + rand);
        return rand;
    }
}

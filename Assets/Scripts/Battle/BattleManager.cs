using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Transform battleHUDGroupParent;
    CanvasGroup battleHUDGroup;

    private void Awake()
    {
        battleHUDGroupParent = GameObject.Find("BattleHUDHeroGroup").transform;
        battleHUDGroup = battleHUDGroupParent.GetComponentInParent<CanvasGroup>();

        InstantiateHeroesInUI();

        SetupUI();        
    }

    void SetupUI()
    {
        battleHUDGroup.alpha = 1;
    }

    void InstantiateHeroesInUI()
    {
        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            GameObject heroFrame = Instantiate(PrefabManager.i.BattleHUDHeroFrame, battleHUDGroupParent);
            BattleHeroProcessing heroProcessing = heroFrame.GetComponent<BattleHeroProcessing>();

            heroProcessing.SetHeroManager(heroManager);
            heroProcessing.SetValues();

            //Debug.Log("Instantiated hero frame for " + heroManager.Hero().GetName());
        }
    }
}

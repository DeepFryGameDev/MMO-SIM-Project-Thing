using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIHandler : MonoBehaviour
{
    [SerializeField] CanvasGroup heroActionPanelCanvasGroup;
    [SerializeField] CanvasGroup battleEnemyListCanvasGroup;
    [SerializeField] Transform battleEnemyList;

    [SerializeField] Image heroFacePanel;

    public static BattleUIHandler i;

    private void Awake()
    {
        Debug.Log("Setting i");
        i = this;
    }

    public void SetHeroFacePanel(Sprite faceImage)
    {
        if (heroFacePanel == null) { heroFacePanel = transform.Find("HeroActionPanel/HeroFaceBackground/HeroFacePanel").GetComponent<Image>(); }
        heroFacePanel.sprite = faceImage;
    }

    public void ToggleHeroActionPanel(bool toggle)
    {
        if (toggle) { heroActionPanelCanvasGroup.alpha = 1; } else { heroActionPanelCanvasGroup.alpha = 0; }

        heroActionPanelCanvasGroup.blocksRaycasts = toggle;
        heroActionPanelCanvasGroup.interactable = toggle;
    }

    public void ToggleEnemyListPanel(bool toggle)
    {
        if (toggle) { battleEnemyListCanvasGroup.alpha = 1; } else { battleEnemyListCanvasGroup.alpha = 0; }

        battleEnemyListCanvasGroup.blocksRaycasts = toggle;
        battleEnemyListCanvasGroup.interactable = toggle;
    }

    public void Setup()
    {
        // for each enemy in BattleData.i.GetBaseBattle().GetEnemies(), instantiate buttons to BattleEnemyListTransform and set their EnemyListButtonHandler enemy.
        foreach (BaseEnemy enemy in BattleManager.i.GetEnemyList())
        {
            Debug.Log("BattleUIHandler Setup - Instantiating enemy: " + enemy.GetEnemyData().GetName());
            GameObject newEnemyListButton = Instantiate(PrefabManager.i.BattleEnemyListButton);

            // ---------------- SENSITIVE LINE BELOW ----------------
            newEnemyListButton.transform.SetParent(GameObject.Find("BattleCanvas/BattleEnemyList/BattleEnemyListGroup").transform, false);
            // FOR WHATEVER REASON, THIS WORKS. I DONT KNOW WHY. UNCOMMENT WHEN FIGURED OUT. I tried a million things but for whatever reason it will either just not instantiate or it won't set the parent.
            // It doesn't like using the serialized field as a transform or a gameobject.  It will only take a manual GameObject.Find (and only on this line - I can't even set it to a transform variable. WEIRD.    

            newEnemyListButton.GetComponent<EnemyListButtonHandler>().SetEnemy(enemy);
            newEnemyListButton.GetComponentInChildren<TextMeshProUGUI>().SetText(enemy.GetEnemyData().GetName());
        }
    }
}

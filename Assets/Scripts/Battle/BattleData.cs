using UnityEngine;

public class BattleData : MonoBehaviour
{
    public static BattleData i;

    BaseBattle baseBattle;
    public BaseBattle GetBaseBattle() { return baseBattle; }
    public void SetBaseBattle(BaseBattle battle) { baseBattle = battle; }

    void Awake()
    {
        i = this;
        DontDestroyOnLoad(gameObject);
    }
}

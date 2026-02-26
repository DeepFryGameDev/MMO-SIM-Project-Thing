using UnityEngine;

public class BattleEnemyProcessing : MonoBehaviour
{
    [Tooltip("The speed at which the enemy's ATB gauge fills up. Higher value means faster turns.")]
    [Range(.1f, 99f)] [SerializeField] protected float atbSpeed;

    BaseEnemy enemy;

    // increase hidden ATB in here.
    // also track how many turns the enemy has taken, etc.
    // handle enemy status effects and stats here as well

    float currentATBVal = 0;

    // new tuning parameters
    float minFillTime = 2.5f;   // fastest time (seconds) to go from 0 -> full at max fill speed
    float maxFillTime = 10f;  // slowest time (seconds) to go from 0 -> full at min fill speed
    float maxAtbSpeed = 99f;       // ATB Speed value that maps to fastest time

    bool ready = false;
    protected bool turnReady = false;

    private void Awake()
    {
        SetValues();
    }

    protected virtual void Update()
    {
        if (!ready || currentATBVal >= BattleSettings.maxATBVal) return;
                
        ProcessATB();

        if (currentATBVal >= BattleSettings.maxATBVal) // Enemy is ready to perform an action
        {
            DebugManager.i.BattleDebugOut("BattleHeroProcessing", enemy.GetEnemyData().GetName() + " is ready to act!");
            BattleManager.i.AddToUnitTurnQueue(enemy);
            Debug.Log("Queue size: " + BattleManager.i.GetUnitTurnQueue().Count);
            turnReady = true;
        }
    }

    protected void EnqueueBasicAttack()
    {
        // get random hero target for now
        int rand = Random.Range(0, GameSettings.GetHeroesInParty().Count);
        BaseHero heroToAttack = GameSettings.GetHeroesInParty()[rand].Hero();
        BaseAction basicAttack = new BaseAction(enemy, heroToAttack, EnumHandler.battleActionTypes.BASICATTACK);

        BattleManager.i.AddToActionQueue(basicAttack);

        DebugManager.i.BattleDebugOut("BattleEnemyProcessing", enemy.GetEnemyData().GetName() + " is enqueuing a basic attack against " + heroToAttack.GetName());
        turnReady = false;
    }

    private void ProcessATB()
    {
        // get atbSpeed and clamp
        float tempAtbSpeed = Mathf.Max(0f, atbSpeed);

        // map tempAtbSpeed -> time to fill (seconds). Lerp gives predictable linear scaling.
        float t = Mathf.Clamp01(tempAtbSpeed / maxAtbSpeed);
        float timeToFill = Mathf.Lerp(maxFillTime, minFillTime, t);

        // fill rate in ATB units per second
        float fillRatePerSecond = BattleSettings.maxATBVal / timeToFill;

        // advance bar
        currentATBVal += fillRatePerSecond * Time.deltaTime;
        if (currentATBVal > BattleSettings.maxATBVal) currentATBVal = BattleSettings.maxATBVal;

        //Debug.Log("currentATBVal: " + currentATBVal);
    }

    public void SetValues()
    {
        enemy = GetComponent<BaseEnemy>();

        currentATBVal = GetATBStartingVal();
        DebugManager.i.BattleDebugOut("BattleEnemyProcessing", "Starting " + enemy.GetEnemyData().GetName() + "'s ATB at: " + currentATBVal);
        ready = true;
    }

    float GetATBStartingVal()
    {
        float rand = UnityEngine.Random.Range(BattleSettings.minATBStartingVal, BattleSettings.maxATBStartingVal);
        //Debug.Log("Random ATB Starting Value for " + heroManager.Hero().GetName() + ": " + rand);
        return rand;
    }
}

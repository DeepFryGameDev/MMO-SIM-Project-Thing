using UnityEngine;

public class BattleEnemyProcessing : MonoBehaviour
{
    [Tooltip("The speed at which the enemy's ATB gauge fills up. Higher value means faster turns.")]
    [Range(.1f, 99f)] [SerializeField] protected float atbSpeed;

    BaseEnemy enemy;

    // increase hidden ATB in here.
    // also track how many turns the enemy has taken, etc.
    // handle enemy status effects and stats here as well

    int minATBStartingVal = 5;
    int maxATBStartingVal = 40;
    float maxATBVal = 100f;
    float currentATBVal = 0;

    // new tuning parameters
    float minFillTime = 3f;   // fastest time (seconds) to go from 0 -> full at max fill speed
    float maxFillTime = 10f;  // slowest time (seconds) to go from 0 -> full at min fill speed
    float maxAtbSpeed = 99f;       // ATB Speed value that maps to fastest time

    bool ready = false;

    private void Awake()
    {
        SetValues();
    }

    private void Update()
    {
        if (!ready || currentATBVal >= maxATBVal) return;

        // get atbSpeed and clamp
        float tempAtbSpeed = Mathf.Max(0f, atbSpeed);

        // map tempAtbSpeed -> time to fill (seconds). Lerp gives predictable linear scaling.
        float t = Mathf.Clamp01(tempAtbSpeed / maxAtbSpeed);
        float timeToFill = Mathf.Lerp(maxFillTime, minFillTime, t);

        // fill rate in ATB units per second
        float fillRatePerSecond = maxATBVal / timeToFill;

        // advance bar
        currentATBVal += fillRatePerSecond * Time.deltaTime;
        if (currentATBVal > maxATBVal) currentATBVal = maxATBVal;

        //Debug.Log(enemy.GetEnemyData().GetName() + "'s ATB: " + currentATBVal);

        if (currentATBVal >= maxATBVal) DebugManager.i.BattleDebugOut("BattleHeroProcessing", enemy.GetEnemyData().GetName() + " is ready to act!");
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
        float rand = UnityEngine.Random.Range(minATBStartingVal, maxATBStartingVal);
        //Debug.Log("Random ATB Starting Value for " + heroManager.Hero().GetName() + ": " + rand);
        return rand;
    }
}

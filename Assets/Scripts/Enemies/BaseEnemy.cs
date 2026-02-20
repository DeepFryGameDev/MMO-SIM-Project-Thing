using UnityEngine;

public class BaseEnemy : BaseAttackableUnit
{
    [SerializeField] protected EnemyScriptableObject enemyData;
    EnemyAnimHandler animHandler;

    private void Awake()
    {
        // Set all variables based on the enemy data scriptable object
        name = enemyData.GetName();
        currentHP = enemyData.GetBaseHP();
        baseStrength = enemyData.GetBaseStrength();
        baseEndurance = enemyData.GetBaseEndurance();
        baseAgility = enemyData.GetBaseAgility();
        baseDexterity = enemyData.GetBaseDexterity();
        baseIntelligence = enemyData.GetBaseIntelligence();
        baseFaith = enemyData.GetBaseFaith();

        armor = enemyData.GetBaseArmor();
        magicResist = enemyData.GetBaseMagicResist();

        animHandler = GetComponent<EnemyAnimHandler>();
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/New Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] new string name;
    public string GetName() { return name; }

    [SerializeField] int baseHP;
    public int GetBaseHP() { return baseHP; }

    [SerializeField] int baseArmor;
    public int GetBaseArmor() { return baseArmor; }

    [SerializeField] int baseMagicResist;
    public int GetBaseMagicResist() { return baseMagicResist; }

    [SerializeField] int baseStrength;
    public int GetBaseStrength() { return baseStrength; }

    [SerializeField] int baseEndurance;
    public int GetBaseEndurance() { return baseEndurance; }

    [SerializeField] int baseAgiliy;
    public int GetBaseAgility() { return baseAgiliy; }

    [SerializeField] int baseDexterity;
    public int GetBaseDexterity() { return baseDexterity; }

    [SerializeField] int baseIntelligence;
    public int GetBaseIntelligence() { return baseIntelligence; }

    [SerializeField] int baseFaith;    
    public int GetBaseFaith() { return baseFaith; }

    [SerializeField] int baseAttackDamage;
    public int GetBaseAttackDamage() { return baseAttackDamage; }

    [SerializeField] GameObject enemyPrefab;
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
}

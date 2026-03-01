using UnityEngine;

public static class DamageCalc
{
    static float heroAttackStrengthModifier = 4.5f;
    static float enemyAttackStrengthModifier = 4.5f;

    static float heroArmorDefenseModifier = 1.25f;
    static float enemyArmorDefenseModifier = 1.25f;

    static int damageVariance = 5;


    public static int GetHeroToEnemyBasicAttackDamage(HeroManager sourceHero, BaseEnemy targetEnemy)
    {
        // get hero weapon damage
        int weaponDamage = sourceHero.GetWeaponDamage();

        //Debug.Log("Weapon damage: " + weaponDamage);

        // get hero strength * heroAttackStrengthModifier
        int heroStrength = sourceHero.Hero().GetStrength();
        int attackStrength = Mathf.RoundToInt(heroStrength * heroAttackStrengthModifier);

        //Debug.Log("Hero strength: " + heroStrength + " * heroAttackStrengthModifier: " + heroAttackStrengthModifier + " = " + attackStrength);

        // get enemy armor * enemyArmorDefenseModifier
        int enemyArmor = targetEnemy.GetArmor();
        int enemyDefense = Mathf.RoundToInt(enemyArmor * enemyArmorDefenseModifier);

        //Debug.Log("Enemy armor: " + enemyArmor + " * enemyArmorDefenseModifier: " + enemyArmorDefenseModifier + " = " + enemyDefense);

        // get the top part - the bottom part
        int damage = attackStrength - enemyDefense;
        //Debug.Log("Damage: " + damage);

        // add some variance
        int finalDamage = Random.Range((damage - damageVariance), (damage + damageVariance));

        // return that result
        if (finalDamage < 0) finalDamage = 0;
        //Debug.Log("Final damage: " + finalDamage);

        return finalDamage;
    }

    public static int GetEnemyToHeroBasicAttackDamage(BaseEnemy sourceEnemy, HeroManager targetHero)
    {
        // get enemy attack damage
        int attackDamage = sourceEnemy.GetEnemyData().GetBaseAttackDamage();

        // get enemy strength * enemyAttackStrengthModifier
        int enemyStrength = sourceEnemy.GetEnemyData().GetBaseStrength();
        int attackStrength = Mathf.RoundToInt(enemyStrength * enemyAttackStrengthModifier);

        //Debug.Log("Enemy strength: " + enemyStrength + " * enemyAttackStrengthModifier: " + enemyAttackStrengthModifier + " = " + attackStrength);

        // get hero armor * heroArmorDefenseModifier
        int heroArmor = targetHero.GetArmor();
        int heroDefense = Mathf.RoundToInt(heroArmor * heroArmorDefenseModifier);

        //Debug.Log("Hero Armor: " + heroArmor + " * heroArmorDefenseModifier: " + heroArmorDefenseModifier + " = " + heroDefense);

        // get the top part - the bottom part
        int damage = attackStrength - heroDefense;
        //Debug.Log("Damage: " + damage);

        // add some variance
        int finalDamage = Random.Range((damage - damageVariance), (damage + damageVariance));

        // return that result
        if (finalDamage < 0) finalDamage = 0;

        //Debug.Log("Final damage: " + finalDamage);

        return finalDamage;
    }
}

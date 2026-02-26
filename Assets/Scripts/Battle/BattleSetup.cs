using Unity.VisualScripting;
using UnityEngine;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] Transform inactiveHeroesTransform;

    [SerializeField] Transform heroSpawnPointC, heroSpawnPointIL, heroSpawnPointOL, heroSpawnPointIR, heroSpawnPointOR;
    [SerializeField] Transform quadHeroSpawnPointIL, quadHeroSpawnPointOL, quadHeroSpawnPointIR, quadHeroSpawnPointOR;
    [SerializeField] Transform enemySpawnPointC, enemySpawnPointIL, enemySpawnPointOL, enemySpawnPointIR, enemySpawnPointOR;

    [SerializeField] Transform heroSpawnPointsParent, enemySpawnPointsParent, enemiesParent;

    [SerializeField] GameObject mainCamera, battleCamera;

    private void Awake()
    {
        Debug.Log("Lets get some battle stuff setup");

        SpawnActiveHeroes();

        SpawnInactiveHeroes();

        SpawnEnemies();

        // Switch cameras
        SetupCameras();

        Debug.Log("-------------------");
    }

    private void SetupCameras()
    {
        mainCamera.SetActive(false);
        battleCamera.SetActive(true);
    }

    void PrepHeroForBattle(HeroManager heroManager)
    {
        heroManager.HeroPathing().StopPathing();
        heroManager.HeroPathing().ToggleNavMeshAgent(false);
        heroManager.AnimHandler().SetMovementToIdle();
        heroManager.AnimHandler().SetAnimationState(EnumHandler.heroAnimationStates.BATTLE);
        heroManager.AnimHandler().SetBattleAnimationState(EnumHandler.heroBattleAnimationStates.IDLE);
    }

    void SpawnActiveHeroes()
    {
        Debug.Log("Heroes to spawn:");
        int partySize = GameSettings.GetHeroesInParty().Count;

        switch (partySize)
        {
            case 1:
                // spawn on HeroSpawnPoint C
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[0].Hero().GetName() + " on HeroSpawnPoint C");

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[0]);
                GameSettings.GetHeroesInParty()[0].gameObject.transform.position = heroSpawnPointC.position;
                break;
            case 2:
                // spawn on HeroSpawnPoint IL and IR
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[0].Hero().GetName() + " on HeroSpawnPoint IL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[1].Hero().GetName() + " on HeroSpawnPoint IR");

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[0]);
                GameSettings.GetHeroesInParty()[0].gameObject.transform.position = heroSpawnPointIL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[1]);
                GameSettings.GetHeroesInParty()[1].gameObject.transform.position = heroSpawnPointIR.position;
                break;
            case 3:
                // spawn on HeroSpawnPoint IL, C, and IR
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[0].Hero().GetName() + " on HeroSpawnPoint IL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[1].Hero().GetName() + " on HeroSpawnPoint C");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[2].Hero().GetName() + " on HeroSpawnPoint IR");

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[0]);
                GameSettings.GetHeroesInParty()[0].gameObject.transform.position = heroSpawnPointC.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[1]);
                GameSettings.GetHeroesInParty()[1].gameObject.transform.position = heroSpawnPointIL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[2]);
                GameSettings.GetHeroesInParty()[2].gameObject.transform.position = heroSpawnPointIR.position;

                break;
            case 4:
                // spawn on HeroSpawnPoint OL, IL, IR, and OR
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[0].Hero().GetName() + " on QuadHeroSpawnPoint OL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[1].Hero().GetName() + " on QuadHeroSpawnPoint IL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[2].Hero().GetName() + " on QuadHeroSpawnPoint IR");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[3].Hero().GetName() + " on QuadHeroSpawnPoint OR");

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[0]);
                GameSettings.GetHeroesInParty()[0].gameObject.transform.position = quadHeroSpawnPointOL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[1]);
                GameSettings.GetHeroesInParty()[1].gameObject.transform.position = quadHeroSpawnPointIL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[2]);
                GameSettings.GetHeroesInParty()[2].gameObject.transform.position = quadHeroSpawnPointIR.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[3]);
                GameSettings.GetHeroesInParty()[3].gameObject.transform.position = quadHeroSpawnPointOR.position;
                break;
            case 5:
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[0].Hero().GetName() + " on HeroSpawnPoint C");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[1].Hero().GetName() + " on HeroSpawnPoint IL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[2].Hero().GetName() + " on HeroSpawnPoint OL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[3].Hero().GetName() + " on HeroSpawnPoint IR");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[4].Hero().GetName() + " on HeroSpawnPoint OR");

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[0]);
                GameSettings.GetHeroesInParty()[0].gameObject.transform.position = heroSpawnPointC.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[1]);
                GameSettings.GetHeroesInParty()[1].gameObject.transform.position = heroSpawnPointIL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[2]);
                GameSettings.GetHeroesInParty()[2].gameObject.transform.position = heroSpawnPointIR.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[3]);
                GameSettings.GetHeroesInParty()[3].gameObject.transform.position = heroSpawnPointOL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[4]);
                GameSettings.GetHeroesInParty()[4].gameObject.transform.position = heroSpawnPointOR.position;
                break;
            default:
                Debug.Log("No heroes in party");
                break;
        }

        foreach (HeroManager heroManager in GameSettings.GetHeroesInParty())
        {
            heroManager.transform.LookAt(enemySpawnPointsParent.transform.position);
        }
    }

    void SpawnInactiveHeroes()
    {
        foreach (HeroManager heroManager in GameSettings.GetIdleHeroes())
        {
            DebugManager.i.BattleDebugOut("BattleSetup", "Hiding idle hero " + heroManager.Hero().GetName() + " in the battle scene");
            heroManager.gameObject.transform.SetParent(inactiveHeroesTransform);
        }
    }

    void SpawnEnemies()
    {
        foreach (EnemyScriptableObject enemy in BattleData.i.GetBaseBattle().GetEnemies())
        {
            Debug.Log("Spawning enemy " + enemy.GetName());
            GameObject newEnemy = Instantiate(enemy.GetEnemyPrefab(), enemySpawnPointC.position, Quaternion.identity);
            newEnemy.transform.SetParent(enemiesParent);

            newEnemy.gameObject.name = enemy.GetName();
            newEnemy.transform.LookAt(heroSpawnPointsParent.position);
        }
    }
}

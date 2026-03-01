using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] Transform inactiveHeroesTransform;

    [SerializeField] Transform heroSpawnPointC, heroSpawnPointIL, heroSpawnPointOL, heroSpawnPointIR, heroSpawnPointOR;
    [SerializeField] Transform quadHeroSpawnPointIL, quadHeroSpawnPointOL, quadHeroSpawnPointIR, quadHeroSpawnPointOR;
    [SerializeField] Transform enemySpawnPointC, enemySpawnPointIL, enemySpawnPointOL, enemySpawnPointIR, enemySpawnPointOR;
    [SerializeField] Transform quadEnemySpawnPointIL, quadEnemySpawnPointOL, quadEnemySpawnPointIR, quadEnemySpawnPointOR;

    [SerializeField] Transform heroSpawnPointsParent, enemySpawnPointsParent, enemiesParent;

    [SerializeField] GameObject mainCamera, battleCamera;

    Transform battleHUDGroupParent;
    CanvasGroup battleHUDGroup;

    List<int> tempDuplicateEnemyIDs = new List<int>();
    List<int> duplicateEnemyIDs = new List<int>();

    int enemyNamingModIndex = 0;
    char[] enemyNamingMod;

    private void Awake()
    {
        SetupVars();

        SpawnActiveHeroes();

        SpawnInactiveHeroes();

        SpawnEnemies();

        // Switch cameras
        SetupCameras();

        SetupUI();

        InstantiateHeroesInUI();

        Debug.Log("-------------------");
    }

    private void SetupVars()
    {
        enemyNamingMod = BattleSettings.enemyDupeNamingConvention.ToCharArray();
    }

    private void SetupCameras()
    {
        mainCamera.SetActive(false);
        battleCamera.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[0].Hero().GetName() + " on HeroSpawnPoint C");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[1].Hero().GetName() + " on HeroSpawnPoint OL");
                DebugManager.i.BattleDebugOut("BattleSetup", "Spawning " + GameSettings.GetHeroesInParty()[2].Hero().GetName() + " on HeroSpawnPoint OR");

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[0]);
                GameSettings.GetHeroesInParty()[0].gameObject.transform.position = heroSpawnPointC.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[1]);
                GameSettings.GetHeroesInParty()[1].gameObject.transform.position = heroSpawnPointOL.position;

                PrepHeroForBattle(GameSettings.GetHeroesInParty()[2]);
                GameSettings.GetHeroesInParty()[2].gameObject.transform.position = heroSpawnPointOR.position;

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

            SetBattleMovementParams(heroManager);
        }
    }

    void SetBattleMovementParams(HeroManager heroManager)
    {
        heroManager.gameObject.AddComponent<BattleUnitMovement>();
        heroManager.HeroPathing().ToggleNavMeshAgent(true);
        heroManager.HeroPathing().SetInBattle(true);
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
        //SetEnemyNamesAndPrepInstantiation();

        if (BattleManager.i == null) { BattleManager.i = FindFirstObjectByType<BattleManager>(); }

        switch (BattleData.i.GetBaseBattle().GetEnemies().Count)
        {
            case 1:
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[0], enemySpawnPointC);
                break;
            case 2:
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[0], enemySpawnPointIL);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[1], enemySpawnPointIR);
                break;
            case 3:
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[0], enemySpawnPointC);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[1], enemySpawnPointOL);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[2], enemySpawnPointOR);
                break;
            case 4:
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[0], quadEnemySpawnPointOL);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[1], quadEnemySpawnPointIL);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[2], quadEnemySpawnPointIR);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[3], quadEnemySpawnPointOR);
                break;
            case 5:
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[0], enemySpawnPointC);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[1], enemySpawnPointIL);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[2], enemySpawnPointOL);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[3], enemySpawnPointIR);
                InstantiateEnemyToBattle(BattleData.i.GetBaseBattle().GetEnemies()[4], enemySpawnPointOR);
                break;
        }

        SetEnemyNames();
    }

    void SetEnemyNames()
    {
        // check if there are duplicates of any IDs in BattleManager.i.GetActiveEnemies().  Set their name appropriately
        foreach (BaseEnemy enemy in BattleManager.i.GetActiveEnemies())
        {
            if (!tempDuplicateEnemyIDs.Contains(enemy.GetEnemyData().GetID()))
            {
                tempDuplicateEnemyIDs.Add(enemy.GetEnemyData().GetID());
            }
            else if (!duplicateEnemyIDs.Contains(enemy.GetEnemyData().GetID()))
            {
                duplicateEnemyIDs.Add(enemy.GetEnemyData().GetID());
                DebugManager.i.BattleDebugOut("BattleSetup", "Enemies with duplicate IDs found - " + enemy.GetEnemyData().GetName() + " / ID: " + enemy.GetEnemyData().GetID());
            }
        }

        tempDuplicateEnemyIDs.Clear(); // reusing to ensure duplicate enemies are renamed individually by enemy ID

        foreach (int ID in duplicateEnemyIDs) // name duplicate enemies with naming mod
        {
            int counter = 0;
            foreach (BaseEnemy enemy in BattleManager.i.GetActiveEnemies())
            {
                if (enemy.GetEnemyData().GetID() == ID && !tempDuplicateEnemyIDs.Contains(enemy.GetEnemyData().GetID()))
                {
                    BaseEnemy newEnemy = BattleManager.i.GetActiveEnemies()[counter];

                    char tempChar = enemyNamingMod[enemyNamingModIndex];

                    String tempName = newEnemy.GetEnemyData().GetName() + " " + tempChar;

                    Debug.Log("Setting name to " + tempName);

                    Debug.Log("Setting naming mod " + tempChar + " to " + newEnemy.GetEnemyData().GetName());

                    newEnemy.SetName(tempName);
                    newEnemy.gameObject.name = tempName;

                    enemyNamingModIndex++;
                }

                counter++;
            }

            enemyNamingModIndex = 0;
            tempDuplicateEnemyIDs.Add(ID);
        }

        foreach (BaseEnemy enemy in BattleManager.i.GetActiveEnemies()) // name them normally
        {
            int counter = 0;

            if (!duplicateEnemyIDs.Contains(enemy.GetEnemyData().GetID())) // ignore those we already did above that are duplicates
            {
                BaseEnemy newEnemy = BattleManager.i.GetActiveEnemies()[counter];

                newEnemy.SetName(enemy.GetEnemyData().GetName());
                newEnemy.gameObject.name = enemy.GetEnemyData().GetName();
            }
            counter++;
        }
    }

    private void InstantiateEnemyToBattle(BaseEnemy enemy, Transform position)
    {
        Debug.Log("Spawning enemy " + enemy.GetName());
        GameObject newEnemy = Instantiate(enemy.GetEnemyData().GetEnemyPrefab(), position.position, Quaternion.identity);
        newEnemy.transform.SetParent(enemiesParent);

        //newEnemy.gameObject.name = enemy.GetName();     

        newEnemy.transform.LookAt(heroSpawnPointsParent.position);
        newEnemy.GetComponent<BattleUnitMovement>().SetOriginRotation();

        BattleManager.i.AddToActiveEnemies(newEnemy.GetComponent<BaseEnemy>());
    }

    void SetupUI()
    {
        battleHUDGroupParent = GameObject.Find("BattleHUDHeroGroup").transform;
        battleHUDGroup = battleHUDGroupParent.GetComponentInParent<CanvasGroup>();

        battleHUDGroup.alpha = 1;

        BattleUIHandler.i.Setup();
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

using System;
using UnityEngine;

// Purpose: Links all associated scripts together for each hero - this makes referencing any heroes scripts far easier and should reduce the spaghetti.
// Directions: Attach to each hero
// Other notes: 

public class HeroManager : MonoBehaviour
{
    [SerializeField] int ID;
    public int GetID() { return ID; }

    [SerializeField] Sprite faceImage;
    public Sprite GetFaceImage() { return faceImage; }
    
    // Scripts attached to the hero - each of these should be set in the Setup() method

    BaseHero hero;
    public BaseHero Hero() { return hero; }

    HeroPathing heroPathing;
    public HeroPathing HeroPathing() { return heroPathing; }

    HeroInteraction heroInteraction;
    public HeroInteraction HeroInteraction() { return heroInteraction; }

    HeroTraining heroTraining;
    public HeroTraining HeroTraining() { return heroTraining; }

    HeroTrainingEquipment heroTrainingEquipment;
    public HeroTrainingEquipment HeroTrainingEquipment() { return heroTrainingEquipment; }

    HeroInventory heroInventory;
    public HeroInventory HeroInventory() { return heroInventory; }

    HeroEquipment heroEquipment;
    public HeroEquipment HeroEquipment() { return heroEquipment; }

    HeroSchedule heroSchedule;
    public HeroSchedule HeroSchedule() { return heroSchedule; }

    HeroParty heroParty;
    public HeroParty HeroParty() { return heroParty; }

    HeroClass heroClass;
    public HeroClass HeroClass() { return heroClass; }

    new Collider collider;
    public Collider Collider() { return collider; }

    HeroAnimHandler animHandler;
    public HeroAnimHandler AnimHandler() { return animHandler; }

    BattleHeroProcessing battleHeroProcessing;
    public BattleHeroProcessing BattleHeroProcessing() { return battleHeroProcessing; }
    public void SetBattleHeroProcessing(BattleHeroProcessing proc) { battleHeroProcessing = proc; }

    // Scripts attached elsewhere - these will need a public setter so they can be set externally.
    // These should be set in Awake() (in the other script) so that this script can use Start()/CheckForNulls() to detect any issues.
    HeroHomeZone homeZone;
    public HeroHomeZone HomeZone() { return homeZone; }
    public void SetHomeZone(HeroHomeZone homeZone) { this.homeZone = homeZone; }

    Vector3 startingPosition;
    public Vector3 GetStartingPosition() { return startingPosition; }

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        CheckForNulls();

        startingPosition = transform.position;
    }

    public void Setup() 
    {
        hero = GetComponent<BaseHero>();

        heroPathing = GetComponent<HeroPathing>();
        heroInteraction = GetComponent<HeroInteraction>();

        heroClass = GetComponent<HeroClass>();

        heroTraining = GetComponent<HeroTraining>();
        heroTrainingEquipment = GetComponent<HeroTrainingEquipment>();

        heroInventory = GetComponent<HeroInventory>();
        heroEquipment = GetComponent<HeroEquipment>();

        heroParty = GetComponent<HeroParty>();

        heroSchedule = GetComponent<HeroSchedule>();

        collider = GetComponent<Collider>();

        animHandler = GetComponent<HeroAnimHandler>();

        gameObject.name = "[" + ID + "] " + hero.GetName();
    }

    public int GetArmor()
    {
        int baseArmor = hero.GetArmor();

        int armorFromEquipment = (GetRawArmorFromEquipment(heroEquipment.GetEquippedHead()) + GetRawArmorFromEquipment(heroEquipment.GetEquippedChest()) + GetRawArmorFromEquipment(heroEquipment.GetEquippedHands()) + 
            GetRawArmorFromEquipment(heroEquipment.GetEquippedLegs()) + GetRawArmorFromEquipment(heroEquipment.GetEquippedFeet()));

        return baseArmor + armorFromEquipment;
    }

    public int GetWeaponDamage()
    {
        return GetRawWeaponDamageFromEquipment(heroEquipment.GetEquippedMainHand());
    }

    int GetRawWeaponDamageFromEquipment(WeaponEquipment weaponEquip)
    {
        if (weaponEquip == null) { return 1; } else { return weaponEquip.attackDamage; }
    }

    int GetRawArmorFromEquipment(ArmorEquipment armorEquip)
    {
        if (armorEquip == null)
        {
            return 0;
        } else
        {
            return armorEquip.baseArmorValue;
        }
    }

    void CheckForNulls()
    {
        if (hero == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "Hero null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroPathing == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroPathing null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroInteraction == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroInteraction null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroTraining == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroTraining null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroTrainingEquipment == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroTrainingEquipment null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroSchedule == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroSchedule null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroInventory == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroInventory null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroEquipment == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroEquipment null on HeroManager: " + gameObject.name, false, true);
        }

        if (heroParty == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "heroParty null on HeroManager: " + gameObject.name, false, true);
        }

        if (collider == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "collider null on HeroManager: " + gameObject.name, false, true);
        }

        if (animHandler == null)
        {
            DebugManager.i.SystemDebugOut("HeroManager", "animHandler null on HeroManager: " + gameObject.name, false, true);
        }
    }
}

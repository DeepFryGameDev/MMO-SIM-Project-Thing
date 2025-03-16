using UnityEngine;
using UnityEngine.AI;

// Purpose: Links all associated scripts together for each hero - this makes referencing any heroes scripts far easier and should reduce the spaghetti.
// Directions: Attach to each hero
// Other notes: 

public class HeroManager : MonoBehaviour
{
    // Scripts attached to the hero - each of these should be set in the Setup() method

    BaseHero hero;
    public BaseHero Hero() { return hero; }

    NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent() { return navMeshAgent; }

    HeroPathing heroPathing;
    public HeroPathing HeroPathing() { return heroPathing; }

    HeroInteraction heroInteraction;
    public HeroInteraction HeroInteraction() { return heroInteraction; }

    new Collider collider;
    public Collider Collider() { return collider; }

    HeroAnimHandler animHandler;
    public HeroAnimHandler AnimHandler() { return animHandler; }

    // Scripts attached elsewhere - these will need a public setter so they can be set externally.
    // These should be set in Awake() (in the other script) so that this script can use Start()/CheckForNulls() to detect any issues.
    HeroHomeZone homeZone;
    public HeroHomeZone HomeZone() { return homeZone; }
    public void SetHomeZone(HeroHomeZone homeZone) { this.homeZone = homeZone; }

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        CheckForNulls();
    }

    void Setup() 
    {
        hero = transform.GetComponent<BaseHero>();

        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        heroPathing = transform.GetComponent<HeroPathing>();
        heroInteraction = transform.GetComponent<HeroInteraction>();

        collider = transform.GetComponent<Collider>();

        animHandler = transform.GetComponent<HeroAnimHandler>();
    }

    void CheckForNulls()
    {
        if (hero == null)
        {
            Debug.LogError("*-*-*-*-* Hero null on HeroManager: " + gameObject.name + "*-*-*-*-*");
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("*-*-*-*-* navMeshAgent null on HeroManager: " + gameObject.name + "*-*-*-*-*");
        }

        if (heroPathing == null)
        {
            Debug.LogError("*-*-*-*-* heroPathing null on HeroManager: " + gameObject.name + "*-*-*-*-*");
        }

        if (heroInteraction == null)
        {
            Debug.LogError("*-*-*-*-* heroInteraction null on HeroManager: " + gameObject.name + "*-*-*-*-*");
        }

        if (collider == null)
        {
            Debug.LogError("*-*-*-*-* collider null on HeroManager: " + gameObject.name + "*-*-*-*-*");
        }

        if (animHandler == null)
        {
            Debug.LogError("*-*-*-*-* animHandler null on HeroManager: " + gameObject.name + "*-*-*-*-*");
        }
    }

}

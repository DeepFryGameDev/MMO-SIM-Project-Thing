using UnityEngine;

// Purpose: Used to keep track of hero objects as they transition from scene to scene.  This keeps them from unloading and can be referenced at any point anywhere.
// Directions: Attach to the [Heroes] GameObject.
// Other notes: 

public class ActiveHeroesSystem : MonoBehaviour
{
    public static ActiveHeroesSystem i;

    GameObject felricObject;
    HeroManager felricHeroManager;
    GameObject archieObject;
    HeroManager archieHeroManager;
    GameObject mayaObject;
    HeroManager mayaHeroManager;
    GameObject claraObject;
    HeroManager claraHeroManager;
    GameObject nicholinObject;
    HeroManager nicholinHeroManager;

    void Awake()
    {
        Singleton();
    }

    void Singleton()
    {
        if (i != null)
            Destroy(gameObject);
        else
            i = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Sets the GameObject for the corresponding hero.  This should only need to be called once I think because they will never change.
    /// </summary>
    /// <param name="ID">ID of the hero to set the object</param>
    /// <param name="obj">GameObject of the hero that has already been instantiated.  The hero's GameObject will be set to the object in this script.</param>
    public void SetHeroObject(int ID, GameObject obj)
    {
        switch (ID)
        {
            case 0:
                felricObject = obj;
                break;
            case 1:
                archieObject = obj;
                break;
            case 2:
                mayaObject = obj;
                break;
            case 3:
                claraObject = obj;
                break;
            case 4:
                nicholinObject = obj;
                break;
        }
    }

    /// <summary>
    /// This will return the GameObject that is being transitioned between scenes.  It shouldn't need to be referenced for much, but can always be an easy reference to these objects.
    /// </summary>
    /// <param name="ID">ID of the hero to get the GameObject</param>
    /// <returns>GameObject of the hero</returns>
    public GameObject GetHeroObject(int ID)
    {
        switch (ID)
        {
            case 0:
                return felricObject;
            case 1:
                return archieObject;
            case 2:
                return mayaObject;
            case 3:
                return claraObject;
            case 4:
                return nicholinObject;
            default:
                return null;
        }
    }

    /// <summary>
    /// I don't think these will be needed?  This just stores the hero managers in this script so they aren't de-referenced when loading a new scene, but they are still saved as Idle and In Party Heroes in GameSettings.
    /// </summary>
    /// <param name="heroManager">HeroManager of the hero to set</param>
    public void SetHeroManager(HeroManager heroManager)
    {
        switch (heroManager.GetID())
        {
            case 0:
                felricHeroManager = heroManager;
                break;
            case 1:
                archieHeroManager = heroManager;
                break;
            case 2:
                mayaHeroManager = heroManager;
                break;
            case 3:
                claraHeroManager = heroManager;
                break;
            case 4:
                nicholinHeroManager = heroManager;
                break;
        }
    }

    /// <summary>
    /// Again not sure if this is needed.  Just returns the HeroManager here by ID.
    /// </summary>
    /// <param name="ID">ID of the HeroManager to be returned</param>
    /// <returns>The HeroManager assigned to this ID</returns>
    public HeroManager GetHeroManager(int ID)
    {
        switch (ID)
        {
            case 0:
                return felricHeroManager;
            case 1:
                return archieHeroManager;
            case 2:
                return mayaHeroManager;
            case 3:
                return claraHeroManager;
            case 4:
                return nicholinHeroManager;
            default:
                return null;
        }
    }
}

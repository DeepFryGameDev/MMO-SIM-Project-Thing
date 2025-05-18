using UnityEngine;

// Purpose: 
// Directions: 
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

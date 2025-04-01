using UnityEngine;

// Purpose: Provides the ability to read items/equipment in the "Resources/GameItems" object. This will likely need to be updated.
// Directions: Simply call GameItems.i.[BaseItemScriptableObject referenced below] to obtain the item details set in the 'Resources/GameItems' object
// Other notes: 

public class GameItems : MonoBehaviour
{
    private static GameItems _i;

    public static GameItems i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameItems>("GameItems"));
            return _i;
        }
    }
}

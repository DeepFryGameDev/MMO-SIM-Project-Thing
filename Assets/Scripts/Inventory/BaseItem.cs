using UnityEngine;

// Purpose: Used as the base level class for inheritance on all items
// Directions: Derived classes from this should have a corresponding CreateAssetMenu.  Simply just derive this if we are making a new type of item.
// Other notes: 

public class BaseItem : ScriptableObject
{
    [Header("---Base Item Values---")]
    new public string name;

    public int ID;

    public Sprite icon;

    public string description;
}

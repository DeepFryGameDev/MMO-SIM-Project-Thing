using UnityEngine;

// Purpose: 
// Directions: 
// Other notes: 

public class TestBattle : BaseBattle
{ 
    public override void OnInteract()
    {
        base.OnInteract();
        DebugManager.i.BattleDebugOut("Test Battle", "Test battle initated!");

        StartCoroutine(bse.BattleTransition(battleScene));
    }
}

using UnityEngine;

public class BaseAction
{
    BaseAttackableUnit sourceUnit;
    public void SetSourceUnit(BaseAttackableUnit unit) { sourceUnit = unit; }
    public BaseAttackableUnit GetSourceUnit() { return sourceUnit; }

    BaseAttackableUnit targetUnit;
    public void SetTargetUnit(BaseAttackableUnit unit) { targetUnit = unit; }
    public BaseAttackableUnit GetTargetUnit() { return targetUnit; }

    EnumHandler.battleActionTypes actionType;
    public void SetActionType(EnumHandler.battleActionTypes type) { actionType = type; }
    public EnumHandler.battleActionTypes GetActionType() { return actionType; }

    public BaseAction(BaseAttackableUnit sourceUnit, BaseAttackableUnit targetUnit, EnumHandler.battleActionTypes actionType) 
    { 
        this.sourceUnit = sourceUnit;
        this.targetUnit = targetUnit;
        this.actionType = actionType;
    }
}

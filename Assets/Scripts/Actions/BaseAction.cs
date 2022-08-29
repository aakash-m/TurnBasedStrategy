using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
    }

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public abstract string GetActionName();

    public virtual bool IsValidactionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }    

    public virtual int GetActionPointsCost()
    {
        return 1;
    }


}

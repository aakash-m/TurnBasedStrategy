using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive) return;
            
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360)
        {
            ActionComplete();
        }
    }
    
    //Spin Action
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {    
        totalSpinAmount = 0f;
        ActionStart(onActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition> { unitGridPosition };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;

    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField] bool isEnemy;

    private GridPosition currentgridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private HealthSystem healthSystem;
    private BaseAction[] baseActionArray;

    private int actionPoints = ACTION_POINTS_MAX;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        healthSystem = GetComponent<HealthSystem>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        currentgridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentgridPosition, this);

        healthSystem.OnDead += HealthSystem_OnDead;

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(currentgridPosition, this);
        Destroy(gameObject);
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if ( (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()) )
        {
            actionPoints = ACTION_POINTS_MAX;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        
        GridPosition newgridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newgridPosition != currentgridPosition)
        {
            //Unit changed grid position
            GridPosition oldGridPosition = currentgridPosition;
            currentgridPosition = newgridPosition;

            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newgridPosition);

        }

    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public GridPosition GetGridPosition()
    {
        return currentgridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

}

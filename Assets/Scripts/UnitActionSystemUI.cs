using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform actionButtonPrefab;
    [SerializeField] Transform actionButtonContainerTransform;

    private List<ActionButtonUI> actionButtonUIList;

    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnityChanged += UnitActionSystem_OnSelectedUnityChanged;
        UnitActionSystem.Instance.OnSelectedActinChanged += UnitActionSystem_OnSelectedActinChanged;
       
        CreateUnitActionButton();
        UpdatedSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedActinChanged(object sender, EventArgs e)
    {
        UpdatedSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedUnityChanged(object sender, System.EventArgs e)
    {
        CreateUnitActionButton();
        UpdatedSelectedVisual();
    }

    private void CreateUnitActionButton()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);

            actionButtonUIList.Add(actionButtonUI);

        } 
    }

    private void UpdatedSelectedVisual()
    {
        foreach(ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

}

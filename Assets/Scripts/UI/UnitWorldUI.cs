using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] TextMeshProUGUI actionPointsText;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] Image healthBarImage;


    private void Start()
    {
        //One drawback of this static event callback is it will call for every single unit world ui even if one unit changes its action points
        //For now not a big issue but it will be if units constantly changing action points or if we have thousands of units
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }

}

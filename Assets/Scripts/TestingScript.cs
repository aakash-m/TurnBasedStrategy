using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisuals.Instance.HideAllGridPosition();
           // GridSystemVisuals.Instance.ShowGridPositionList(unit.GetMoveAction().GetValidGridPositionList()); 
        }
    }

}

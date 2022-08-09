using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
 
    [SerializeField] LayerMask mousePlaneLayer;

    private void Awake()
    {
        instance = this;
    }

    private static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayer);
        return raycastHit.point;

    }

}

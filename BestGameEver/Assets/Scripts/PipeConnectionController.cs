using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeConnectionController : MonoBehaviour
{
    [HideInInspector] public PipeConnect _selected;
    [HideInInspector] public Rope _rope;
    public LayerMask LayerMask;

    private void Update()
    {
        return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            if (hit.collider.TryGetComponent<Building>(out Building building))
            {
                _selected = null;
                //_rope.
            }
        }
    }
}

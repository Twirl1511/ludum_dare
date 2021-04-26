using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeConnectionController : MonoBehaviour
{
    [HideInInspector] public PipeConnect _selected;
    public LayerMask LayerMask;

    public static PipeConnectionController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && _selected != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
            {
                if (hit.collider.TryGetComponent<Building>(out Building building))
                {
                    float distanse = Vector3.Distance(_selected.rope.buildingFrom.transform.position, building.transform.position);
                    if (distanse <= (_selected.rope._ropeLength) && _selected.rope.buildingFrom != building)
                    {
                        _selected.rope.RepairPipe(building);
                    }
                    _selected.selection.SetActive(false);
                    _selected = null;
                }
            }
        }
    }
}

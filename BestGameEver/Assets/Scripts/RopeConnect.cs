using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnect : MonoBehaviour
{
    [SerializeField] private LayerMask _layerBuilding;

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _layerBuilding))
            {
                Vector3 position = hit.collider.transform.GetChild(0).position;
                if (hit.collider.GetComponent<PositionToBuild>().IsOcupied() == false)
                {
                    BuildStructure(position);
                    hit.collider.GetComponent<PositionToBuild>().SetIsOcupied(true);
                }
            }
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float RayLength;
    public LayerMask LayerMask;
    [SerializeField] private float _ropeLength;
    private Vector3 _startPosition;
    private bool _isFoundStart;
    private Vector3 _endPosition;
    [SerializeField] private Building _prefabStructure;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (hit.collider.GetComponent<PositionToBuild>().IsOcupied())
                {
                    print(1);
                    _startPosition = hit.collider.GetComponent<PositionToBuild>().Position.position;
                    _isFoundStart = true;
                }
                if(!hit.collider.GetComponent<PositionToBuild>().IsOcupied() && _isFoundStart)
                {
                    print(2);
                    _endPosition = hit.collider.GetComponent<PositionToBuild>().Position.position;
                    float distanse = Vector3.Distance(_startPosition, _endPosition);
                    if (distanse <= _ropeLength)
                    {
                        print(3);
                        Vector3 position = hit.collider.GetComponent<PositionToBuild>().Position.position;
                        BuildStructure(position, hit.collider.GetComponent<PositionToBuild>());
                        hit.collider.GetComponent<PositionToBuild>().SetIsOcupied(true);
                        _isFoundStart = false;
                    }
                    else
                    {
                        _isFoundStart = false;
                    }
                }

            }
        }


    }

    private void BuildStructure(Vector3 position, PositionToBuild platform)
    {
        Building building = Instantiate(_prefabStructure, position, Quaternion.identity, platform.transform);
        building._platform = platform.GetComponent<PlatformMove>();
    }

}

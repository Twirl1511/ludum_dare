using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float RayLength;
    public LayerMask LayerMask;
    [SerializeField] private Building _prefabStructure;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if(hit.collider.GetComponent<PositionToBuild>().IsOcupied() == false)
                {
                    Vector3 position = hit.collider.GetComponent<PositionToBuild>().Position.position;
                    BuildStructure(position, hit.collider.GetComponent<PositionToBuild>());
                    hit.collider.GetComponent<PositionToBuild>().SetIsOcupied(true);
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

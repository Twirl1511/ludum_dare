using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float RayLength;
    public LayerMask LayerMask;
    [SerializeField] private GameObject _prefabStructure;
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
                Vector3 position = hit.collider.transform.GetChild(0).position;
                if(hit.collider.GetComponent<PositionToBuild>().IsOcupied() == false)
                {
                    BuildStructure(position);
                    hit.collider.GetComponent<PositionToBuild>().SetIsOcupied(true);
                }
                
            }
        }    

    }

    private void BuildStructure(Vector3 position)
    {
        Instantiate(_prefabStructure, position, Quaternion.identity);
        
    }

}

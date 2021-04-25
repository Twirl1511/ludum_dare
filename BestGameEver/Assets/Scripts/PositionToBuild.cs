using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToBuild : MonoBehaviour
{
    public Transform Position;
    public Building building;
    [SerializeField] private bool _isOcupied;
    public GameObject ColliderToHihlightPositionToBuild;
    public GameObject PositionToBuildObject;
    private void Start()
    {
        ColliderToHihlightPositionToBuild.SetActive(false);
    }
    public bool IsOcupied()
    {
        return _isOcupied;
    }
    public void SetIsOcupied(bool flag)
    {
        _isOcupied = flag;
        PositionToBuildObject.SetActive(!flag);
    }


}

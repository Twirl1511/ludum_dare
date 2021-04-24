using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToBuild : MonoBehaviour
{
    [SerializeField] private bool _isOcupied;

    public bool IsOcupied()
    {
        return _isOcupied;
    }
    public void SetIsOcupied(bool flag)
    {
        _isOcupied = flag;
    }
}

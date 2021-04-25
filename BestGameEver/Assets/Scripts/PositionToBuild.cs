﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToBuild : MonoBehaviour
{
    public Transform Position;
    public Building building;
    [SerializeField] private bool _isOcupied;
    public GameObject SphereCollider;
    private void Start()
    {
        SphereCollider.SetActive(false);
    }
    public bool IsOcupied()
    {
        return _isOcupied;
    }
    public void SetIsOcupied(bool flag)
    {
        _isOcupied = flag;
    }


}

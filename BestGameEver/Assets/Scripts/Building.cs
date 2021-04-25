﻿using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private int _production = 1;
    [SerializeField] private int _productionIncrement = 1;
    [SerializeField] private int _pipeSuckPower = 1;
    [SerializeField] private float _productionSpeed = 1;
    public Building ConnectedBuilding;
    public Transform PipePosition;
    public RopeRendering _ropeRender;
    public PlatformMove _platform;
    public int _maxMass = 500;

    //[Header("Формула")]
    //[SerializeField] private float multiplyBy = 0.107f;
    //[SerializeField] private float powerBy = 1.05f;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        InvokeRepeating(nameof(Production), _productionSpeed, _productionSpeed);
        InvokeRepeating(nameof(ProductionIncrement), 60f, 60f);
        _platform.InitFall();
    }

    public void Highlight(bool state)
    {
        if(state)
            _renderer.material.EnableKeyword("_EMISSION");
        else
            _renderer.material.DisableKeyword("_EMISSION");
    }

    private void ProductionIncrement()
    {
        _production += _productionIncrement;
    }

    private void Production()
    {
        if (_platform != null)
            _platform._mass += _production;
        if (ConnectedBuilding != null && ConnectedBuilding._platform._mass >= _pipeSuckPower)
        {
            ConnectedBuilding._platform._mass -= _pipeSuckPower;
            _platform._mass += _pipeSuckPower;
        }
        if (_platform._mass > _maxMass)
        {
            Destroy(_platform.gameObject);
            Destroy(gameObject);
        }
    }

    //private float CalculateProduction()
    //{
    //    float result = (float)_production;
    //    result = Mathf.Pow(_platform._mass * _productionSpeed * multiplyBy, powerBy);
    //    return result;
    //}
}

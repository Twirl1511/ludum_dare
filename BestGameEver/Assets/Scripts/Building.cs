using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] public int _production = 1;
    [SerializeField] private int _productionIncrementSpeed = 60;
    [SerializeField] private float _productionSpeed = 1;
    public Transform PipePosition;
    public PlatformMove _platform;
    public int _maxMass = 500;
    public bool HasPipe = true;
    public Transform[] PipeInputs;
    public bool mainBase = true;

    [HideInInspector] public List<Rope> pipes = new List<Rope>();

    // счетчик смертей
    public static float DeathCounter;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        InvokeRepeating(nameof(Production), _productionSpeed, _productionSpeed);
        InvokeRepeating(nameof(ProductionIncrement), _productionIncrementSpeed, _productionIncrementSpeed);
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
        _production += 1;
    }

    private void Production()
    {
        if (_platform != null && _production > 0)
            _platform.Mass += _production;

        if (_platform.Mass > _maxMass)
        {
            Destroy(_platform.gameObject);
            Destroy(gameObject);
        }
    }

}

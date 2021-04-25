using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private int _production = 1;
    [SerializeField] private int _productionIncrement = 1;
    [SerializeField] private float _productionSpeed = 1;
    public Building ConnectedBuilding;
    public Transform PipePosition;
    public RopeRendering _ropeRender;
    public PlatformMove _platform;
    public int _maxMass = 500;

    [SerializeField] private Dependency[] _suckPower;
    [HideInInspector] public float _pipeSuckPower = 0;

    //[Header("Формула")]
    //[SerializeField] private float multiplyBy = 0.107f;
    //[SerializeField] private float powerBy = 1.05f;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        InvokeRepeating(nameof(Production), _productionSpeed, _productionSpeed);
        InvokeRepeating(nameof(ProductionIncrement), 60f, 60f);
        _platform.InitFall();
        CalculateSuckPower();
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
        if (_platform != null && _production > 0)
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
        CalculateSuckPower();
    }

    public void CalculateSuckPower()
    {
        if (ConnectedBuilding == null)
            return;
        float deltaHeight = ConnectedBuilding.transform.position.y - transform.position.y;
        for (int i = 0; i < _suckPower.Length; i++)
        {
            if (deltaHeight > _suckPower[i].Height)
            {
                if(i < _suckPower.Length - 1 && deltaHeight < _suckPower[i + 1].Height)
                {
                    _pipeSuckPower = _suckPower[i].SuckPower;
                    break;
                }
                if(i == _suckPower.Length - 1)
                {
                    _pipeSuckPower = _suckPower[i].SuckPower;
                }
            }
        }
    }

    //private float CalculateProduction()
    //{
    //    float result = (float)_production;
    //    result = Mathf.Pow(_platform._mass * _productionSpeed * multiplyBy, powerBy);
    //    return result;
    //}

    [System.Serializable]
    public struct Dependency
    {
        public float Height;
        public float SuckPower;
    }
}

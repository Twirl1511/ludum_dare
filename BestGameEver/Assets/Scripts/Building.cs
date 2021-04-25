using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private int _production = 1;
    [SerializeField] private int _productionIncrementSpeed = 60;
    [SerializeField] private float _productionSpeed = 1;
    public Building ConnectedBuilding;
    public Transform PipePosition;
    public RopeRendering _ropeRender;
    public PlatformMove _platform;
    public int _maxMass = 500;
    public bool BrokenPipe = false;

    [SerializeField] private Dependency[] _suckPower;
    [SerializeField] private DependencyPipe[] _dropSpeed;
    [HideInInspector] public int _currentSuckIndex = 0;
    [HideInInspector] public float _pipeSuckPower = 0;

    //[Header("Формула")]
    //[SerializeField] private float multiplyBy = 0.107f;
    //[SerializeField] private float powerBy = 1.05f;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        InvokeRepeating(nameof(Production), _productionSpeed, _productionSpeed);
        InvokeRepeating(nameof(ProductionIncrement), _productionIncrementSpeed, _productionIncrementSpeed);
        //_platform.InitFall();
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
        _production += 1;
    }

    private void Production()
    {
        if (_platform != null && _production > 0)
            _platform.Mass += _production;

        CalculatePipe();
        
        if (_platform.Mass > _maxMass)
        {
            Destroy(_platform.gameObject);
            Destroy(gameObject);
        }
        CalculateSuckPower();
    }

    private void CalculatePipe()
    {
        if (ConnectedBuilding != null && ConnectedBuilding._platform.Mass >= _pipeSuckPower)
        {
            ConnectedBuilding._platform.Mass -= _pipeSuckPower;
            _platform.Mass += _pipeSuckPower;
        }

        if(BrokenPipe)
        {
            float drops = 0f;
            for (int i = 0; i < _dropSpeed.Length; i++)
            {
                if (_platform.Mass < _dropSpeed[i].Humans)
                {
                    drops = _dropSpeed[i].DropPerSec;
                    break;
                }
            }
            _platform.Mass -= drops;
            if (_platform.Mass < 0f)
                _platform.Mass = 0f;
        }
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
                    _currentSuckIndex = i;
                    break;
                }
                if(i == _suckPower.Length - 1)
                {
                    _pipeSuckPower = _suckPower[i].SuckPower;
                    _currentSuckIndex = i;
                }
            }
        }
    }

    [System.Serializable]
    public struct Dependency
    {
        public float Height;
        public float SuckPower;
    }

    [System.Serializable]
    public struct DependencyPipe
    {
        public float Humans;
        public float DropPerSec;
    }
}

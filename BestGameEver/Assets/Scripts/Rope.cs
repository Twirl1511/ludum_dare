using UnityEngine;

public class Rope : MonoBehaviour
{
    public Building buildingFrom;
    public Building buildingTo;
    public float _ropeLength;
    public bool BrokenPipe = false;
    [HideInInspector] public RopeRendering _renderer;

    [HideInInspector] public int _currentSuckIndex = 0;
    [HideInInspector] public int _suckArrows = 0;
    [HideInInspector] public float _pipeSuckPower = 0;
    [SerializeField] private Dependency[] _suckPower;
    [SerializeField] private DependencyPipe[] _dropSpeed;

    public static event System.Action<float> OnHumanDrops;

    public void Init(Building from, Building to)
    {
        buildingFrom = from;
        buildingTo = to;
        _renderer = GetComponent<RopeRendering>();
        _renderer.Init(from, to);

        InvokeRepeating(nameof(Suck), 0.5f, 0.5f);
        CalculateSuckPower();
    }

    public void BrokePipe()
    {
        Building upperBuilding;
        upperBuilding = buildingFrom.transform.position.y > buildingTo.transform.position.y ? buildingFrom : buildingTo;

        buildingFrom = upperBuilding;
        buildingTo = null;

        BrokenPipe = true;

        _renderer.BrokePipe(buildingFrom);

        //_broken = true;
        //_p2.parent = null;
        //_p2.position = GetMiddlePos();
        //Quaternion rot = Quaternion.LookRotation((_p2.position - _p1.position).normalized);
        //_p2.rotation = rot;
        //_humanParticles.Play();
    }

    private void Suck()
    {
        CalculateSuckPower();
        CalculateSuck();
        CheckLength();
    }

    void CheckLength()
    {
        if(_renderer._length > _ropeLength)
        { 
            BrokePipe();
        }
    }

    public void CalculateSuckPower()
    {
        if (buildingFrom == null || buildingTo == null)
            return;
        float deltaHeight = buildingFrom.transform.position.y - buildingTo.transform.position.y;
        for (int i = 0; i < _suckPower.Length; i++)
        {
            if (deltaHeight > _suckPower[i].Height)
            {
                if (i < _suckPower.Length - 1 && deltaHeight < _suckPower[i + 1].Height)
                {
                    _pipeSuckPower = _suckPower[i].SuckPower;
                    _currentSuckIndex = i;
                    _suckArrows = _suckPower.Length - _currentSuckIndex;
                    break;
                }
                if (i == _suckPower.Length - 1)
                {
                    _pipeSuckPower = _suckPower[i].SuckPower;
                    _currentSuckIndex = i;
                    _suckArrows = _suckPower.Length - _currentSuckIndex;
                }
            }
        }
        if (_renderer._length > _ropeLength - 0.2f)
        {
            AudioController.singleton.PlayPipeTensionSound();
        }

        
    }

    private void CalculateSuck()
    {
        if (buildingFrom != null && buildingTo != null && buildingFrom._platform.Mass >= _pipeSuckPower)
        {
            buildingFrom._platform.Mass -= _pipeSuckPower;
            buildingTo._platform.Mass += _pipeSuckPower;
        }
        // buildingFrom - откуда выпадают людишки (переделать, чтобы могли выпадать из любого здания)
        if (BrokenPipe && buildingFrom._platform.Mass > 0f)
        {
            /// выпадающие людишки
            float drops = 0f;
            for (int i = 0; i < _dropSpeed.Length; i++)
            {
                if (buildingFrom._platform.Mass < _dropSpeed[i].Humans)
                {
                    drops = _dropSpeed[i].DropPerSec;
                    break;
                }
            }
            //счетчик смертей
            //DeathCounter += drops;
            OnHumanDrops?.Invoke(drops);

            AudioController.singleton.PlayPipeTensionSoundONN();
            AudioController.singleton.PlayScreamSound();

            buildingFrom._platform.Mass -= drops;
            if (buildingFrom._platform.Mass < 0f)
                buildingFrom._platform.Mass = 0f;
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

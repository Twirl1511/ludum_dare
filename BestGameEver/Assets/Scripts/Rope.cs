using UnityEngine;

public class Rope : MonoBehaviour
{
    public Building buildingFrom;
    public Building buildingTo;
    public Building buildingToPrev;
    public float _ropeLength;
    public bool BrokenPipe = false;
    [HideInInspector] public RopeRendering _renderer;

    [HideInInspector] public int _currentSuckIndex = 0;
    [HideInInspector] public int _suckArrows = 0;
    [HideInInspector] public float SuckSpeed = 1f;
    [HideInInspector] public float _pipeSuckPower = 0;
    [SerializeField] private Dependency[] _suckPower;
    [SerializeField] private DependencyPipe[] _dropSpeed;

    public bool StartACtive = false;

    private void Awake()
    {
        if(StartACtive)
        {
            InvokeRepeating(nameof(Suck), SuckSpeed, SuckSpeed);

        }
    }

    public void Init(Building from, Building to)
    {
        from.pipes.Add(this);
        to.pipes.Add(this);

        buildingFrom = from;
        buildingToPrev = buildingTo;
        buildingTo = to;
        _renderer = GetComponent<RopeRendering>();
        _renderer.Init(from, to);

        InvokeRepeating(nameof(Suck), SuckSpeed, SuckSpeed);
        CalculateSuckPower();
    }

    public void RepairPipe(Building building)
    {
        buildingTo = building;
        buildingTo.pipes.Add(this);
        _renderer.Init(buildingFrom, buildingTo);
        AudioController.singleton.PlayPipeTensionSoundOFF();
        AudioController.singleton.StopScreamSound();
        BrokenPipe = false;
    }

    public void BrokePipe()
    {
        Building upperBuilding;
        upperBuilding = buildingFrom.transform.position.y > buildingTo.transform.position.y ? buildingFrom : buildingTo;

        //if(buildingFrom.mainBase || buildingTo.mainBase)
        //{
        //    Population.Instance.Lose();
        //    Time.timeScale = 0f;
        //    return;
        //}

        buildingFrom = upperBuilding;
        buildingToPrev = buildingTo;
        buildingTo.pipes.Remove(this);
        buildingTo = null;
        BrokenPipe = true;

        _renderer.BrokePipe(buildingFrom);
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

        if(BrokenPipe)
        {
            if((buildingFrom.transform.position - buildingToPrev.transform.position).magnitude <= _ropeLength)
            {
                RepairPipe(buildingToPrev);
            }
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
            Building.DeathCounter += drops;

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

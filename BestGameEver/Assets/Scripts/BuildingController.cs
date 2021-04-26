using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public static BuildingController singleton;
    public float RayLength;
    public float _ropeLength;
    public float minusLength;
    private bool _isFoundStart;
    public LayerMask LayerMask;
    private Vector3 _endPosition;
    private Vector3 _startPosition;
    private Building _baseBuilding = null;
    [SerializeField] private Building _prefabStructure;
    [SerializeField] private float _buildCD = 2f;
    private bool _canBuild = true;

    private void Awake()

    {

        singleton = this;

    }
    void Update()
    {
        if(Pause.State == Pause.States.Pause)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                if (hit.collider.TryGetComponent<Building>(out Building building))
                {
                    /// получаем позицию для строительства через домик
                    PositionToBuild positionToBuild = building._platform.GetComponent<PositionToBuild>();
                    _startPosition = positionToBuild.Position.position;
                    _isFoundStart = true;
                    if (_baseBuilding != null)
                    {
                        _baseBuilding.Highlight(false);
                    }
                    _baseBuilding = positionToBuild.building;
                    _baseBuilding.Highlight(true);
                    AudioController.singleton.PlayClickSound();
                    /// подстветка клеток где можно строить
                    if(highlightPosition != null)
                    {
                        if(positionToBuild != highlightPosition)
                            HiglightPositionOff();
                    }
                    HiglightPositionOn(positionToBuild);
                }
                else
                {
                    /// получаем позицию для строительства через платформу
                    PositionToBuild positionToBuild = hit.collider.GetComponentInParent<PositionToBuild>();
                    if (!positionToBuild.IsOcupied() && _isFoundStart && _canBuild)
                    {
                        /// отключаем подстветку клеток где можно строить
                        HiglightPositionOff();
                        _endPosition = positionToBuild.Position.position;
                        float distanse = Vector3.Distance(_startPosition, _endPosition);
                        if (distanse <= _ropeLength - minusLength)
                        {
                            Vector3 position = positionToBuild.Position.position;
                            BuildStructure(position, positionToBuild);
                            positionToBuild.SetIsOcupied(true);
                            _isFoundStart = false;
                            _canBuild = false;
                            Invoke(nameof(SetCanBuild), _buildCD);
                        }
                        else

                        {
                            _isFoundStart = false;

                        }
                        _baseBuilding.Highlight(false);
                    }
                } 
            }
        }
    }

    void SetCanBuild() => _canBuild = true;

    private void BuildStructure(Vector3 position, PositionToBuild platform)
    {
        Building building = Instantiate(_prefabStructure, position, Quaternion.identity, platform.transform);
        AudioController.singleton.PlayBuildSound();
        platform.building = building;
        building._platform = platform.GetComponent<PlatformMove>();
        /// для отображения количества людей в здании
        building.GetComponent<ShowPopulation>()._platform = building._platform;
        //building._platform.InitFall();
        building.ConnectedBuilding = _baseBuilding;
        building._ropeRender._building = building;
        building._ropeRender.SetRopeBase(_baseBuilding);

        //building._ropeRender.SetPos1(_baseBuilding.PipePosition);
        //building._ropeRender.SetPos2(building.PipePosition);

        building._ropeRender.Init();
        building._ropeRender.SetActive(true);
    }


    private PositionToBuild highlightPosition;
    private void HiglightPositionOn(PositionToBuild from)
    {
        highlightPosition = from;
        from.ColliderToHihlightPositionToBuild.SetActive(true);
    }
    
    private void HiglightPositionOff()
    {
        highlightPosition.ColliderToHihlightPositionToBuild.GetComponent<HiglightPositionsToBuild>().HiglightOff();
        highlightPosition = null;
    }
}

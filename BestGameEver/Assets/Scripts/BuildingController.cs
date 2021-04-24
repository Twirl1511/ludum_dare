using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float RayLength;
    public float _ropeLength;
    private bool _isFoundStart;
    public LayerMask LayerMask;
    private Vector3 _endPosition;
    private Vector3 _startPosition;
    private Building _baseBuilding = null;
    [SerializeField] private Building _prefabStructure;
    [SerializeField] private float _buildCD = 2f;
    private bool _canBuild = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canBuild)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RayLength, LayerMask))
            {
                PositionToBuild platform = hit.collider.GetComponent<PositionToBuild>();
                if (platform.IsOcupied())
                {
                    _startPosition = platform.Position.position;
                    _isFoundStart = true;
                    if(_baseBuilding != null)
                        _baseBuilding.Highlight(false);
                    _baseBuilding = platform.building;
                    _baseBuilding.Highlight(true);
                }
                if (!platform.IsOcupied() && _isFoundStart)
                {
                    _endPosition = platform.Position.position;
                    float distanse = Vector3.Distance(_startPosition, _endPosition);
                    if (distanse <= _ropeLength)
                    {
                        Vector3 position = platform.Position.position;
                        BuildStructure(position, platform);
                        platform.SetIsOcupied(true);
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

    void SetCanBuild() => _canBuild = true;

    private void BuildStructure(Vector3 position, PositionToBuild platform)
    {
        Building building = Instantiate(_prefabStructure, position, Quaternion.identity, platform.transform);
        platform.building = building;
        building._platform = platform.GetComponent<PlatformMove>();
        /// для отображения количества людей в здании
        building.GetComponent<ShowPopulation>()._platform = building._platform;
        building._platform.InitFall();
        building.ConnectedBuilding = _baseBuilding;
        building._ropeRender.SetPos1(_baseBuilding.PipePosition);
        building._ropeRender.SetPos2(building.PipePosition);
        building._ropeRender.Init();
        building._ropeRender.SetActive(true);
    }
}

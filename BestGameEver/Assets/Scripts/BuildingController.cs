using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float RayLength;
    public LayerMask LayerMask;
    public float _ropeLength;
    private Vector3 _startPosition;
    private bool _isFoundStart;
    private Vector3 _endPosition;
    private Building _baseBuilding = null;
    [SerializeField] private Building _prefabStructure;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                    _baseBuilding = platform.building;
                }
                if(!platform.IsOcupied() && _isFoundStart)
                {
                    _endPosition = platform.Position.position;
                    float distanse = Vector3.Distance(_startPosition, _endPosition);
                    if (distanse <= _ropeLength)
                    {
                        Vector3 position = platform.Position.position;
                        BuildStructure(position, platform);
                        platform.SetIsOcupied(true);
                        _isFoundStart = false;
                    }
                    else
                    {
                        _isFoundStart = false;
                    }
                }
            }
        }
    }

    private void BuildStructure(Vector3 position, PositionToBuild platform)
    {
        Building building = Instantiate(_prefabStructure, position, Quaternion.identity, platform.transform);
        platform.building = building;
        building._platform = platform.GetComponent<PlatformMove>();
        /// для отображения количества людей в здании
        building.GetComponent<ShowPopulation>()._platform = building._platform;
        building._platform.InitFall();
        building._ropeRender.SetPos1(_baseBuilding.PipePosition);
        building._ropeRender.SetPos2(building.PipePosition);
        building._ropeRender.Init();
        building._ropeRender.SetActive(true);
    }
}

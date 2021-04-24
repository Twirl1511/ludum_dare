using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _production;
    [SerializeField] private float _productionSpeed;
    public Transform PipePosition;
    public RopeRendering _ropeRender;
    [HideInInspector] public PlatformMove _platform;

    void Start()
    {
        InvokeRepeating(nameof(Production), _productionSpeed, _productionSpeed);
    }

    private void Production()
    {
        if(_platform != null)
            _platform._mass += _production;
    }
}

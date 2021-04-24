using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _production;
    [SerializeField] private float _productionSpeed;
    [HideInInspector] public PlatformMove _platform;

    void Start()
    {
        InvokeRepeating(nameof(Production), _productionSpeed, _productionSpeed);
    }

    private void Production()
    {
        _platform._mass += _production;
    }
}

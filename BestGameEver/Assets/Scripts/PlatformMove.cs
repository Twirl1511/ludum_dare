using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    // Mass = 0   |   Height = 0
    // Mass = 2   |   Height = -0.2
    // Mass = 10   |   Height = -1.0

    [HideInInspector] public float _mass = 0f;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        InvokeRepeating(nameof(MoveDown), 5f, 5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _mass++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _mass--;
        }
    }

    public void MoveDown()
    {
        float newY = _startPosition.y - _mass / 10f;
        transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
    }
}

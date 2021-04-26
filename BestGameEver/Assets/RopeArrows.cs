using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeArrows : MonoBehaviour
{
    //public Rope rope;
    [SerializeField] private float speed = 0.6f;
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;

    }

    private void FixedUpdate()
    {
        material.SetTextureOffset("_EMISSION", new Vector2(Time.time * speed, 0));
    }
}

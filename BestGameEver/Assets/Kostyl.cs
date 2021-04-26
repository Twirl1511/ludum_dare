using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kostyl : MonoBehaviour
{
    public PlatformMove platform;
    public Population popl;
    void Start()
    {
        platform = GetComponentInParent<PlatformMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(platform.Mass > 700)
        {
            popl.Lose();
        }
    }
}

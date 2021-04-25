using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject[] Arrows;

    public void SetArrowCount(int amount)
    {
        if(amount> 0)
        {
            for(int i = 0; i < Arrows.Length; i++)
            {
                Arrows[i].gameObject.SetActive(false);
                if(i < amount)
                    Arrows[i].gameObject.SetActive(true);
            }
        }
    }
}

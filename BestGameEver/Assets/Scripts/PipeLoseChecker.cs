using UnityEngine;

public class PipeLoseChecker : MonoBehaviour
{
    public Population popl;
    public Building building;
    public float checkTimer = 120f;

    void Start()
    {
        //InvokeRepeating(nameof(Check), checkTimer, checkTimer);
    }

    //void Check()
    //{
    //    if(building.pipes.Count <= 0)
    //    {
    //        popl.Lose();
    //    }
    //}
}

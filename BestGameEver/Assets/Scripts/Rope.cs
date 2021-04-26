using UnityEngine;

public class Rope : MonoBehaviour
{
    public Building building;
    public RopeRendering renderer;
    public float _ropeLength;

    void Start()
    {
        InvokeRepeating(nameof(CheckLength), 0.5f, 0.5f);
    }

    void CheckLength()
    {
        if(renderer._length > _ropeLength)
        {
            //renderer.SetActive(false);
            renderer.BrokePipe();
            building.ConnectedBuilding = null;
            building.BrokenPipe = true;
        }
        if (renderer._length > _ropeLength - 0.2f)
        {
            print("tension sound");
            AudioController.singleton.PlayPipeTensionSound();
        }

        
    }
}

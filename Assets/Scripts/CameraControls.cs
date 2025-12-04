using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public Transform FishingHook;
    public bool followHook = false;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (followHook)
            transform.position = new Vector3(0, FishingHook.position.y, -10);
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,-10), Mathf.Abs(transform.position.y));
        }
    }
}

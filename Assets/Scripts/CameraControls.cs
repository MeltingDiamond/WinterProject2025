using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public Transform FishingHook;
    public bool followHook = false;

    
    void FixedUpdate()
    {
        // Should the hook move with the hooks y position or go towards 0,0
        if (followHook)
        {
            transform.position = new Vector3(0, FishingHook.position.y, -10);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,-10), 2);
        }
    }
}

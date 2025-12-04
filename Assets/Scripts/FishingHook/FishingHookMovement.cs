using System;
using UnityEngine;

public class FishingHookMovement : MonoBehaviour
{
    private InputManager _input;
    private Rigidbody2D _rigidbody2D;
    private CameraControls _cameraControls;
    private bool _isHooked = false;
    private RelativeJoint2D _joint2D;
    private float _dropSpeed = 5;
    private bool _dropped = false;
    private float _oldCrankDetectorZRotation;
    
    public new Camera camera;
    public GameManager gameManager;
    public GameObject crank;
    public GameObject crankDetector;
    
    private void Start()
    {
        _input = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _joint2D = GetComponent<RelativeJoint2D>();
        _joint2D.enabled = false;
        _cameraControls = camera.GetComponent<CameraControls>();
        
        // Center crank rotator in the center of the screen.
        crankDetector.transform.position = new Vector2();
        _oldCrankDetectorZRotation = crankDetector.transform.rotation.eulerAngles.z + 180;

    }

    private void Update()
    {
        // Drop the hook on touching the screen
        if (_input.isTouchingScreen)
            _dropped = true;
        if (_dropped)
        {
            // Hook is dropped, but not hooked into anything.
            if (!_isHooked)
            {
                // Camera follows the hook and hook drops slowly down
                _cameraControls.followHook = true;
                _rigidbody2D.linearVelocityY = -_dropSpeed;
                _rigidbody2D.position = new Vector2(camera.ScreenToWorldPoint(_input.touchPosition).x, _rigidbody2D.position.y);
            }
            // Hook is dropped and it is hooked into something
            else
            {
                // Camera returns to the top of the screen, and you rotate in a circle to reel on the hook
                _cameraControls.followHook = false;
                // Rotate a crank on the fishing rod to reel in the hook
                var screenToWorldPos = camera.ScreenToWorldPoint(_input.touchPosition);
                crankDetector.transform.rotation = Quaternion.Euler(0f, 0f, 0f) * Quaternion.AngleAxis(GetTheAngle(crankDetector.transform.position, screenToWorldPos), Vector3.forward);
                crank.transform.rotation = crankDetector.transform.rotation;
                
                _rigidbody2D.linearVelocityY = 0;
                // Take the touch position and point the crank detector towards it 
                var crankDetectorCurrentRotation = crankDetector.transform.rotation.eulerAngles;
                
                var deltaAngle = Mathf.DeltaAngle(_oldCrankDetectorZRotation, crankDetectorCurrentRotation.z);
                
                if (deltaAngle > 1f)
                {
                    // Move the hook upwards
                    _rigidbody2D.position = new Vector2(transform.position.x, transform.position.y + 1);
                }
                if (deltaAngle < -1f)
                {
                    _rigidbody2D.position = new Vector2(transform.position.x, transform.position.y - 1);
                }
                _oldCrankDetectorZRotation = crankDetectorCurrentRotation.z;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If you collide with something fishable hook it, you can only hook once
        if (!_isHooked && collision.gameObject.CompareTag("Fishable"))
        {
            collision.gameObject.GetComponent<FishMovement>().GetHooked(transform);
            _joint2D.connectedBody = collision.gameObject.GetComponentInChildren<Rigidbody2D>();
            _joint2D.enabled = true;
        }
        _isHooked = true;
    }
    
    
    private float GetTheAngle(Vector3 objectToPoint, Vector3 pointTowards)
    {
        // Gets the angle so an object correctly points towards another position
        // used to rotate the crank detector during reeling in of the hook
        Vector2 from = Vector2.right;
        Vector3 to = objectToPoint - pointTowards;

        float ang = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
            ang = 360 - ang;

        ang *= -1f;

        return ang;
    }
}

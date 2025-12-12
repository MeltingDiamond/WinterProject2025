using System;
using UnityEngine;

public class FishingHookMovement : MonoBehaviour
{
    private InputManager _input;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private CameraControls _cameraControls;
    private Collision2D _hookedFish;
    private FishMovement _hookedFishScript;
    private RelativeJoint2D _joint2D;
    private bool _isHooked = false;
    private float _reelSpeed = 20;
    private float _dropSpeed = 5;
    private bool _dropped = false;
    private float _oldCrankDetectorZRotation;
    private float _canDropHookTimer = 0f;
    private bool _canDrop = false;
    private SpriteRenderer _crankDetectorSprite;
    
    public new Camera camera;
    public GameObject crankDetector;
    public Sprite spinSprite;
    public FishSpawner fishSpawner;
    
    private void Start()
    {
        _input = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _joint2D = GetComponent<RelativeJoint2D>();
        _joint2D.enabled = false;
        _cameraControls = camera.GetComponent<CameraControls>();
        _crankDetectorSprite = crankDetector.GetComponent<SpriteRenderer>();
        
        _crankDetectorSprite.enabled = false;
        
        // Center crank rotator in the center of the screen.
        crankDetector.transform.position = new Vector2();
        _oldCrankDetectorZRotation = crankDetector.transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        if (_canDropHookTimer > 0)
        {
            _canDropHookTimer -= Time.deltaTime;
        }
        else
        {
            _canDrop = true;
        }
        
        // Drop the hook on touching the screen
        if (_input.isTouchingScreen && _canDrop)
        {
            _dropped = true;
        }
    }
    
    private void FixedUpdate()
    {
        if (_dropped)
        {
            if (transform.position.y < fishSpawner.oceanFloor)
            {
                _isHooked =  true;
            }
            // Hook is dropped, but not hooked into anything.
            if (!_isHooked)
            {
                _crankDetectorSprite.enabled = false;
                _rigidbody2D.linearVelocityY = -_dropSpeed;
                _rigidbody2D.excludeLayers = 0;
                // Camera follows the hook and hook drops slowly down
                _cameraControls.followHook = true;
                _rigidbody2D.position = new Vector2(camera.ScreenToWorldPoint(_input.touchPosition).x, _rigidbody2D.position.y);
            }
            // Hook is dropped and it is hooked into something
            else
            {
                // Camera returns to the top of the screen, and you rotate in a circle to reel in the hook
                _cameraControls.followHook = true;
                _rigidbody2D.excludeLayers = -1;

                if (transform.position.y > 1f)
                {
                    _joint2D.connectedBody = null;
                    _joint2D.enabled = false;
                    if (_hookedFish != null && _hookedFishScript)
                    {
                        print("You collected a " + _hookedFish.gameObject.name);
                        fishSpawner.RemoveFish(_hookedFish.gameObject);
                        _hookedFishScript.UnhookAndCollect();
                    }
                    Reset();
                    _rigidbody2D.linearVelocityY = 0;
                }
                else
                {
                    ReelWithCrank();
                }
            }
        }
    }

    private void Reset()
    {
        // Resets the fishing hook variables back to how it is at the start of the game
        _isHooked = false;
        _dropped = false;
        _canDropHookTimer = 0.5f;
        _canDrop = false;
        _cameraControls.followHook = false;
        _hookedFish = null;
        _hookedFishScript = null;
        _crankDetectorSprite.enabled = false;
    }

    private void ReelWithCrank()
    {
        // Rotate a crank on the fishing rod to reel in the hook
        _crankDetectorSprite.enabled = true;
        var screenToWorldPos = camera.ScreenToWorldPoint(_input.touchPosition);
        crankDetector.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, crankDetector.transform.position.z);
        crankDetector.transform.rotation = Quaternion.Euler(0f, 0f, 0f) * Quaternion.AngleAxis(GetTheAngle(crankDetector.transform.position, screenToWorldPos), Vector3.forward);
        
        // Take the touch position and point the crank detector towards it 
        var crankDetectorCurrentRotation = crankDetector.transform.rotation.eulerAngles;
                
        var deltaAngle = Mathf.DeltaAngle(_oldCrankDetectorZRotation, crankDetectorCurrentRotation.z);
                
        if (deltaAngle > 0.1f)
        {
            // Move the hook upwards
            _rigidbody2D.linearVelocityY = _reelSpeed + Mathf.Min(Mathf.Abs(transform.position.y), 10);
        }
        else
        {
            _rigidbody2D.linearVelocityY = 0;
        }
        //if (deltaAngle < -1f)
        //{
        // If we need to move in both directions
        //    _rigidbody2D.position = new Vector2(transform.position.x, transform.position.y - 1);
        //}
        _oldCrankDetectorZRotation = crankDetectorCurrentRotation.z;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If you collide with something fishable hook it, you can only hook once
        if (!_isHooked && collision.gameObject.CompareTag("Fishable"))
        {
            _hookedFishScript = collision.gameObject.GetComponent<FishMovement>();
            _hookedFishScript.GetHooked(transform);
            _joint2D.connectedBody = collision.gameObject.GetComponentInChildren<Rigidbody2D>();
            _joint2D.enabled = true;
            _hookedFish = collision;
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

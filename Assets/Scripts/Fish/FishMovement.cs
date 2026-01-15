using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _startPosition;
    private bool _hooked = false;
    private bool _fished = false;
    
    public float swimSpeed = 5;
    public bool facingLeft;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position.y;
        
        // Makes sure the fish swims the way it is facing
        if (facingLeft)
        {
            swimSpeed = -swimSpeed;
        }
        // Only collide with the hook
        _rigidbody2D.includeLayers = LayerMask.GetMask("Hook");
        
        // Choose a random swimming direction when fish is spawned
        var random = Random.Range(0, 2);
        if (random == 0)
        {
            transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
            swimSpeed = -swimSpeed;
        }
    }

    private void FixedUpdate()
    {
        // Fish is not hooked and is free swimming
        if (!_hooked && !_fished)
        {
            if (_rigidbody2D.position.x is > 3.5f or < -3.5f)
            {
                transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
                swimSpeed = -swimSpeed;
            }
            _rigidbody2D.linearVelocityX = swimSpeed;
            _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _startPosition);
        }
        // The fish has been fished up and is now getting collected
        else if (!_hooked && _fished)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(0f, 3.4f), 1f);
        }
        // The fish is on the fishing hook
        else if (_hooked)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
    
    // Sets the fish in hooked mode
    public void GetHooked(Transform hook)
    {
        if (!_hooked)
        {
            _rigidbody2D.angularVelocity = 0;
            _rigidbody2D.excludeLayers = -1;
        }
        _hooked = true;
    }
    
    public void UnhookAndCollect(float destroyTime)
    {
        _fished = true;
        _hooked = false;
        Destroy(gameObject, destroyTime);
    }
}

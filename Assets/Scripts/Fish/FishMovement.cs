using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float swimSpeed = 5;
    public bool facingLeft;
    private float _startPosition;
    private bool _hooked = false;
    private bool _fished = false;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position.y;
        if (facingLeft)
        {
            swimSpeed = -swimSpeed;
        }
        _rigidbody2D.includeLayers = LayerMask.GetMask("Hook");
        
        // Choose a random swimming direction when fish is spawned
        var random = Random.Range(0, 2);
        if (random == 0)
        {
            transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
            swimSpeed = -swimSpeed;
        }

        if (random == 2)
        {
            print("The random is king 2");
        }
    }

    private void FixedUpdate()
    {
        if (!_hooked && !_fished)
        {
            if (_rigidbody2D.position.x is > 4 or < -4)
            {
                transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
                swimSpeed = -swimSpeed;
            }
            _rigidbody2D.linearVelocityX = swimSpeed;
            _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _startPosition);
        }
        else if (!_hooked && _fished)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(0f, 3f), 1f);
        }
        else if (_hooked)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    public void GetHooked(Transform hook)
    {
        if (!_hooked)
        {
            //transform.Rotate(0, 0, -90f);
            _rigidbody2D.angularVelocity = 0;
            _rigidbody2D.excludeLayers = -1;
        }
        _hooked = true;
    }
    
    public void UnhookAndCollect()
    {
        _fished = true;
        _hooked = false;
        Destroy(gameObject, 2f);
    }
}

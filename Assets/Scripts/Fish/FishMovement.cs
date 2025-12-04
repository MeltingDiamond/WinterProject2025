using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float swimSpeed = 5;
    private float _startPosition;
    private bool _hooked = false;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position.y;
        if (transform.localScale.x == 1f)
        {
            swimSpeed = -swimSpeed;
        }
    }

    void Update()
    {
        if (!_hooked)
        {
            
            if (_rigidbody2D.position.x is > 4 or < -4)
            {
                transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
                swimSpeed = -swimSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_hooked)
        {
            _rigidbody2D.linearVelocityX = swimSpeed;
            _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _startPosition);  
        }
        else
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    public void GetHooked(Transform hook)
    {
        if (!_hooked)
        {
            transform.Rotate(0, 0, -90f);
            _rigidbody2D.angularVelocity = 0;
        }
        _hooked = true;
    }
}

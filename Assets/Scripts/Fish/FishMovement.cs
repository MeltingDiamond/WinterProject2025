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
    }

    void Update()
    {
        if (!_hooked)
        {
            _rigidbody2D.linearVelocity = new Vector2(swimSpeed, 0);
            _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _startPosition);
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

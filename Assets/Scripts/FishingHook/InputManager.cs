using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;
    
    public Vector2 touchPosition;
    public bool isTouchingScreen;
    
    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    private void Update()
    {
        touchPosition = _inputSystem.Player.TouchPosition.ReadValue<Vector2>();
        isTouchingScreen = _inputSystem.Player.TouchClick.IsPressed();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
    }
}

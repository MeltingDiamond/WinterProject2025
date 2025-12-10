using UnityEngine;

namespace Core.Input
{
    public class PlayerInput : MonoBehaviour
    {
        private InputActions _inputActions;

        public Vector2 MoveDirection { get; private set; }
        public Vector2 CameraMovement { get; private set; }
    
        public bool Fire { get; private set ; }
        public bool Interact { get; private set; }
    

        private void Update()
        {
            MoveDirection = _inputActions.Player.Move.ReadValue<Vector2>();
            CameraMovement = _inputActions.Player.Look.ReadValue<Vector2>();
        
            Fire = _inputActions.Player.Fire.triggered;
            Interact = _inputActions.Player.Interact.triggered;
        }

        private void Awake() => _inputActions = new InputActions();
        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();
    }
}

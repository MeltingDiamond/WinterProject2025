using Core.Input;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(PlayerAnimations))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerInput))]
    
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerMovement _movement;
        private PlayerAnimations _animations;

        private void Start()
        {
            _input = GetComponent<PlayerInput>();
            _movement = GetComponent<PlayerMovement>();
            _animations = GetComponent<PlayerAnimations>();
        }

        private void Update()
        {
            _animations.UpdateAnimations(_input.MoveDirection);
        }

        private void FixedUpdate()
        {
            _movement.UpdateMovement(_input.MoveDirection);
        }
    }
}

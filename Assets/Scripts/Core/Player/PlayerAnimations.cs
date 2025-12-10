using UnityEngine;

namespace Core.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void UpdateAnimations(Vector2 direction)
        {
            // If we have movement - Set our Animations
            if (direction != Vector2.zero)
            {
                _animator.SetFloat(Horizontal, direction.x);
                _animator.SetFloat(Vertical, direction.y);
                _animator.SetBool(IsWalking, true);
            }
            else
            {
                _animator.SetBool(IsWalking, false);
            }

            // Flip the Player if horizontal direction is not 0
            if (direction.x != 0f)
            {
                // transform.localScale = new Vector2(direction.x, 1f);
                // Flips just the sprite
               // _spriteRenderer.flipX = direction.x < 0;
            }
        }
    }
}

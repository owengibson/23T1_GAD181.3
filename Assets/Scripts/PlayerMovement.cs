using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeadowMateys
{
    public class PlayerMovement : MonoBehaviour
    {
        private enum Direction { Horizontal, Vertical };
        [SerializeField] private Direction playerDirection;

        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float jumpStrength = 8f;

        [SerializeField] private GameObject otherPlayer;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider2D;

        [SerializeField] private LayerMask levelLayerMask;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }
        private void FixedUpdate()
        {
            if (playerDirection == Direction.Horizontal)
            {
                MovePlayer(_rigidbody);
                if (Vector2.Distance(transform.position, otherPlayer.transform.position) > 5)
                {
                    MovePlayer(otherPlayer.GetComponent<Rigidbody2D>());
                }
            }
            if (playerDirection == Direction.Vertical && IsGrounded() && Input.GetButton("Jump"))
            {
                Debug.Log(IsGrounded());
                Jump();
            }
            if (playerDirection == Direction.Vertical)
            {
                Debug.Log(IsGrounded());
            }

            //float lastMoveDirection = rb.velocity.normalized.x;
        }
        private void MovePlayer(Rigidbody2D rb)
        {
            // ------------
            // if X position < otherCharacterPosition.x-distanceMax (that is, character A is to the left of character B, but not past max distance...) { move Right }
            // (also want to use a normalised vector here so that diagonals don't get weird
            // ------------
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
        private void Jump() => _rigidbody.velocity = new Vector2(0, jumpStrength);

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(_boxCollider2D.bounds.center, Vector2.down, _boxCollider2D.bounds.extents.y + 0.02f, levelLayerMask);
            return hit.collider != null;
        }
    }
}
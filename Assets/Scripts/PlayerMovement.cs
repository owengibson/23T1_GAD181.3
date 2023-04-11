using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD181_3
{
    public class PlayerMovement : MonoBehaviour
    {
        private enum Direction { Horizontal, Vertical };
        [SerializeField] private Direction playerDirection;

        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float jumpStrength = 8f;

        [SerializeField] private GameObject otherPlayer;

        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;

        [SerializeField] private LayerMask levelLayerMask;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        private void FixedUpdate()
        {
            if (playerDirection == Direction.Horizontal)
            {
                MovePlayer(rb);
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
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
        private void Jump() => rb.velocity = new Vector2(0, jumpStrength);

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.02f, levelLayerMask);
            return hit.collider != null;
        }
    }
}
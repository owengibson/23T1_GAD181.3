using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD181_3
{
    public class SimplePlayerMovement : MonoBehaviour
    {
        [SerializeField] private KeyCode leftKey, rightKey, jumpKey, crouchKey;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpStrength;
        [SerializeField] private float climbSpeed;
        [SerializeField] private LayerMask levelLayerMask;

        private bool isLadder;
        private bool isClimbing;

        private Rigidbody2D rb;
        private SpriteRenderer sr;
        private BoxCollider2D bc;
        private Animator anim;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            bc = GetComponent<BoxCollider2D>();
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            if (isLadder && (Input.GetKey(jumpKey) || Input.GetKey(crouchKey)))
            {
                isClimbing = true;
            }
        }
        private void FixedUpdate()
        {
            float lastXPos;
            lastXPos = transform.position.x;

            if (Input.GetKey(rightKey))
            {
                transform.position += new Vector3(moveSpeed, 0, 0);
                sr.flipX = false;
            }
            else if (Input.GetKey(leftKey))
            {
                transform.position += new Vector3(-moveSpeed, 0, 0);
                sr.flipX = true;
            }

            if (Input.GetKey(jumpKey) && IsGrounded() && !isClimbing)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            }
            else if (Input.GetKey(jumpKey) && isClimbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
            }
            else if (Input.GetKey(crouchKey) && isClimbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);
            }

            //---------ANIMATION CONTROL-----------//

            //Walk animation
            if (transform.position.x != lastXPos)
            {
                anim.SetFloat("Speed", 1);
            }
            else anim.SetFloat("Speed", 0);

            //Jump animation
            if (Mathf.Abs(rb.velocity.y) > 0.01f && !isClimbing)
            {
                anim.SetBool("isJumping", true);
            }
            else anim.SetBool("isJumping", false);

            //Climb animation
            if (Mathf.Abs(rb.velocity.y) > 0.01f && isClimbing)
            {
                anim.SetBool("isClimbing", true);
            }
            else anim.SetBool("isClimbing", false);
        }

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + 0.02f, levelLayerMask);
            return hit.collider != null;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                isLadder = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                isLadder = false;
                isClimbing = false;
                rb.gravityScale = 3f;
            }
        }
    }
}
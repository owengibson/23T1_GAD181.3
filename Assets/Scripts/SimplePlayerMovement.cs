using UnityEngine;

namespace MeadowMateys
{
    public class SimplePlayerMovement : MonoBehaviour
    {
        [SerializeField] private KeyCode leftKey, rightKey, jumpKey, crouchKey, increaseRopeKey, decreaseRopeKey, attachRopeKey;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpStrength;
        [SerializeField] private float climbSpeed;
        [SerializeField] private LayerMask levelLayerMask;
        [SerializeField] private Transform otherPlayer;
        [SerializeField] private AudioManager audioManager;

        private static bool _isRopeAttached = false;
        private static float _maxDistance = 5f;
        private bool _isLadder;
        private bool _isClimbing;
        private bool _isRopeAttachOnCooldown = false;
        private float _distance;

        private Rigidbody2D _rigidbody;
        private Rigidbody2D _otherPlayerRigidbody;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private Animator _animator;
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _otherPlayerRigidbody = otherPlayer.GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _lineRenderer = GetComponentInChildren<LineRenderer>();

            _maxDistance = 5f;
            _isRopeAttached = false;

            audioManager.Play("LevelMusic");
            audioManager.Play("Ambience");
        }
        private void Update()
        {
            if (_isLadder && (Input.GetKey(jumpKey) || Input.GetKey(crouchKey)))
            {
                _isClimbing = true;
            }

            _distance = Vector2.Distance(transform.position, otherPlayer.position);
            if (Input.GetKeyDown(attachRopeKey) && !_isRopeAttachOnCooldown && _lineRenderer != null)
            {
                _maxDistance = 5f;
                _isRopeAttached = !_isRopeAttached;
                Debug.Log("is rope attached?: " + _isRopeAttached);
                _lineRenderer.enabled = !_lineRenderer.enabled;
            }
        }
        private void FixedUpdate()
        {
            Vector3 currentMove = new Vector3(0f, _rigidbody.velocity.y, 0f);

            float lastXPos;
            lastXPos = transform.position.x;

            if (Input.GetKey(rightKey)) // Move right
            {
                currentMove = new Vector3(moveSpeed, _rigidbody.velocity.y, 0);
                _spriteRenderer.flipX = false;
            }
            else if (Input.GetKey(leftKey)) // Move left
            {
                currentMove = new Vector3(-moveSpeed, _rigidbody.velocity.y, 0);
                _spriteRenderer.flipX = true;
            }

            //--- Rope control ---//
            if (_isRopeAttached && _lineRenderer == null)
            {
                _distance = Vector2.Distance(transform.position, otherPlayer.position);
                if (_distance >= _maxDistance)
                {
                    //_otherPlayerRigidbody.velocity += new Vector2(currentMove.x, currentMove.y);
                    Vector3 direction = (transform.position - otherPlayer.position).normalized * 1000;
                    _otherPlayerRigidbody.AddForce(direction);
                    Debug.Log("Current move: " + currentMove + "| Direction: " + direction + "| Distance: " + _distance);
                }
                else _rigidbody.velocity = currentMove;

                if (Input.GetKey(increaseRopeKey) && _maxDistance <= 5f)
                {
                    _maxDistance += 0.1f;
                    Debug.Log(_maxDistance);
                }
                else if (Input.GetKey(decreaseRopeKey) && _maxDistance >= 2f)
                {
                    _maxDistance -= 0.1f;
                    Debug.Log(_maxDistance);
                }
            }
            else _rigidbody.velocity = currentMove;

            //--- Jump and climb ---//
            if (Input.GetKey(jumpKey) && IsGrounded() && !_isClimbing) // Jump
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpStrength);
                audioManager.Play("Jump");
            }
            else if (Input.GetKey(jumpKey) && _isClimbing) // Climb up
            {
                _rigidbody.gravityScale = 0f;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, climbSpeed);
            }
            else if (Input.GetKey(crouchKey) && _isClimbing) // Climb down
            {
                _rigidbody.gravityScale = 0f;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -climbSpeed);
            }

            //---------ANIMATION CONTROL-----------//

            //Walk animation
            if (Mathf.Abs(_rigidbody.velocity.x) != 0)
            {
                _animator.SetFloat("Speed", 1);
            }
            else _animator.SetFloat("Speed", 0);

            //Jump animation
            if (Mathf.Abs(_rigidbody.velocity.y) > 0.01f && !_isClimbing && !IsGrounded())
            {
                _animator.SetBool("isJumping", true);
            }
            else _animator.SetBool("isJumping", false);

            //Climb animation
            if (Mathf.Abs(_rigidbody.velocity.y) > 0.01f && _isClimbing)
            {
                _animator.SetBool("isClimbing", true);
            }
            else _animator.SetBool("isClimbing", false);
        }

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(_boxCollider2D.bounds.center, Vector2.down, _boxCollider2D.bounds.extents.y + 0.02f, levelLayerMask);
            return hit.collider != null;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                _isLadder = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                _isLadder = false;
                _isClimbing = false;
                _rigidbody.gravityScale = 3f;
            }
        }
    }
}
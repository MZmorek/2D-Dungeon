using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isJumping = false;
    private float horizontalInput;
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask playerMask;
    private Animator warriorAnim;

    public float movementSpeed = 3;
    public float jumpForce = 5.0f;
    private bool isFacingRight = true;
    private bool isRunning = false;
    //private bool jumping = false;
    public bool isGrounded;
    public float groundCheckRadius;

    public LayerMask whatIsGround;

    [SerializeField] AudioSource jumpSFX;
    [SerializeField] AudioSource footstepsSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        warriorAnim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerAnimations();
        CheckInput();
        CheckMovementDirection();
        Jump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckGround();
    }
    private void CheckInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * horizontalInput,rb.velocity.y);
    }

    private void CheckMovementDirection()
    {
        if(horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

        if (horizontalInput != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded)
        {
            jumpSFX.Play();
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    public void PlayerAnimations()
    {
        warriorAnim.SetBool("isRunning", isRunning);
        warriorAnim.SetBool("isJumping", isJumping);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

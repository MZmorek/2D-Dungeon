using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask whatIsGround;
    public LayerMask playerMask;

    public float groundCheckRadius;
    public float movementSpeed = 3;
    public float jumpForce = 5.0f;
    public bool isGrounded;

    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource footstepsSFX;
    [SerializeField] private Transform groundCheck = null;

    private bool isFacingRight = true;
    private bool isRunning = false;
    private bool isJumping = false;
    private float horizontalInput;

    private Rigidbody2D playerRigidbody;
    private Animator warriorAnim;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
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

    //=============== MOVEMENT CONDITIONS METHODS ===============
    private void CheckInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    private void CheckMovementDirection()
    {
        if (horizontalInput > 0 && !isFacingRight)
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
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }


    //=============== MOVEMENT METHODS ===============
    private void ApplyMovement()
    {
        playerRigidbody.velocity = new Vector2(movementSpeed * horizontalInput, playerRigidbody.velocity.y);
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
            playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    //=============== ANIMATIONS METHODS ===============
    public void PlayerAnimations()
    {
        warriorAnim.SetBool("isRunning", isRunning);
        warriorAnim.SetBool("isJumping", isJumping);
    }
}

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundSpeed = 8f;
    public float midAirSpeed = 7f;
    public float jumpingPower = 16f;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    public float dashVerticalMovementScale = 0.4f;
    public bool canDoubleJump = false;
    public float secondJumpScale = 1.6f;

    private float horizontal;
    private bool isFacingRight = true;
    private bool canDash = true;
    private bool isDashing;
    private float speed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Start() 
    {
        speed = groundSpeed;
    }

    private void Update()
    {
        if (isDashing)
        {
            //return;
        }

        
        // Checks if the player is grounded
        if(IsGrounded())
        {
            // If the player is not dashing, allow the player to dash
            if(!isDashing)
            {
                canDash = true;
            }
            speed = groundSpeed;
            canDoubleJump = true;
        
        // If the player is in the air, change their speed to the slower air speed
        } else {
            speed = midAirSpeed;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            
            } else if (canDoubleJump) {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower / secondJumpScale);
                canDoubleJump = false;
            }
        }

        // Slows player on the way up after jumping when the player lets go of the jump button - Allows for a short hop instead of always doing a full jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, rb.velocity.y * dashVerticalMovementScale);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
    }
}
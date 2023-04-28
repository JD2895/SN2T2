using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D rb;
    GameControls controls;
    Vector3 newVelocity;
    bool controlEnabled = true;

    [Header("Side Movement")]
    public float sideMoveAcceleration = 60f;
    public float sideMoveMax = 4f;

    [Header("Jump")]
    public float jumpForce = 9.5f;
    public float jumpBrakeMultiplier = 2f;
    public float minVelocityForJumpBrake = 1f;

    [Header("Buffers")]
    public float jumpCancelWaitTime;
    bool canJumpCancel = true;
    bool jumpCancelQueued;
    public float jumpQueuedTimeLimit;
    bool jumpQueued = false;
    public float coyoteTime;
    bool inCoyoteTime;

    [Header("Ground Check")]
    public LayerMask groundcheckLayermask;
    bool isGrounded = false;
    bool prevIsGrounded;
    public float raycastDistance = 1.5f;

    /*// Jump Timer START
    // Current average jump time = 1.013593
    bool timeJump = false;
    float jumpTimer = 0f;
    float totalJumpTime = 0f;
    int jumpsPerformed = 0;
    // Jump Timer END */

    //TEMP
    public Transform jumpMarker;
    public float avgJumpTime;

    private void Awake()
    {
        controls = new GameControls();

        if (controlEnabled)
        {
            controls.Main.Jump.performed += _ => JumpStart();
            controls.Main.Jump.canceled += _ => JumpEnd();
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //TEMP
        //StartCoroutine(SlowlyIncreaseMaxSpeed());
    }

    void Update()
    {
        newVelocity = rb.velocity;          // Get previous velocity before making any changes
        isGrounded = CheckGrounded();

        // Coyote time
        if (!isGrounded && prevIsGrounded)  // Check if the player is no longer grounded, but WAS grounded the last frame
        {
            if (canJumpCancel)      // Use jumpCancel availability as a check for if the player recently jumped
                StartCoroutine(coyoteTimeWait());
        }

        prevIsGrounded = isGrounded;

        // Velocity clamping
        if (newVelocity.x < sideMoveMax)
            newVelocity.x += sideMoveAcceleration * Time.fixedDeltaTime;
        else
            newVelocity.x -= sideMoveAcceleration * Time.fixedDeltaTime;

        rb.velocity = newVelocity;          // Set new velocity

        if (jumpQueued && isGrounded)       // Queued jump executes when grounded
            PeformJump();

        /*// Jump Timer START
        if (timeJump)
            jumpTimer += Time.deltaTime;
        // Jump Timer END */
    }

    #region Jump control
    public void JumpStart()
    {
        jumpCancelQueued = false;   // Immediately cancel a queued jumpCancel on a new jump attempt (from player input)
        PeformJump();
    }

    public void JumpEnd()
    {
        PerformJumpCancel();
    }

    public void PeformJump()
    {
        if (rb != null && (isGrounded || inCoyoteTime))
        {
            StartCoroutine(jumpCancelWait());
            ApplyJumpForce();
            jumpQueued = false;
        }
        else
        {
            jumpQueued = true;
            StartCoroutine(jumpQueuedWait());
        }
    }

    public void PerformJumpCancel()
    {
        // Check if rigidbody exists and is moving upwards fast enough
        if (rb != null && rb.velocity.y > minVelocityForJumpBrake)
        {
            jumpCancelQueued = true;        // Queue the jump
            if (canJumpCancel)              // Cancel the jump
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / jumpBrakeMultiplier);
                jumpCancelQueued = false;
            }
            else if (!canJumpCancel && !isGrounded)     // Else, queue the jump if jump canceling isn't available yet
                jumpCancelQueued = true;
        }
        // Else, reset jump trackers (player is on the way down to the ground, or on it already)
        else
        {
            jumpCancelQueued = jumpQueued ? true : false;   // If a jump was queued, then preserve the jumpCancel
            canJumpCancel = true;
        }
    }

    public void ApplyJumpForce()
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);

            /*// Jump Timer START
            timeJump = true;
            jumpsPerformed++;
            // Jump Timer END */

            //TEMP
            Vector3 newMarkerPosition = Vector3.zero;
            newMarkerPosition.x = this.transform.position.x + (rb.velocity.x * avgJumpTime);
            jumpMarker.position = newMarkerPosition;
        }
    }
    #endregion

    #region Buffers
    IEnumerator jumpCancelWait()
    {
        canJumpCancel = false;
        yield return new WaitForSeconds(jumpCancelWaitTime);
        canJumpCancel = true;
        if (jumpCancelQueued)       // If a jumpCancel was queued, try perform it now
            JumpEnd();
    }

    IEnumerator jumpQueuedWait()
    {
        yield return new WaitForSeconds(jumpQueuedTimeLimit);
        jumpQueued = false;
    }

    IEnumerator coyoteTimeWait()
    {
        inCoyoteTime = true;
        //TEMP Debug.Log("coyote start");
        yield return new WaitForSeconds(coyoteTime);
        inCoyoteTime = false;
        //TEMP Debug.Log("coyote end");
    }
    #endregion

    public void SetControllable(bool toSet)
    {
        if (toSet)
        {
            controls.Main.Enable();
        }
        else
        {
            controls.Main.Disable();
        }
        controlEnabled = toSet;
    }

    public bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDistance, groundcheckLayermask);
        if (hit.collider != null)
        {
            if (hit.collider.tag.EndsWith("Platform"))
                return true;
        }

        return false;
    }

    public void Reset()
    {
        Debug.Log("Character reset");
    }

    public float GetMaxPlayerSpeed()
    {
        return sideMoveMax;
    }

    /*// Jump Timer START
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.EndsWith("Platform"))
        {
            Debug.Log("hit ground, time: " + jumpTimer);
            timeJump = false;
            totalJumpTime += jumpTimer;
            Debug.Log("average time: " + (totalJumpTime/jumpsPerformed));
            jumpTimer = 0f;
        }
    }
    // Jump Timer END */

    //TEMP
    IEnumerator SlowlyIncreaseMaxSpeed()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1f);
            sideMoveMax *= 1.18f;
            Debug.Log(rb.velocity.x + " : " + sideMoveMax);
        }
    }
}

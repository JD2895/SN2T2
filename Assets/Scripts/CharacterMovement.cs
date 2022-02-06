using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Controls controls;
    Vector3 newVelocity;
    bool controllable = true;

    [Header("Side Movement")]
    public float sideMoveAcceleration;
    public float sideMoveMax;
    public float noInputFrictionMultiplier;
    MoveDir toMoveDir = MoveDir.None;

    [Header("Jump")]
    public float jumpForce;
    public float jumpBrakeMultiplier;
    public float minimumVelocityForJumpBrake;

    private void Awake()
    {
        controls = new Controls();

        if (controllable)
        {
            controls.Movement.Left.performed += _ => MoveLeftStart();
            controls.Movement.Right.performed += _ => MoveRightStart();
            controls.Movement.Left.canceled += _ => MoveLeftEnd();
            controls.Movement.Right.canceled += _ => MoveRightEnd();
            controls.Movement.Jump.performed += _ => JumpStart();
            controls.Movement.Jump.canceled += _ => JumpEnd();
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

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (toMoveDir != MoveDir.None)
        {
            newVelocity = rb.velocity;

            if (toMoveDir == MoveDir.Left)
            {
                if (newVelocity.x > (sideMoveMax * -1f))
                    newVelocity.x -= sideMoveAcceleration * Time.fixedDeltaTime;
                else
                    newVelocity.x += sideMoveAcceleration * Time.fixedDeltaTime;
            }
            else if (toMoveDir == MoveDir.Right)
            {
                if (newVelocity.x < (sideMoveMax * 1f))
                    newVelocity.x += sideMoveAcceleration * Time.fixedDeltaTime;
                else
                    newVelocity.x -= sideMoveAcceleration * Time.fixedDeltaTime;
            }

            rb.velocity = newVelocity;
        }
        else
        {
            newVelocity = rb.velocity;

            if (Mathf.Abs(newVelocity.x) < 0.3)
                newVelocity.x = 0;
            else
                newVelocity.x /= noInputFrictionMultiplier;

            rb.velocity = newVelocity;
        }
    }

    #region Side movement control
    public void MoveLeftStart()
    {
        toMoveDir = MoveDir.Left;
    }

    public void MoveRightStart()
    {
        toMoveDir = MoveDir.Right;
    }

    public void MoveLeftEnd()
    {
        if (toMoveDir == MoveDir.Left)
            toMoveDir = MoveDir.None;
    }

    public void MoveRightEnd()
    {
        if (toMoveDir == MoveDir.Right)
            toMoveDir = MoveDir.None;
    }
    #endregion

    #region Jump control
    private void JumpStart()
    {
        if (controllable)
            ApplyJumpForce();
    }

    private void JumpEnd()
    {
        if (controllable)
            if (rb != null)
            {
                if (rb.velocity.y > minimumVelocityForJumpBrake)
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / jumpBrakeMultiplier);
            }
    }

    public void ApplyJumpForce()
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
        }
    }
    #endregion

    public void SetControllable(bool toSet)
    {
        if (toSet)
        {
            controls.Movement.Enable();
        }
        else
        {
            controls.Movement.Disable();
        }
        toMoveDir = MoveDir.None;
        controllable = toSet;
    }

    public enum MoveDir
    {
        Left,
        None,
        Right
    }
}

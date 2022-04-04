using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    Controls controls;
    Vector3 newVelocity;
    bool controlEnabled = true;

    [Header("Side Movement")]
    float sideMoveAcceleration = 30f;
    public float sideMoveMax = 4f;
    float noInputFrictionMultiplier = 1.3f;
    MoveDir toMoveDir = MoveDir.None;

    [Header("Jump")]
    float jumpForce = 9.5f;
    float jumpBrakeMultiplier = 2f;
    float minimumVelocityForJumpBrake = 1f;

    private void Awake()
    {
        controls = new Controls();

        if (controlEnabled)
        {
            controls.Movement.Left.performed += _ => MoveHorizontalStart(MoveDir.Left);
            controls.Movement.Left.canceled += _ => MoveHorizontalEnd(MoveDir.Left);
            controls.Movement.Right.performed += _ => MoveHorizontalStart(MoveDir.Right);
            controls.Movement.Right.canceled += _ => MoveHorizontalEnd(MoveDir.Right);
            controls.Movement.Down.performed += _ => DownStart();
            controls.Movement.Down.canceled += _ => DownEnd();
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
    public void MoveHorizontalStart(MoveDir dirToMove)
    {
        toMoveDir = dirToMove;
    }

    public void MoveHorizontalEnd(MoveDir dirToMove)
    {
        if (toMoveDir == dirToMove)
            toMoveDir = MoveDir.None;
    }
    #endregion

    #region Down logic
    public void DownStart()
    {
        Debug.Log("Down Start");
        return;
    }

    public void DownEnd()
    {
        Debug.Log("Down End");
        return;
    }
    #endregion

    #region Jump control
    public void JumpStart()
    {
        if (rb != null)
            ApplyJumpForce();
    }

    public void JumpEnd()
    {
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
        controlEnabled = toSet;
    }

    public void Reset()
    {
        toMoveDir = MoveDir.None;
    }
}

public enum MoveDir
{
    Left,
    None,
    Right
}

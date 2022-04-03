using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAnimator : MonoBehaviour
{
    public Animator characterAnim;
    public GameObject spriteObject;
    public ContactFilter2D contactFilter;
    public float directionChangeThresh = 0.1f;
    public float standToRunThresh = 0.01f;
    public float standToJumpThresh = 0.1f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float yVel = rb.velocity.y;
        float xVel = rb.velocity.x;

        if (xVel > directionChangeThresh)
            FaceRight(true);
        else if (xVel < -directionChangeThresh)
            FaceRight(false);

        if (yVel > standToJumpThresh)
        {
            characterAnim.SetTrigger("jump");
        }
        else
        {
            if(GroundCheck())
            {
                if (Mathf.Pow(xVel, 2) > Mathf.Pow(standToRunThresh, 2))
                    characterAnim.SetTrigger("run");
                else
                    characterAnim.SetTrigger("side");
            }

        }
    }

    bool GroundCheck()
    {
        RaycastHit2D[] hitresults = new RaycastHit2D[3];
        int numhit = rb.Cast(Vector2.down, hitresults, 0.1f);
        if (numhit > 0)
        {
            return true;
        }
        return false;
    }

    void FaceRight(bool facingRight)
    {
        bool currentlyFacingRight = spriteObject.transform.localScale.x == 1 ? true : false;
        if (facingRight != currentlyFacingRight)
        {
            Vector3 newScale = spriteObject.transform.localScale;
            newScale.x *= -1;
            spriteObject.transform.localScale = newScale;
        }
    }
}

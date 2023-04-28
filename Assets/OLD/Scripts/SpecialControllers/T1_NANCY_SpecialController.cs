using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1_NANCY_SpecialController : SpecialControllerBase
{
    // Q > Flip Sprite
    // W > Side
    // E > Run
    // R > Jump
    // T > Press
    // Y > Hold
    // U > Battle

    // A > Gun Idle
    // S > Gun Run
    // D > Gun Jump
    // F > 
    // G > 
    // H > 
    // J > 

    // Z > Disable mouth
    // X > Mouth Closed 1
    // C > Mouth Open 1
    // V > Mouth Closed 2
    // B > Mouth Open 2
    // N > Mouth Closed 3
    // M > Mouth Open 3

    public Animator characterAnim;
    public GameObject spriteObject;
    public GameObject[] mouths;

    #region *** Group ONE ***
    public override void Q_start()
    {
        Vector3 newScale = spriteObject.transform.localScale;
        newScale.x *= -1;
        spriteObject.transform.localScale = newScale;
    }

    public override void W_start()
    {
        characterAnim.SetTrigger("side");
    }

    public override void E_start()
    {
        characterAnim.SetTrigger("run");
    }

    public override void R_start()
    {
        characterAnim.SetTrigger("jump");
    }

    public override void T_start()
    {
        characterAnim.SetTrigger("press");
    }

    public override void Y_start()
    {
        characterAnim.SetTrigger("hold");
    }

    public override void U_start()
    {
        characterAnim.SetTrigger("battle");
    }
    #endregion

    #region *** Group TWO ***
    public override void A_start()
    {
        characterAnim.SetTrigger("gun_idle");
    }

    public override void S_start()
    {
        characterAnim.SetTrigger("gun_run");
    }

    public override void D_start()
    {
        characterAnim.SetTrigger("gun_jump");
    }
    #endregion

    #region *** Group THREE ***
    public override void Z_start()
    {
        EnableOnlyThisMouth(6);
    }

    public override void X_start()
    {
        EnableOnlyThisMouth(0);
    }

    public override void C_start()
    {
        EnableOnlyThisMouth(1);
    }

    public override void V_start()
    {
        EnableOnlyThisMouth(2);
    }
    public override void B_start()
    {
        EnableOnlyThisMouth(3);
    }

    public override void N_start()
    {
        EnableOnlyThisMouth(4);
    }

    public override void M_start()
    {
        EnableOnlyThisMouth(5);
    }
    #endregion

    #region *** Helpers ***
    void EnableOnlyThisMouth(int toEnable)
    {
        for (int i = 0; i < mouths.Length; i++)
        {
            if (i == toEnable)
                mouths[i].SetActive(true);
            else
                mouths[i].SetActive(false);
        }
    }

    public override void FaceForward()
    {
        Vector3 newScale = spriteObject.transform.localScale;
        if (newScale.x < 0)
            newScale.x *= -1;
        spriteObject.transform.localScale = newScale;
    }
    #endregion
}

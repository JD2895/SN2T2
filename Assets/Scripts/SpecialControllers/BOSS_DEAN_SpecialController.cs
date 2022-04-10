using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_DEAN_SpecialController : SpecialControllerBase
{
    // Q > Idle
    // W > Unhinge
    // E > Charge
    // R > Mouth Close
    // T > Dead Idle
    // Y > Toggle Title
    // U > Fire Laser

    // A > 
    // S > 
    // D > 
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
    public GameObject[] mouths;
    public GameObject title;

    public GameObject laserObject;

    #region *** Group ONE ***
    public override void Q_start()
    {
        characterAnim.SetTrigger("idle");
    }

    public override void W_start()
    {
        characterAnim.SetTrigger("unhinge");
    }

    public override void E_start()
    {
        characterAnim.SetTrigger("charge");
    }

    public override void R_start()
    {
        characterAnim.SetTrigger("mouth_close");
    }

    public override void T_start()
    {
        characterAnim.SetTrigger("dead_idle");
    }

    public override void Y_start()
    {
        title.SetActive(!title.activeSelf);
    }

    public override void U_start()
    {
        laserObject.SetActive(!laserObject.activeSelf);
    }
    #endregion

    #region *** Group TWO ***
    public override void A_start()
    {
    }

    public override void S_start()
    {
    }

    public override void D_start()
    {
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

    public override void Reset()
    {
    }
    #endregion
}

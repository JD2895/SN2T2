using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIE_SpecialController : SpecialControllerBase
{
    // Q > Idle
    // W > Idle Float
    // E > Idle Bounce
    // R > Idle Bounce 2
    // T > Yeet
    // Y > 
    // U > 

    // A > Fire Missile
    // S > 
    // D > 
    // F > 
    // G > 
    // H > 
    // J > 

    // Z > 
    // X > 
    // C > 
    // V > 
    // B > 
    // N > 
    // M > 

    public Animator characterAnim;
    public ProjectileFirer projFirer;

    #region *** Group ONE ***
    public override void Q_start()
    {
        characterAnim.SetTrigger("idle");
    }

    public override void W_start()
    {
        characterAnim.SetTrigger("idle_float");
    }

    public override void E_start()
    {
        characterAnim.SetTrigger("idle_bounce");
    }

    public override void R_start()
    {
        characterAnim.SetTrigger("idle_bounce2");
    }

    public override void T_start()
    {
        characterAnim.SetTrigger("yeet");
    }

    public override void Y_start()
    {
    }

    public override void U_start()
    {
    }
    #endregion

    #region *** Group TWO ***
    public override void A_start()
    {
        projFirer.FireProjectile();
    }

    public override void S_start()
    {
    }

    public override void D_start()
    {
    }
    #endregion

    #region *** Group THREE ***
    #endregion

    #region *** Helpers ***

    public override void Reset()
    {
    }
    #endregion
}

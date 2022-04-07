using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D4_1_SpecialController : SpecialControllerBase
{
    // Q > Move Camera
    // W > 
    // E > 
    // R > 
    // T > 
    // Y > 
    // U > 

    // A > 
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

    Vector3 initialCameraPosition;
    public Animator cameraAnimator;

    private void Start()
    {
        initialCameraPosition = Camera.main.transform.position;
    }

    #region *** Group ONE ***
    public override void Q_start()
    {
        cameraAnimator.SetTrigger("panToRing");
    }

    public override void W_start()
    {
    }

    public override void E_start()
    {
    }

    public override void R_start()
    {
    }

    public override void T_start()
    {
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
    }

    public override void S_start()
    {
    }
    #endregion

    #region *** Group THREE ***
    public override void Z_start()
    {
    }

    public override void X_start()
    {
    }

    public override void C_start()
    {
    }

    public override void N_start()
    {
    }

    public override void M_start()
    {
    }
    #endregion

    #region *** Helpers ***
    public override void Reset()
    {
        Camera.main.transform.position = initialCameraPosition;
    }
    #endregion
}

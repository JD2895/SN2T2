using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2_1_SpecialController : SpecialControllerBase
{
    // Q > Spawn MSE's next item
    // W > Next ground crack level
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

    public ObjectSpawner objectSpawner;

    #region *** Group ONE ***
    public override void Q_start()
    {
        objectSpawner.SpawnNextItem();
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
    #endregion

    #region *** Group THREE ***
    #endregion

    #region *** Helpers ***
    #endregion
}

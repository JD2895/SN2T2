using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D3_1_SpecialController : SpecialControllerBase
{
    // Q > Change village hut
    // W > Potato text
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

    public SpriteRenderer backWithHut;
    public Sprite newHutSprite;

    public GameObject potatoesText;

    private void Start()
    {
    }

    #region *** Group ONE ***
    public override void Q_start()
    {
        backWithHut.sprite = newHutSprite;
    }

    public override void W_start()
    {
        potatoesText.SetActive(true);
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
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D3_1_SpecialController : SpecialControllerBase
{
    // Q > Change village hut
    // W > Potato text
    // E > Ladder bend
    // R > Pipe Descend
    // T > Toggle Plutonium
    // Y > Button Light Up
    // U > Disable button title

    // A > Teleport All Instant
    // S > Open Door
    // D > Teleport With Gap
    // F > Cue Spawn Beams
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

    public GameObject supportObject;
    public Animator pipeAnimator;

    public GameObject plutObject;
    public SpriteRenderer buttonSprite;
    public GameObject buttonTitle;

    public List<GameObject> whoToTeleport;
    public List<Transform> whereToTeleport;

    public Animator doorAnim;

    public GameObject telportBeam;

    private void Start()
    {
        potatoesText?.SetActive(false);
        supportObject?.SetActive(false);
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
        supportObject.SetActive(true);
    }

    public override void R_start()
    {
        pipeAnimator.SetTrigger("descend");
    }

    public override void T_start()
    {
        plutObject.SetActive(!plutObject.activeSelf);
    }

    public override void Y_start()
    {
        Color buttonBright = buttonSprite.color;
        buttonBright.r = buttonBright.g = buttonBright.b = 1f;
        buttonSprite.color = buttonBright;
        buttonTitle.SetActive(true);
    }

    public override void U_start()
    {
        buttonTitle.SetActive(false);
    }
    #endregion

    #region *** Group TWO ***
    public override void A_start()
    {
        for (int i = 0; i < whoToTeleport.Count; i++)
        {
            whoToTeleport[i].transform.position = whereToTeleport[i].position;
        }
    }

    public override void S_start()
    {
        doorAnim.SetTrigger("open");
    }

    public override void D_start()
    {
        StartCoroutine(SlowTeleport());
    }

    public override void F_start()
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

    IEnumerator SlowTeleport()
    {
        for (int i = 0; i < whoToTeleport.Count; i++)
        {
            Instantiate(telportBeam, whereToTeleport[i].position, Quaternion.identity);
            whoToTeleport[i].transform.position = whereToTeleport[i].position;
            yield return new WaitForSeconds(0.2f);
        }
    }
    #endregion
}

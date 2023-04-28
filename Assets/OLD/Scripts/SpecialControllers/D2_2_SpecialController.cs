using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2_2_SpecialController : SpecialControllerBase
{
    // Q > Lumber
    // W > Hammer Taken
    // E > Prehit
    // R > Hit 1
    // T > Hit 2
    // Y > Hit 3
    // U > Hit 4

    // A > Done
    // S > Disappear
    // D > 
    // F > 
    // G > 
    // H > 
    // J > 

    // Z > +1 Hammer
    // X > ECE Active Toggle
    // C > ECE Spawn
    // V > 
    // B > 
    // N > Boat Appears
    // M > Boat Kinematic

    public Animator lumberAnim;
    public GameObject plusHammerText;
    public GameObject ece;
    public GameObject boat;
    public Transform boatSpawnPoint;
    public Transform eceSpawnPoint;

    #region *** Group ONE ***
    public override void Q_start()
    {
        Debug.Log("here");
        lumberAnim.SetTrigger("lumber");
    }

    public override void W_start()
    {
        lumberAnim.SetTrigger("hammer_taken");
    }

    public override void E_start()
    {
        lumberAnim.SetTrigger("pre_hit");
    }

    public override void R_start()
    {
        lumberAnim.SetTrigger("hit1");
    }

    public override void T_start()
    {
        lumberAnim.SetTrigger("hit2");
    }

    public override void Y_start()
    {
        lumberAnim.SetTrigger("hit3");
    }

    public override void U_start()
    {
        lumberAnim.SetTrigger("hit4");
    }
    #endregion

    #region *** Group TWO ***
    public override void A_start()
    {
        lumberAnim.SetTrigger("done");
    }

    public override void S_start()
    {
        lumberAnim.SetTrigger("disappear");
    }
    #endregion

    #region *** Group THREE ***
    public override void Z_start()
    {
        plusHammerText.SetActive(true);
    }

    public override void X_start()
    {
        ece.SetActive(!ece.activeSelf);
    }

    public override void C_start()
    {
        ece.transform.position = eceSpawnPoint.position;
    }

    public override void N_start()
    {
        if (boat != null)
            boat.transform.position = boatSpawnPoint.position;
    }

    public override void M_start()
    {
        if (boat != null)
            boat.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
    #endregion

    #region *** Helpers ***
    public override void Reset()
    {
        Q_start();
        ece.SetActive(true);
        if (boat != null)
        {
            boat.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            boat.transform.position = new Vector3(-12.89f, 0.37f, 4.571f);
        }
    }
    #endregion
}

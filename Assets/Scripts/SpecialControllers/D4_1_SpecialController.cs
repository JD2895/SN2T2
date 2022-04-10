using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D4_1_SpecialController : SpecialControllerBase
{
    // Q > Move Camera
    // W > Move Camera 2
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

    // Z > Move Boss In
    // X > Move Tie In
    // C > 
    // V > 
    // B > 
    // N > 
    // M > 

    Vector3 initialCameraPosition;
    public Animator cameraAnimator;

    public GameObject bossObject;
    public Transform bossStartPos;
    public Transform bossEndPos;
    public GameObject tieObject;
    public Transform tieStartPos;
    public Transform tieEndPos;
    public float moveInTime;

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
        cameraAnimator.SetTrigger("panToBoss");
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
        //StartCoroutine(MoveInBoss());
        StartCoroutine(MoveInObject(bossObject, bossStartPos.position, bossEndPos.position));
    }

    public override void S_start()
    {
        StartCoroutine(MoveInObject(tieObject, tieStartPos.position, tieEndPos.position));
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

    IEnumerator MoveInBoss()
    {
        Vector3 startPos = bossStartPos.position;
        Vector3 endPos = bossEndPos.position;
        Vector3 newPos = new Vector3();
        float timePeriod = 0.01f;
        float lerpPeriod = timePeriod / moveInTime;
        float lerpValue = 0;

        bossObject.transform.position = startPos;

        for (float timePassed = 0f; timePassed < moveInTime; timePassed += timePeriod)
        {
            newPos = Vector3.Lerp(startPos, endPos, lerpValue);
            bossObject.transform.position = newPos;
            lerpValue += lerpPeriod;
            yield return new WaitForSeconds(timePeriod);
        }

        bossObject.transform.position = endPos;

        yield return null;
    }

    IEnumerator MoveInObject(GameObject movingObject, Vector3 startPos, Vector3 endPos)
    {
        Vector3 newPos = new Vector3();
        float timePeriod = 0.01f;
        float lerpPeriod = timePeriod / moveInTime;
        float lerpValue = 0;

        movingObject.transform.position = startPos;

        for (float timePassed = 0f; timePassed < moveInTime; timePassed += timePeriod)
        {
            newPos = Vector3.Lerp(startPos, endPos, lerpValue);
            movingObject.transform.position = newPos;
            lerpValue += lerpPeriod;
            yield return new WaitForSeconds(timePeriod);
        }

        movingObject.transform.position = endPos;

        yield return null;
    }
    #endregion
}

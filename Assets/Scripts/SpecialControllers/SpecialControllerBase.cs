using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialControllerBase : MonoBehaviour
{
    Controls controls;
    bool controlEnabled = true;

    private void Awake()
    {
        controls = new Controls();
        controls.Special.Q.performed += _ => Q_start();
        controls.Special.W.performed += _ => W_start();
        controls.Special.E.performed += _ => E_start();
        controls.Special.R.performed += _ => R_start();
        controls.Special.T.performed += _ => T_start();
        controls.Special.Y.performed += _ => Y_start();
        controls.Special.U.performed += _ => U_start();

        controls.Special.A.performed += _ => A_start();
        controls.Special.S.performed += _ => S_start();
        controls.Special.D.performed += _ => D_start();
        controls.Special.F.performed += _ => F_start();
        controls.Special.G.performed += _ => G_start();
        controls.Special.H.performed += _ => H_start();
        controls.Special.J.performed += _ => J_start();

        controls.Special.Z.performed += _ => Z_start();
        controls.Special.X.performed += _ => X_start();
        controls.Special.C.performed += _ => C_start();
        controls.Special.V.performed += _ => V_start();
        controls.Special.B.performed += _ => B_start();
        controls.Special.N.performed += _ => N_start();
        controls.Special.M.performed += _ => M_start();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public virtual void Q_start() { Debug.Log("q"); }
    public virtual void W_start() { Debug.Log("w"); }
    public virtual void E_start() { Debug.Log("e"); }
    public virtual void R_start() { Debug.Log("r"); }
    public virtual void T_start() { Debug.Log("t"); }
    public virtual void Y_start() { Debug.Log("y"); }
    public virtual void U_start() { Debug.Log("u"); }

    public virtual void A_start() { Debug.Log("a"); }
    public virtual void S_start() { Debug.Log("s"); }
    public virtual void D_start() { Debug.Log("d"); }
    public virtual void F_start() { Debug.Log("f"); }
    public virtual void G_start() { Debug.Log("g"); }
    public virtual void H_start() { Debug.Log("h"); }
    public virtual void J_start() { Debug.Log("j"); }

    public virtual void Z_start() { Debug.Log("z"); }
    public virtual void X_start() { Debug.Log("x"); }
    public virtual void C_start() { Debug.Log("c"); }
    public virtual void V_start() { Debug.Log("v"); }
    public virtual void B_start() { Debug.Log("b"); }
    public virtual void N_start() { Debug.Log("n"); }
    public virtual void M_start() { Debug.Log("m"); }

    public virtual void Reset()
    {
        W_start();
        X_start();
    }

    public virtual void SetControllable(bool toSet)
    {
        if (toSet)
        {
            controls.Special.Enable();
        }
        else
        {
            controls.Special.Disable();
        }
        controlEnabled = toSet;
    }
}

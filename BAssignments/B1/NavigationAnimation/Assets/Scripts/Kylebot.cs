using UnityEngine;
using System.Collections;

public class Kylebot : MonoBehaviour {

    private Animator anim;
    private AnimatorStateInfo currBaseState;
    private bool jumper = false;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int wfState = Animator.StringToHash("Base Layer.WalkFwd");
    static int rfState = Animator.StringToHash("Base Layer.RunFwd");
    static int wbState = Animator.StringToHash("Base Layer.WalkBack");
    static int rbState = Animator.StringToHash("Base Layer.RunBack");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", moveV);
        anim.SetFloat("Direction", moveH);
    
        currBaseState = anim.GetCurrentAnimatorStateInfo(0);
        Debug.Log(anim.GetFloat("Speed"));
        //walk and run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currBaseState.fullPathHash == wfState || currBaseState.fullPathHash == wbState)
                anim.SetBool("Run", true);
            if (currBaseState.fullPathHash == rfState || currBaseState.fullPathHash == rbState)
                anim.SetBool("Run", false);
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && !jumper)
        {
            if (currBaseState.fullPathHash != jumpState)
                anim.SetBool("Jump", true);
            jumper = true;
        }
        if(currBaseState.fullPathHash == jumpState)
        {
            //if jumped, reset bool to jump again
            if (!anim.IsInTransition(0))
                anim.SetBool("Jump", false);
            jumper = false;
        }
    }
}


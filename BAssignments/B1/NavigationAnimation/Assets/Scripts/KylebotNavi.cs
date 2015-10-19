using UnityEngine;
using System.Collections;

public class KylebotNavi : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator anim;

    AnimatorStateInfo currBaseState;
    static int jumpState = Animator.StringToHash("Base Layer.Jump");

    bool selected = false;
    bool ready = false;
    bool run = false;
    Vector3 destination;


    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        currBaseState = anim.GetCurrentAnimatorStateInfo(0);
        if (selected && ready)
        {
            ready = false;
            agent.SetDestination(destination);
            if (run) anim.SetBool("Run", true);
            else anim.SetBool("Run", false);
            anim.SetFloat("Speed", 1);
            run = false;
        }
        if (Vector3.Distance(transform.position, destination) <= 1.0f)
        {
            anim.SetFloat("Speed", 0);
            run = false;
            anim.SetBool("Run", false);
        }
        if (agent.isOnOffMeshLink)
        {
            anim.SetBool("Jump", true);
        }
        if (currBaseState.fullPathHash == jumpState)
        {
            //if jumped, reset bool to jump again
            if (!anim.IsInTransition(0))
                anim.SetBool("Jump", false);
        }
    }

    void RunDude(int x)
    {
        run = true;
    }
    void Select(int x)
    {
        selected = true;
    }
    void Deselect(int x)
    {
        selected = false;
    }
    bool SelCheck(int x)
    {
        return (selected);
    }
    void Destination(Vector3 d)
    {
        destination = d;
        ready = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class samePos : StateMachineBehaviour
{

    private UnityEngine.Vector3 rot;
    private UnityEngine.Vector3 rot1;

    //private Vector3 pos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rot = animator.transform.rotation.eulerAngles;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //Debug.Log("hipssssss " + g.transform.position);
    //    //Debug.Log("hipssssss " + animator.transform.GetChild(5).position);
    //    //animator.transform.position += new Vector3(0f, 0f, 1f )*Time.deltaTime;


    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(animator.transform.parent.name + " " + animator.transform.parent.position);
        //Debug.Log(animator.transform.name + " " + animator.transform.position);
        //Debug.Log(g.transform.position);
        animator.transform.eulerAngles = rot;

       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

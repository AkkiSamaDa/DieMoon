using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HeroController hero = animator.GetComponent<HeroController>();
        hero.StopMove();
        hero.isReloading = true;
        hero.isShooting = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HeroController hero = animator.GetComponent<HeroController>();
        hero.StopMove();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HeroController hero = animator.GetComponent<HeroController>();
        hero.RecoverMove();
        
        hero.isReloading = false;
        hero.transform.position = new Vector3(hero.transform.position.x + hero.forward.x * 0.31f, hero.transform.position.y, hero.transform.position.z);
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

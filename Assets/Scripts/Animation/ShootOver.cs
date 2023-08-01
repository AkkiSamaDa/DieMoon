using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootOver : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("状态进入");

        HeroController hero = animator.GetComponent<HeroController>();
        animator.SetBool("shootKeyDown", false);
        hero.shootKeyDown = false;
        hero.isFiring = true;
        hero.StopMove();
        Debug.Log("开火，子弹减1");
        hero.bulletCount--;
        if (hero.bulletCount < 0)
        {
            hero.bulletCount = 0;
        }
        EventCenter.Instance.EventTrigger("HeroShoot");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HeroController hero = animator.GetComponent<HeroController>();
        //射击后座力修正
        hero.ShootFixed();
        hero.isFiring = false;
        hero.RecoverMove();
        Debug.Log("状态退出"+ stateInfo.fullPathHash);
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

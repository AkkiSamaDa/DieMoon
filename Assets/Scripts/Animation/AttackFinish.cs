using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFinish : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HeroController controller = animator.GetComponent<HeroController>();
        controller.StopMove();
        controller.isAttacking = true;
        controller.UpdateTurn();
        if (!stateInfo.IsName("Attack1_already"))
        {
            controller.combos++;
            controller.AddAttackForce();
            if (controller.combos > 3)
            {
                controller.combos = 0;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DemageCheck demageCheck = animator.GetComponent<DemageCheck>();
        demageCheck.lockInvincible = false;
        HeroController controller = animator.GetComponent<HeroController>();
        controller.isAttacking = false;
        controller.RecoverMove();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimBehaviour : StateMachineBehaviour
{
    AudioSource footStepSource;
    HeroController heroController;
    SurfaceReader surfaceReader;
    WalkVFX walkVFX;
    SurfaceType lastSurface = SurfaceType.None;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        heroController = animator.GetComponent<HeroController>();
        surfaceReader = animator.GetComponent<SurfaceReader>();
        walkVFX = animator.GetComponent<WalkVFX>();
        walkVFX.ActiveVFX();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (heroController.isDead)
        {
            MusicMgr.Instance.StopSound(footStepSource);
            walkVFX.DeactiveVFX();
            return;
        }
        if (!animator.GetComponent<PhysicsCheck2D>().isOnGround() || !heroController.canMove)
        {
            MusicMgr.Instance.StopSound(footStepSource);
            footStepSource = null;
            walkVFX.DeactiveVFX();
        }
        else
        {
            walkVFX.ActiveVFX();
             if (footStepSource == null)
            {
                switch (surfaceReader.currentSurface)
                {
                    case SurfaceType.Grass:

                        Debug.Log("²¥·Å½Å²½Éù");
                        MusicMgr.Instance.PlaySound("hero_run_footsteps_stone", true, (source) =>
                        {
                            footStepSource = source;
                        },true);

                        break;
                    case SurfaceType.Iron:

                        MusicMgr.Instance.PlaySound("footStepOnIron", true, (source) =>
                        {
                            footStepSource = source;
                        });
                        break;
                }

                //lastSurface = surfaceReader.currentSurface;
            }else
            {
                if (lastSurface == SurfaceType.None)
                {
                    lastSurface = surfaceReader.currentSurface;
                }
                switch (surfaceReader.currentSurface)
                {
                    case SurfaceType.Grass:
                        if (lastSurface != surfaceReader.currentSurface)
                        {
                            MusicMgr.Instance.ChangeSoundClip("hero_run_footsteps_stone", true, footStepSource);
                        }
                        break;
                    case SurfaceType.Iron:
                        if (lastSurface != surfaceReader.currentSurface)
                        {
                            MusicMgr.Instance.ChangeSoundClip("footStepOnIron", true, footStepSource);
                        }
                        break;
                }
                lastSurface = surfaceReader.currentSurface;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walkVFX.DeactiveVFX();
        MusicMgr.Instance.StopSound(footStepSource);
        footStepSource = null;
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

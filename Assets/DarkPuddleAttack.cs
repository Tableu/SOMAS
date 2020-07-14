using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPuddleAttack : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    public Vector3 spawnDisplacement;
    public float projectileSpeed;
    private GameObject projectile;
    private GameObject player;
    public Vector2 direction;
    private bool projectileInstantiated;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player"); 
        projectileInstantiated = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float animationProgress = Mathf.Round((stateInfo.normalizedTime % 1)*100f);
        if((animationProgress > 85f) && !projectileInstantiated){
            projectile = Instantiate(projectilePrefab, animator.GetComponent<Transform>().position + spawnDisplacement,Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = projectileSpeed*direction;   
            if(direction.x > 0){
                projectile.transform.rotation = Quaternion.Euler(0,0,180);
            }
            projectileInstantiated = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

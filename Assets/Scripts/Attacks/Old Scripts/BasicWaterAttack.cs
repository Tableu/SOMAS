using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWaterAttack : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    private GameObject projectile;
    private GameObject player;
    public float speed;
    public float rotationSpeed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player"); 
        waterAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
    private void waterAttack(){
        projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        projectile.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed);
    }
}

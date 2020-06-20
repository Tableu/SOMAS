using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    private GameObject projectile;
    private GameObject player;
    public float speed;
    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player"); 
        flamethrowerAttack();
        timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        timer += Time.deltaTime;
        if(timer > 1f){
            Destroy(projectile);
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
    private void flamethrowerAttack(){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        projectile = Instantiate(projectilePrefab, player.transform.position + new Vector3(7.5f*playerForward.x,0.2f,0), Quaternion.identity);
        projectile.transform.parent = player.transform;
        projectile.transform.rotation = Quaternion.Euler(0,0,80*playerForward.x);
    }
}

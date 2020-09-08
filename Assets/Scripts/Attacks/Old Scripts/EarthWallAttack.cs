using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallAttack : StateMachineBehaviour
{
    public GameObject earthWallPrefab;
    private GameObject earthWall;
    private GameObject player;
    public float earthWallPosition;
    public float raycastLength;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");  
        if(player.GetComponent<PlayerData>().grounded){
            createEarthWall(animator);  
        }else{
            animator.SetTrigger("ConditionFailed");
        }
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
    private void createEarthWall(Animator animator){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        Vector2 spawnPosition = player.transform.position + playerForward*earthWallPosition;
        
        RaycastHit2D rayHit = Physics2D.Raycast(spawnPosition, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(spawnPosition, Vector2.down*raycastLength,Color.red);
        if(rayHit){
            earthWall = Instantiate(earthWallPrefab, spawnPosition, Quaternion.identity);
        }else{
            animator.SetTrigger("ConditionFailed");
        }
    }
}

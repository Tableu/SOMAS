using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeAttack : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    private GameObject[] projectile = new GameObject[3];
    private GameObject player;
    public float speed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        iceAttack(); 
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
    private void iceAttack(){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        Vector3 playerPos = player.GetComponent<PlayerInput>().playerPos;
        Vector3[] spawnPos = new Vector3[3];
        int angle = 0;
        spawnPos[0] = player.transform.position + playerForward*-1f*2 + new Vector3(0,1,0);
        spawnPos[1] = player.transform.position + playerForward*-1f*4;
        spawnPos[2] = player.transform.position + playerForward*-1f*2 + new Vector3(0,-1,0);
        
        projectile[0] = Instantiate(projectilePrefab, spawnPos[0], Quaternion.identity);
        projectile[1] = Instantiate(projectilePrefab, spawnPos[1], Quaternion.identity);
        projectile[2] = Instantiate(projectilePrefab, spawnPos[2], Quaternion.identity);

        if(playerForward.x < 0){
            angle = 180;
        }
        projectile[0].transform.rotation = Quaternion.Euler(0,0,angle);
        projectile[1].transform.rotation = Quaternion.Euler(0,0,angle);
        projectile[2].transform.rotation = Quaternion.Euler(0,0,angle);

        projectile[0].GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        projectile[1].GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        projectile[2].GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private EnemyData enemyData;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        enemyData = GetComponent<EnemyData>();
        col = GetComponent<Collider2D>();
        player.GetComponent<PlayerHealth>().deathEvent += OnDeathEvent;
    }
    
    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }
    private void detectPlayer(){
        enemyData.distanceToPlayer = (player.transform.position - transform.position);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if(!enemyData.playerFound && enemyData.grounded){
            if(!stateInfo.IsName("Attack")){
                if(checkRaycastsForTag(enemyData.raycastArray, "Player")){
                    animator.SetTrigger("Attack");
                    enemyData.playerFound = true;
                }
            }
        }else{
            float magnitude = enemyData.distanceToPlayer.magnitude;
            animator.SetFloat("AttackLimit", magnitude - enemyData.attackLimit);
            animator.SetFloat("FollowLimit", magnitude - enemyData.followLimit);
            
            if(stateInfo.IsName("Follow") && magnitude > enemyData.followLimit){
                enemyData.playerFound = false;
            }
        }
    }
    private bool checkRaycastsForTag(RaycastHit2D[] raycastHits, string tag){
        for(int index = 0; index < raycastHits.Length; index++){
            if(raycastHits[index]){
                if(raycastHits[index].transform.gameObject.tag == tag){
                    return true;
                }
            }
        }
        return false;
    }
    void OnDeathEvent(){
        animator.SetFloat("AttackLimit", 1);
        animator.SetFloat("FollowLimit", 1);
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().deathEvent -= OnDeathEvent;
        GetComponent<EnemyDetection>().enabled = false;
    }
    void OnEnable(){
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().deathEvent += OnDeathEvent;
    }
}

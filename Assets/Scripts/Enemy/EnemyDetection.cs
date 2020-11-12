using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private EnemyRaycasts enemyRaycasts;
    public bool playerFound;
    private Collider2D col;
    public Vector2 distanceToPlayer;

    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        enemyRaycasts = GetComponent<EnemyRaycasts>();
        col = GetComponent<Collider2D>();
        player.GetComponent<PlayerHealth>().DeathEvent += OnDeathEvent;
        playerFound = false;
    }
    
    // Update is called once per frame
    private void Update()
    {
        DetectPlayer();
    }
    private void DetectPlayer(){
        distanceToPlayer = (player.transform.position - transform.position);
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if(!playerFound && enemyRaycasts.grounded){
            if(!stateInfo.IsName("Attack")){
                if(CheckRaycastsForTag(enemyRaycasts.raycastArray, "Player")){
                    animator.SetTrigger("Attack");
                    playerFound = true;
                    animator.SetBool("PlayerFound",playerFound);
                }
            }
        }else{
            var magnitude = distanceToPlayer.magnitude;
            animator.SetFloat("AttackLimit", magnitude - enemyRaycasts.attackLimit);
            animator.SetFloat("FollowLimit", magnitude - enemyRaycasts.followLimit);
            
            if(stateInfo.IsName("Follow") && magnitude > enemyRaycasts.followLimit){
                playerFound = false;
                animator.SetBool("PlayerFound",playerFound);
            }
        }
    }
    private bool CheckRaycastsForTag(RaycastHit2D[] raycastHits, string tag){
        for(var index = 0; index < raycastHits.Length; index++){
            if(raycastHits[index]){
                if(raycastHits[index].transform.gameObject.CompareTag(tag)){
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDeathEvent(){
        animator.SetFloat("AttackLimit", 1);
        animator.SetFloat("FollowLimit", 1);
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().DeathEvent -= OnDeathEvent;
        GetComponent<EnemyDetection>().enabled = false;
    }

    private void OnEnable(){
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().DeathEvent += OnDeathEvent;
    }
}

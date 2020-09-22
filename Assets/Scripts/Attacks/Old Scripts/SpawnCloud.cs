using UnityEngine;

public class SpawnCloud : StateMachineBehaviour
{
    public GameObject lightningPrefab;
    public GameObject cloudPrefab;
    public float cloudRange;
    private GameObject cloud;
    private GameObject lightning;
    private GameObject player;
    private int timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        player = GameObject.FindWithTag("Player");
        Spawn();    
        timer = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer == 10){
            CloudLightning();
        }
        if(timer == 30){
            Destroy(cloud);
        }
        if(timer == 35){
            Destroy(lightning);
            player.GetComponent<PlayerMovement>().enabled = true; 
            animator.SetBool("Lightning", true);
        }
        timer++;
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
    private void Spawn(){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        cloud = Instantiate(cloudPrefab, new Vector3(player.transform.position.x, player.transform.position.y+10, 0), Quaternion.identity);
    }
    private void CloudLightning(){
        RaycastHit2D hit = Physics2D.Raycast(cloud.transform.position, Vector2.down, cloudRange, LayerMask.GetMask("Platforms"));
        if(hit){
            lightning = Instantiate(lightningPrefab, cloud.transform.position, Quaternion.identity);
            Stretch(lightning, cloud.transform.position, new Vector3(cloud.transform.position.x,hit.collider.bounds.max.y,0));
        }
    }
    public void Stretch(GameObject sprite,Vector3 initialPosition, Vector3 finalPosition) {
         sprite.transform.position = initialPosition;
         
         Vector3 scale = new Vector3(1,1,1);
         scale.y = Vector3.Distance(initialPosition, finalPosition)/10;
         sprite.transform.localScale = scale;
     }
}

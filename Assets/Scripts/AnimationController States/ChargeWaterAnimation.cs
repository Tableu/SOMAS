using UnityEngine;

public class ChargeWaterAnimation : StateMachineBehaviour
{
    public GameObject chargingAnimationPrefab;
    private GameObject chargingAnimation;
    public Vector3 displacement;
    private GameObject player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player"); 
        displacement = new Vector3(displacement.x*player.GetComponent<PlayerData>().forward.x,0,0);
        chargingAnimation = Instantiate(chargingAnimationPrefab, player.transform.position+displacement, Quaternion.identity);
        chargingAnimation.transform.parent = player.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(chargingAnimation);
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

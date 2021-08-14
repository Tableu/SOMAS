using UnityEngine;

namespace Attacks {
    public class FireballAttack : StateMachineBehaviour
    {
        public GameObject fireballPrefab;
        public float spawnDistance;
        public float fireballSpeed;
        private GameObject fireball;
        private GameObject player;
        private Vector2 spawnPos;
        private bool fireballInstantiated;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameObject.FindWithTag("Player");
            fireballInstantiated = false;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
            var animationProgress = Mathf.Round((stateInfo.normalizedTime % 1)*100f);
        
            if(animationProgress > 35f && animationProgress < 50f && !fireballInstantiated){ //Spawn unmoving fireball above cultist
                var transform = animator.transform;
                Vector2 userPos = transform.position;
                Vector2 userDirection = transform.forward;
                //Quaternion userRotation = transform.rotation;
                spawnPos = userPos + (Vector2.up+userDirection)*spawnDistance;
                fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
                fireballInstantiated = true;
            }else if(animationProgress > 50f && animationProgress < 100f && fireballInstantiated){ //Shoot fireball at player location with the appropriate velocity and rotation.
                if(fireball == null){
                    fireballInstantiated = false;
                    return;
                }
                Vector2 playerPos = player.transform.position;
                var relativePos = ((playerPos - spawnPos).normalized);
            
                fireball.GetComponent<Rigidbody2D>().velocity = relativePos*fireballSpeed;
                var angle = Mathf.Atan2(relativePos.y, relativePos.x*-1)*Mathf.Rad2Deg;
                fireball.transform.rotation = Quaternion.AngleAxis(-angle, new Vector3(0,0,1));
                fireballInstantiated = false;
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
}

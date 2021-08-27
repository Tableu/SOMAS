using Projectiles;
using UnityEngine;

namespace Enemy {
    public class EnemyHealth : MonoBehaviour
    {
        public int healthPoints;
        private Animator animator;
        private Collider2D col;
        // Start is called before the first frame update
        private void Start()
        {
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if(healthPoints <= 0){
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision){
            if(collision.gameObject.CompareTag("PlayerProjectile")){
                healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
                var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if(!stateInfo.IsName("Attack")){
                    animator.SetTrigger("Attack");
                    GetComponent<EnemyDetection>().playerFound = true;
                    animator.SetBool("PlayerFound",true);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("PlayerProjectile")){
                healthPoints -= other.gameObject.GetComponent<Projectile>().damagePoints;
                var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if(!stateInfo.IsName("Attack")){
                    animator.SetTrigger("Attack");
                    GetComponent<EnemyDetection>().playerFound = true;
                    animator.SetBool("PlayerFound",true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision){
        
        }
    }
}

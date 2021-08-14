using Player;
using UnityEngine;

namespace Attacks {
    public class MeleeAttack : MonoBehaviour
    {
        public int damagePoints;
        private Collider2D col;
        // Start is called before the first frame update
        private void Start()
        {
            col = GetComponent<Collider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D collision){
            if(collision.gameObject.CompareTag("Player")){
                collision.gameObject.GetComponent<PlayerHealth>().healthPoints -= damagePoints;
            }
        }

        private void OnCollisionExit2D(Collision2D collision){
        
        }
    }
}

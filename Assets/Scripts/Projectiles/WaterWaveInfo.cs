using UnityEngine;

namespace Projectiles {
    public class WaterWaveInfo : Projectile {
        public Vector2 direction;
        public GameObject water;
        public Vector2 displacement;

        private Rigidbody2D rigidBody;
        // Start is called before the first frame update
        private void Start() {
            lifetime = 0;
            rigidBody = GetComponent<Rigidbody2D>();
        }
    
        // Update is called once per frame
        private void Update() {
            var raycastPos = new Vector2(gameObject.transform.position.x + rigidBody.velocity.normalized.x*5.65f,gameObject.transform.position.y - 2.27f);
            Debug.DrawRay(raycastPos, transform.TransformDirection(direction)*7,Color.red);
            if(Physics2D.Raycast(raycastPos,direction,7) == false){
                //Spread water
                Instantiate(water, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        private void FixedUpdate() {
            lifetime++;
            if (lifetime > 50) {
                //Death
                //Spread water
                Instantiate(water, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            //launch enemy back according to lifetime (stronger the smaller lifetime is)
            var enemyRigidbody = other.GetComponent<Rigidbody2D>();
            if (!other.CompareTag("Enemy"))
                return;
            var knockback = new Vector2((rigidBody.velocity.normalized.x)* (1000-((lifetime%20)*10)),1000);
            enemyRigidbody.AddForce(knockback);
        }

        private void OnCollisionExit2D(Collision2D other) {
        
        }
    }
}

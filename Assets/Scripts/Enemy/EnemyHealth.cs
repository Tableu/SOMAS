using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int healthPoints;
    private Collider2D col;
    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider2D>();
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        
    }
}

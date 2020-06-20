using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints;
    private Collider2D col;
    public delegate void deathEventDelegate();
    public event deathEventDelegate deathEvent;
    // Start is called before the first frame update
    void Start()
    {
        col = this.gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints <= 0){
            deathEvent.Invoke();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "EnemyProjectile"){
            healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        
    }
}

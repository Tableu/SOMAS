using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int healthPoints;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints <= 0){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "PlayerProjectile"){
            healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        
    }
}

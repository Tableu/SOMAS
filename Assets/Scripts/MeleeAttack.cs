using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damagePoints;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = this.gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<PlayerHealth>().healthPoints -= damagePoints;
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        
    }
}

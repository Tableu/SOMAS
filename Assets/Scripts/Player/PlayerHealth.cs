﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints;
    private Collider2D col;
    public delegate void deathEventDelegate();
    public event deathEventDelegate deathEvent; //Handles death of player, i.e stop camera, stop enemies
    private Animator animator;
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints <= 0){
            deathEvent.Invoke();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "EnemyProjectile"){
            healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
            float direction = collision.transform.GetComponent<Rigidbody2D>().velocity.normalized.x;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(playerData.knockback.x*direction,playerData.knockback.y),ForceMode2D.Impulse);
            StartCoroutine(Invulnerable());
            if(collision.gameObject.GetComponent<Projectile>().destructible){
                Destroy(collision.gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        
    }
    IEnumerator Invulnerable(){
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        GetComponent<Collider2D>().enabled = true;
    }
}

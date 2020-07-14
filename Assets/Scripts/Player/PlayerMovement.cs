using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;
    private Collider2D col;
    private bool isTouchingCol;
    private PlayerData playerData;
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        col = this.gameObject.GetComponent<Collider2D>();
        playerData = this.gameObject.GetComponent<PlayerData>();
        isTouchingCol = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        float horizontalVelocity = horizontal*playerData.speed;
        
        Jump();
        if(Input.GetButtonUp("Horizontal")){
            rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
        }
        if(Mathf.Abs(rigidBody.velocity.x) < playerData.maxSpeed){
            rigidBody.AddForce(new Vector2(horizontalVelocity, 0),ForceMode2D.Impulse);
        }
        if(!playerData.grounded && isTouchingCol && playerData.forwardRaycastHit){
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }
    private void Jump(){
        //If player is touching the ground
        if(playerData.grounded){
            if(Input.GetButtonDown("Jump")){
                rigidBody.AddRelativeForce(new Vector2(0,playerData.jumpVelocity), ForceMode2D.Impulse);
                Debug.Log("jump key pressed");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Platform"){
            isTouchingCol = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == "Platform"){
            isTouchingCol = false;
        }
    }
}

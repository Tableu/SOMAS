using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float[] horizontalLimits; 
    public float horizontalSpeed;
    public float verticalSpeed;
    public float verticalMaxSpeed;
    private Vector2 verticalVector;
    private Vector2 horizontalVector;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        verticalVector = Vector2.up;
        horizontalVector = Vector2.left;
        rigidBody.velocity = new Vector2(-horizontalSpeed, rigidBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x > horizontalLimits[1]){
            spriteRenderer.flipX = false;
            
            rigidBody.velocity = new Vector2(-horizontalSpeed, rigidBody.velocity.y);
        }else if(gameObject.transform.position.x < horizontalLimits[0]){
            spriteRenderer.flipX = true;
            
            rigidBody.velocity = new Vector2(horizontalSpeed, rigidBody.velocity.y);
        }
        
        if(rigidBody.velocity.y > verticalMaxSpeed){
            verticalVector = Vector2.down;
        }else if(rigidBody.velocity.y < -verticalMaxSpeed){
            verticalVector = Vector2.up;
        }
        
        rigidBody.AddRelativeForce(verticalSpeed*verticalVector);
        velocity = rigidBody.velocity.y;
    }
}

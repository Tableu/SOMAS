using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;
    private Collider2D col;
    private bool isTouchingCol;
    private PlayerData playerData;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerData = GetComponent<PlayerData>();
        isTouchingCol = false;
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        var horizontalVelocity = horizontal*playerData.speed;
        
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
                Debug.Log("Jump Key");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            isTouchingCol = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            isTouchingCol = false;
        }
    }
}

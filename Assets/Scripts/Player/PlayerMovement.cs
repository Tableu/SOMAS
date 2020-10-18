using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;
    private Collider2D col;
    private bool isTouchingCol;
    private PlayerData playerData;
    private PlayerInputActions playerInputActions;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerData = GetComponent<PlayerData>();
        isTouchingCol = false;
        playerInputActions = playerData.playerInputActions;
        playerInputActions.Player.Move.performed += Stop; //Subscribe to input system triggers
        playerInputActions.Player.Jump.started += Jump;
        
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontal = playerInputActions.Player.Move.ReadValue<float>();

        var horizontalVelocity = horizontal*playerData.speed;

        if(Mathf.Abs(rigidBody.velocity.x) < playerData.maxSpeed){
            rigidBody.AddForce(new Vector2(horizontalVelocity, 0),ForceMode2D.Impulse);
        }
        if(!playerData.grounded && isTouchingCol && playerData.forwardRaycastHit){
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //If player is touching the ground don't jump
        if (!playerData.grounded) return;
        rigidBody.AddRelativeForce(new Vector2(0,playerData.jumpVelocity), ForceMode2D.Impulse);
        Debug.Log("Jump Key");
    }

    private void Stop(InputAction.CallbackContext context) {
        rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
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

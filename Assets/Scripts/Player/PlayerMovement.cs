using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;
    private Collider2D col;
    private bool isTouchingCol;
    private PlayerInputActions playerInputActions;
    private PlayerInput playerInput;
    public float speed;
    public float jumpVelocity;
    public float maxSpeed;
    private Vector3 boundSize;
    private Vector3 boundCenterOffset;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerInput = GetComponent<PlayerInput>();
        isTouchingCol = false;
        playerInputActions = playerInput.playerInputActions;
        playerInputActions.Player.Move.performed += Stop; //Subscribe to input system triggers
        playerInputActions.Player.Jump.started += Jump;
        boundSize = col.bounds.size;
        boundCenterOffset = transform.position - col.bounds.center;
    }

    // Update is called once per frame
    private void Update() {
        if(!playerInput.inputLocked)
            Move();
    }

    public void Move()
    {
        var horizontal = playerInputActions.Player.Move.ReadValue<float>();

        var horizontalVelocity = horizontal*speed;

        if(Mathf.Abs(rigidBody.velocity.x) < maxSpeed){
            rigidBody.AddForce(new Vector2(horizontalVelocity, 0),ForceMode2D.Impulse);
        }
        if(!PlayerRaycasts.Grounded(transform.position,boundCenterOffset,boundSize) && isTouchingCol && PlayerRaycasts.ForwardHit(transform,boundCenterOffset,boundSize,transform.right)){
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //If player is not touching the ground don't jump
        if (!PlayerRaycasts.Grounded(transform.position,boundCenterOffset,boundSize)) return;
        rigidBody.AddRelativeForce(new Vector2(0,jumpVelocity), ForceMode2D.Impulse);
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

using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = transform.parent.GetComponent<Rigidbody2D>();
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerInput.playerInputActions.Player.Move.ReadValue<float>() == 0){
            animator.SetBool("Walk",false);
            animator.SetBool("Idle",true);
        }else{
            animator.SetBool("Walk",true);
            animator.SetBool("Idle",false);
        }
    }
}

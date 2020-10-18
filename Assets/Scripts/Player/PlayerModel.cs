using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerData playerData;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = transform.parent.GetComponent<Rigidbody2D>();
        playerData = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerData.playerInputActions.Player.Move.ReadValue<float>() == 0){
            animator.SetBool("Walk",false);
            animator.SetBool("Idle",true);
        }else{
            animator.SetBool("Walk",true);
            animator.SetBool("Idle",false);
        }
    }
}

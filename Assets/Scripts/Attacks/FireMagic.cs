using UnityEngine;

public class FireMagic : MonoBehaviour
{
    private GameObject player;
    public GameObject flamethrower;
    private PlayerInput playerInput;

    private void Start(){
        player = GameObject.FindWithTag("Player");
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update(){
        
    }
    //Read directional input and cast the appropriate spell
    public void CastSpell(){
        var attackDirection = playerInput.playerInputActions.Player.AttackDirection.ReadValue<Vector2>();

        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)){
            
        }else if (attackDirection.Equals(Vector2.down)) {
            Flamethrower(flamethrower);
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }
    }
    //Spawn flamethrower projectile in front of player.
    private void Flamethrower(GameObject projectilePrefab){
        Vector3 playerForward = transform.parent.right;
        var projectile = Instantiate(projectilePrefab, player.transform.position + new Vector3(7.5f*playerForward.x,0.2f,0), Quaternion.identity);
        projectile.transform.parent = player.transform;
        projectile.transform.rotation = Quaternion.Euler(0,0,80*playerForward.x);
    }
}

using UnityEngine;

public class EarthMagic : MonoBehaviour
{
    //Magic Prefabs
    public GameObject earthWall;
    private GameObject player;
    private PlayerInput playerInput;
    private void Start(){
        player = GameObject.FindWithTag("Player");
        playerInput = GetComponent<PlayerInput>();
    }

    //Read directional input and cast the appropriate spell
    public void CastSpell(){
        var attackDirection = playerInput.playerInputActions.Player.AttackDirection.ReadValue<Vector2>();

        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)){
            
        }else if (attackDirection.Equals(Vector2.down)) {
            EarthWall(earthWall, 2,2);
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }
    }

    //Construct a wall of earth in front of the player that blocks attacks. Use a raycast to check if the earth wall can be spawned.
    private void EarthWall(GameObject earthWallPrefab, float raycastLength, float earthWallPosition){
        //if(!playerData.grounded)
            //return;
        Vector3 playerForward = playerInput.GetForward();
        Vector2 spawnPosition = player.transform.position + playerForward*earthWallPosition;
        var rayHit = Physics2D.Raycast(spawnPosition, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(spawnPosition, Vector2.down*raycastLength,Color.red);
        
        if (!rayHit) return;
        var wall = Instantiate(earthWallPrefab, spawnPosition, Quaternion.identity);
        Destroy(wall, 2);
    }

    private void EarthPunch(){
        
    }
}

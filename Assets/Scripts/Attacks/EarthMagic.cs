using UnityEngine;

public class EarthMagic : MonoBehaviour
{
    //Magic Prefabs
    public GameObject earthWall;
    private GameObject player;
    private PlayerData playerData;

    private void Start(){
        player = GameObject.FindWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
    }

    //Read directional input and cast the appropriate spell
    public void CastSpell(){
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if(horizontal != 0){
            
        }else if(vertical > 0){
            EarthWall(earthWall, 2,2);
        }else if(vertical < 0){
            
        }
    }

    //Construct a wall of earth in front of the player that blocks attacks. Use a raycast to check if the earth wall can be spawned.
    private void EarthWall(GameObject earthWallPrefab, float raycastLength, float earthWallPosition){
        if(!playerData.grounded)
            return;
        Vector3 playerForward = playerData.forward;
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

using UnityEngine;

public class EarthMagic : MonoBehaviour
{
    //Magic Prefabs
    public GameObject earthWall;
    private GameObject player;

    private void Start(){

    }

    private void Update(){
        
    }
    //Read directional input and cast the appropriate spell
    public void CastSpell(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
            
        }else if(vertical > 0){
            EarthWall(earthWall, 2,10);
        }else if(vertical < 0){
            
        }
    }
    //Construct a wall of earth in front of the player that blocks attacks. Use a raycast to check if the earth wall can be spawned.
    private void EarthWall(GameObject gameObject, float raycastLength, float earthWallPosition){
        if(!player.GetComponent<PlayerData>().grounded)
            return;
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        Vector2 spawnPosition = player.transform.position + playerForward*earthWallPosition;
        RaycastHit2D rayHit = Physics2D.Raycast(spawnPosition, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(spawnPosition, Vector2.down*raycastLength,Color.red);
        if(rayHit)
            earthWall = Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }
}

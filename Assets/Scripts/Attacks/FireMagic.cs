using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public GameObject player;

    private void Start(){

    }

    private void Update(){
        
    }
    //Read directional input and cast the appropriate spell
    public void CastSpell(){
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
           
        }else if(vertical > 0){
            
        }else if(vertical < 0){
            
        }
    }
    //Spawn flamethrower projectile in front of player.
    private void Flamethrower(GameObject projectilePrefab){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        var projectile = Instantiate(projectilePrefab, player.transform.position + new Vector3(7.5f*playerForward.x,0.2f,0), Quaternion.identity);
        projectile.transform.parent = player.transform;
        projectile.transform.rotation = Quaternion.Euler(0,0,80*playerForward.x);
    }
}

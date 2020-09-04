using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public GameObject player;
    void Start(){

    }
    void Update(){
        
    }
    public void castSpell(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
           
        }else if(vertical > 0){
            
        }else if(vertical < 0){
            
        }
    }
    private void Flamethrower(GameObject projectilePrefab){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
        GameObject projectile = Instantiate(projectilePrefab, player.transform.position + new Vector3(7.5f*playerForward.x,0.2f,0), Quaternion.identity);
        projectile.transform.parent = player.transform;
        projectile.transform.rotation = Quaternion.Euler(0,0,80*playerForward.x);
    }
}

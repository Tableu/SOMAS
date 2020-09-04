using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMagic : MonoBehaviour
{
    public GameObject earthWall;
    private GameObject player;
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
            EarthWall(earthWall, 2,10);
        }else if(vertical < 0){
            
        }
    }
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

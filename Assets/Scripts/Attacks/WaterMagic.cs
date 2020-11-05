﻿using UnityEngine;

public class WaterMagic : MonoBehaviour
{
    public GameObject waterOrb;
    public GameObject waterWave;
    private GameObject player;
    private PlayerInput playerInput;
    private void Start()
    {
        playerInput = transform.parent.GetComponent<PlayerInput>();
    }

    private void Update(){

    }
    public void CastSpell(){
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
            BasicWaterAttack(waterOrb, 10, 10);
        }else if(vertical > 0){
            
        }else if(vertical < 0){
            WaveAttack(waterWave,10,new Vector2(2,0));
        }
    }
    public void BasicWaterAttack(GameObject projectilePrefab, float speed,float rotationSpeed){
        
        //var projectile = AttackFunctions.SpawnProjectile(projectilePrefab, player,speed);
        //projectile.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed);
        
    }
    private void IceAttack(GameObject projectilePrefab, float speed){
        Vector3 playerForward = transform.parent.right;
        var playerPos = player.GetComponent<PlayerInput>().playerPos;
        var spawnPos = new Vector3[3];
        var projectile = new GameObject[3];
        var angle = 0;
        spawnPos[0] = player.transform.position + playerForward*-1f*2 + new Vector3(0,1,0);
        spawnPos[1] = player.transform.position + playerForward*-1f*4;
        spawnPos[2] = player.transform.position + playerForward*-1f*2 + new Vector3(0,-1,0);
        
        projectile[0] = Instantiate(projectilePrefab, spawnPos[0], Quaternion.identity);
        projectile[1] = Instantiate(projectilePrefab, spawnPos[1], Quaternion.identity);
        projectile[2] = Instantiate(projectilePrefab, spawnPos[2], Quaternion.identity);

        if(playerForward.x < 0){
            angle = 180;
        }
        projectile[0].transform.rotation = Quaternion.Euler(0,0,angle);
        projectile[1].transform.rotation = Quaternion.Euler(0,0,angle);
        projectile[2].transform.rotation = Quaternion.Euler(0,0,angle);
        playerForward = transform.parent.right;
        projectile[0].GetComponent<Rigidbody2D>().velocity = playerForward*speed;
        projectile[1].GetComponent<Rigidbody2D>().velocity = playerForward*speed;
        projectile[2].GetComponent<Rigidbody2D>().velocity = playerForward*speed;
    }
    private void WaveAttack(GameObject projectilePrefab, float speed, Vector2 displacement){
        Vector3 playerForward = transform.parent.right;
        Vector3 dist = playerForward*displacement;
        var angle = 0;
        var projectile = Instantiate(projectilePrefab, player.transform.position+dist, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = playerForward*speed;
        if(playerForward.x < 0){
            angle = 180;
        }
        projectile.transform.rotation = Quaternion.Euler(0,angle,0);
    }
}

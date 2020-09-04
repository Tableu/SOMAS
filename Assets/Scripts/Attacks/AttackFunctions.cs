using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackFunctions 
{
    
    public static GameObject SpawnProjectile(GameObject projectilePrefab, GameObject player, float speed){
        GameObject projectile = Transform.Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        return projectile;
    }
}

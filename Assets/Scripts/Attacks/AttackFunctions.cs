using UnityEngine;

public class AttackFunctions 
{
    //Instantiate projectile at player position and give it a velocity. Return a reference to the projectile
    public static GameObject SpawnProjectile(GameObject projectilePrefab, GameObject player, float speed){
        var projectile = Transform.Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        return projectile;
    }
}

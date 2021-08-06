using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SpellCommand {
    void Execute(GameObject projectilePrefab, GameObject player);
}

public class WaterOrbCommand : SpellCommand {
    public void Execute(GameObject projectilePrefab, GameObject player) {
        var projectileScript = projectilePrefab.GetComponent<WaterOrbInfo>();
        var playerInput = player.GetComponent<PlayerInput>();
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var projectile = GameObject.Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.transform.right*projectileScript.speed;
        projectile.GetComponent<Rigidbody2D>().AddTorque(projectileScript.rotationSpeed);
        playerInput.manaPoints -= cost;
    }
}

public class WaterWaveCommand : SpellCommand {
    public void Execute(GameObject projectilePrefab, GameObject player) {
        var projectileScript = projectilePrefab.GetComponent<WaterWaveInfo>();
        Vector3 dist = player.transform.right*projectileScript.displacement;
        var playerInput = player.GetComponent<PlayerInput>();
        var angle = 0;
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var projectile = GameObject.Instantiate(projectilePrefab, player.transform.position+dist, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.transform.right*projectileScript.speed;
        if(player.transform.right.x < 0){
            angle = 180;
        }
        projectile.transform.rotation = Quaternion.Euler(0,angle,0);
        playerInput.manaPoints -= cost;
    }
}
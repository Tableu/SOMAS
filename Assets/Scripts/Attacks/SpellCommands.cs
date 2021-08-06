using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SpellCommand {
    void WaterOrb(GameObject projectilePrefab, GameObject player);
    void WaterWave(GameObject projectilePrefab, GameObject player);
    void Flamethrower(GameObject projectilePrefab, GameObject player);
    void EarthCreation(GameObject projectilePrefab, GameObject player);
}

public class Spellbook : SpellCommand {
    public void WaterOrb(GameObject projectilePrefab, GameObject player) {
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
    public void WaterWave(GameObject projectilePrefab, GameObject player) {
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
    public void Flamethrower(GameObject projectilePrefab, GameObject player) {
        Vector3 playerForward = player.transform.right;
        var playerInput = player.GetComponent<PlayerInput>();
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var projectile = GameObject.Instantiate(projectilePrefab, player.transform.position+new Vector3(0,1,0), Quaternion.identity);
        projectile.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        projectile.transform.parent = player.transform;
        if (player.transform.right.x < 0) {
            projectile.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            projectile.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        playerInput.manaPoints -= cost;
    }
    
    public void EarthCreation(GameObject projectilePrefab, GameObject player) {
        var transform = player.transform;
        var projectileScript = projectilePrefab.GetComponent<EarthWallInfo>();
        Vector3 playerForward = transform.right;
        var playerInput = player.GetComponent<PlayerInput>();
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var rayHit = Physics2D.Raycast(transform.position, Vector2.down, projectileScript.raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(transform.position, Vector2.down*projectileScript.raycastLength,Color.red);

        if (rayHit) {
            Vector2 spawnPosition = transform.position + playerForward*projectileScript.position;
            rayHit = Physics2D.Raycast(spawnPosition, Vector2.down, projectileScript.raycastLength, LayerMask.GetMask("Platforms"));
            Debug.DrawRay(spawnPosition, Vector2.down*projectileScript.raycastLength,Color.red);
            if (!rayHit)
                return;
            var wall = GameObject.Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            wall.GetComponent<EarthWallInfo>().lifetime = wall.GetComponent<EarthWallInfo>().wallLifetime;
            playerInput.manaPoints -= cost;
        }else {
            var platform = GameObject.Instantiate(projectilePrefab, transform.position+ new Vector3(0,-2,0), Quaternion.identity);
            platform.transform.rotation = Quaternion.Euler(0,0,90);
            platform.GetComponent<EarthWallInfo>().lifetime = platform.GetComponent<EarthWallInfo>().platformLifetime;
            playerInput.manaPoints -= cost;
        }
    }
}
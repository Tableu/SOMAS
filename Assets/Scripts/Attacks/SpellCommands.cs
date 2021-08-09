using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public interface SpellCommand {
    void WaterOrb(GameObject projectilePrefab, GameObject player);
    void WaterWave(GameObject projectilePrefab, GameObject player);
    void Flamethrower(GameObject projectilePrefab, GameObject player);
    void EarthCreation(GameObject projectilePrefab, GameObject player);
    void IceAttack(GameObject projectilePrefab, GameObject player);
    IEnumerator FreezeAttack(GameObject projectilePrefab, GameObject player);
    public void EarthPunch(GameObject projectilePrefab, GameObject player);
}


public class Spellbook : SpellCommand {
    private static readonly int Stunned = Animator.StringToHash("Stunned");
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

    public void IceAttack(GameObject projectilePrefab, GameObject player) {
        var transform = player.transform;
        var speed = projectilePrefab.GetComponent<Projectile>().speed;
        var playerForward = transform.parent.right;
        var spawnPos = new Vector3[3];
        var projectile = new GameObject[3];
        var angle = 0;
        spawnPos[0] = transform.position + playerForward*-1f*2 + new Vector3(0,1,0);
        spawnPos[1] = transform.position + playerForward*-1f*4;
        spawnPos[2] = transform.position + playerForward*-1f*2 + new Vector3(0,-1,0);
        
        projectile[0] = GameObject.Instantiate(projectilePrefab, spawnPos[0], Quaternion.identity);
        projectile[1] = GameObject.Instantiate(projectilePrefab, spawnPos[1], Quaternion.identity);
        projectile[2] = GameObject.Instantiate(projectilePrefab, spawnPos[2], Quaternion.identity);

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

    public IEnumerator FreezeAttack(GameObject projectilePrefab, GameObject player) {
        var transform = player.transform;
        var freezeDuration = projectilePrefab.GetComponent<FreezeAttackInfo>().freezeDuration;
        var hit = Physics2D.CircleCast(transform.position,3,new Vector2(1,0),1,LayerMask.GetMask("MagicTriggers","Enemies"));
        if (!hit) yield break;
        if (hit.collider.CompareTag("Water")) {
            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            hit.collider.gameObject.tag = "Ice";
        }
        else if (hit.collider.CompareTag("Enemy")) {
            hit.collider.gameObject.GetComponent<Animator>().SetBool(Stunned, true);
            var ice = GameObject.Instantiate(projectilePrefab, hit.transform.position,quaternion.identity);
            ice.transform.parent = hit.transform;
            yield return new WaitForSeconds(freezeDuration);
            hit.collider.gameObject.GetComponent<Animator>().SetBool(Stunned, false);
            GameObject.Destroy(ice);
        }
    }
    private Vector2 RandomVector2(float angle, float angleMin){
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    public void EarthPunch(GameObject projectilePrefab, GameObject player) {
        float raycastLength = 4;
        var transform = player.transform;
        var hit = Physics2D.Raycast(transform.position, transform.right, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(transform.position, transform.right*raycastLength,Color.red);
        if (hit) {
            for (var index = 0; index < 3; index++) {
                var fragment = GameObject.Instantiate(projectilePrefab, hit.collider.transform.position, Quaternion.identity);
                var randVector = RandomVector2(20 * (3.1415f / 180f), -10 * (3.1415f / 180f)) * 50;
                randVector = new Vector2(transform.right.x*randVector.x, randVector.y);
                fragment.GetComponent<Rigidbody2D>().velocity = randVector;
            }
            GameObject.Destroy(hit.collider.gameObject);
        }else{
            var fragment = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.right.x*50,0);
        }
    }
}
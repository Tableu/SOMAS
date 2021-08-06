using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpells : MonoBehaviour
{
    public GameObject waterOrb;
    public GameObject waterWave;
    public GameObject flamethrower;
    public GameObject earthWall;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private bool cast; //Prevents a water spell from casting if a spell has already been casted

    private SpellCommand waterWaveCommand;

    private SpellCommand waterOrbCommand;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = playerInput.playerInputActions;
        cast = false;
        playerInputActions.Player.BasicSpell.started += context => {
            cast = false;
            CastSpell();
        };
        playerInputActions.Player.BasicSpell.performed += context => {
            WaterSpells(context.duration);
        };
        
        waterWaveCommand = new WaterWaveCommand();
        waterOrbCommand = new WaterOrbCommand();
    }
    private void CastSpell(){
        var attackDirection = playerInputActions.Player.HoldAttack.ReadValue<Vector2>();
        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            Flamethrower(flamethrower);
            cast = true;
        }else if (attackDirection.Equals(Vector2.down)) {
            EarthCreation(earthWall, 2,2);
            cast = true;
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }
    }
    private void WaterSpells(double holdDuration){
        if(cast)
            return;
        if (holdDuration > 1) {
            waterWaveCommand.Execute(waterWave, gameObject);
        }else if (holdDuration > 0.5) {
            waterOrbCommand.Execute(waterOrb, gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void WaterOrb(GameObject projectilePrefab, float speed,float rotationSpeed) {
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.right*speed;
        projectile.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed);
        playerInput.manaPoints -= cost;
    }
    private void WaveAttack(GameObject projectilePrefab, float speed, Vector2 displacement){
        Vector3 playerForward = transform.right;
        Vector3 dist = playerForward*displacement;
        var angle = 0;
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var projectile = Instantiate(projectilePrefab, transform.position+dist, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = playerForward*speed;
        if(playerForward.x < 0){
            angle = 180;
        }
        projectile.transform.rotation = Quaternion.Euler(0,angle,0);
        playerInput.manaPoints -= cost;
    }
    private void Flamethrower(GameObject projectilePrefab){
        Vector3 playerForward = transform.right;
        var mana = playerInput.manaPoints;
        var cost = projectilePrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var projectile = Instantiate(projectilePrefab, transform.position+new Vector3(0,1,0), Quaternion.identity);
        projectile.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        projectile.transform.parent = transform;
        if (transform.right.x < 0) {
            projectile.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            projectile.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        playerInput.manaPoints -= cost;
    }
    private void EarthCreation(GameObject earthWallPrefab, float raycastLength, float position){
        Vector3 playerForward = transform.right;
        var mana = playerInput.manaPoints;
        var cost = earthWallPrefab.GetComponent<Projectile>().manaCost;
        if (mana < cost)
            return;
        var rayHit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(transform.position, Vector2.down*raycastLength,Color.red);

        if (rayHit) {
            Vector2 spawnPosition = transform.position + playerForward*position;
            rayHit = Physics2D.Raycast(spawnPosition, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
            Debug.DrawRay(spawnPosition, Vector2.down*raycastLength,Color.red);
            if (!rayHit)
                return;
            var wall = Instantiate(earthWallPrefab, spawnPosition, Quaternion.identity);
            Destroy(wall, 2);
            playerInput.manaPoints -= cost;
        }else {
            var platform = Instantiate(earthWallPrefab, transform.position+ new Vector3(0,-2,0), Quaternion.identity);
            platform.transform.rotation = Quaternion.Euler(0,0,90);
            Destroy(platform, 5);
            playerInput.manaPoints -= cost;
        }
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EarthMagic : MonoBehaviour
{
    //Magic Prefabs
    public GameObject earthWall;
    public GameObject wallFragment;
    private GameObject player;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private void Start(){
        player = GameObject.FindWithTag("Player");
        playerInput =  transform.parent.GetComponent<PlayerInput>();
        playerInputActions = playerInput.playerInputActions;
        playerInputActions.Player.Earth.performed += context => {
            CastSpell();
        };
        playerInputActions.Player.EarthCreation.performed += context => {
            EarthCreation(earthWall,2,2);
        };
    }

    //Read directional input and cast the appropriate spell
    public void CastSpell(){
        var attackDirection = playerInputActions.Player.TapAttack.ReadValue<Vector2>();

        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            EarthPunch(wallFragment);
        }else if (attackDirection.Equals(Vector2.down)) {
            //EarthCreation(earthWall, 2,2);
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }
    }

    //Construct a wall of earth in front of the player that blocks attacks. Use a raycast to check if the earth wall can be spawned.
    private void EarthCreation(GameObject earthWallPrefab, float raycastLength, float position){
        Vector3 playerForward = transform.parent.right;
        
        var rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(player.transform.position, Vector2.down*raycastLength,Color.red);

        if (rayHit) {
            Vector2 spawnPosition = player.transform.position + playerForward*position;
            rayHit = Physics2D.Raycast(spawnPosition, Vector2.down, raycastLength, LayerMask.GetMask("Platforms"));
            Debug.DrawRay(spawnPosition, Vector2.down*raycastLength,Color.red);
            if (!rayHit)
                return;
            var wall = Instantiate(earthWallPrefab, spawnPosition, Quaternion.identity);
            Destroy(wall, 2);
        }else {
            var platform = Instantiate(earthWallPrefab, player.transform.position+ new Vector3(0,-2,0), Quaternion.identity);
            platform.transform.rotation = Quaternion.Euler(0,0,90);
            Destroy(platform, 5);
        }
    }
    private Vector2 RandomVector2(float angle, float angleMin){
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    private void EarthPunch(GameObject wallFragment) {
        float raycastLength = 4;
        var hit = Physics2D.Raycast(player.transform.position, player.transform.right, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(player.transform.position, player.transform.right*raycastLength,Color.red);
        if (!hit) return;
        for (int index = 0; index < 3; index++) {
            var fragment = Instantiate(wallFragment, hit.collider.transform.position, Quaternion.identity);
            fragment.GetComponent<Rigidbody2D>().velocity = RandomVector2(20 * (3.1415f / 180f), -10 * (3.1415f / 180f)) * 50;
        }
        Destroy(hit.collider.gameObject);
    }
}

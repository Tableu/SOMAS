using UnityEngine;

public class WaterMagic : MonoBehaviour
{
    public GameObject waterOrb;
    public GameObject waterWave;
    private GameObject player;
    private PlayerInput playerInput;
    public PlayerInputActions playerInputActions;
    private void Start()
    {
        playerInput = transform.parent.GetComponent<PlayerInput>();
        player = GameObject.FindWithTag("Player");
        playerInputActions = playerInput.playerInputActions;
        playerInputActions.Player.Water.performed += context => {
            CastSpell(context.duration);
        };
        playerInputActions.Player.Ice.performed += context => {
            FreezeAttack(2);
        };
    }

    private void Update(){

    }
    private void CastSpell(double holdDuration){
        var attackDirection = playerInputActions.Player.TapAttack.ReadValue<Vector2>();
        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            //basic water push
        }else if (attackDirection.Equals(Vector2.down)) {
            
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }else{
            if (holdDuration > 1){
                WaveAttack(waterWave, 10, new Vector2(2, 0));
            }
            else{
                WaterOrb(waterOrb, 10, 10);
            }
        }
    }
    private void WaterOrb(GameObject projectilePrefab, float speed,float rotationSpeed){
        var projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = player.transform.right*speed;
        projectile.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed);
    }
    private void IceAttack(GameObject projectilePrefab, float speed){
        Vector3 playerForward = transform.parent.right;
        var playerPos = player.transform.position;
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

    private void FreezeAttack(float freezeDuration)
    {
        var hit = Physics2D.CircleCast(player.transform.position,3,new Vector2(1,0),1,LayerMask.GetMask("Water"));
        if(hit)
            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
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

using UnityEngine;

public class WaterMagic : MonoBehaviour
{
    public GameObject waterOrb;
    public GameObject waterWave;
    private GameObject player;

    private void Start(){

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
        player.GetComponent<PlayerInput>().lockInput = true;
        var projectile = AttackFunctions.SpawnProjectile(projectilePrefab, player,speed);
        projectile.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed);
        player.GetComponent<PlayerInput>().lockInput = false;
    }
    private void IceAttack(GameObject projectilePrefab, float speed){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
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

        projectile[0].GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        projectile[1].GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
        projectile[2].GetComponent<Rigidbody2D>().velocity = player.GetComponent<PlayerData>().forward*speed;
    }
    private void WaveAttack(GameObject projectilePrefab, float speed, Vector2 displacement){
        Vector3 playerForward = player.GetComponent<PlayerData>().forward;
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

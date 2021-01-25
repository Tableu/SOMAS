using System.Collections;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class FormSpells : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    public GameObject wallFragmentPrefab;
    public GameObject icePrison;
    public GameObject iceSpike;
    private static readonly int Stunned = Animator.StringToHash("Stunned");
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = playerInput.playerInputActions;
        playerInputActions.Player.FormSpell.started += context => {
            CastSpell();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CastSpell(){
        var attackDirection = playerInputActions.Player.TapAttack.ReadValue<Vector2>();

        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            EarthPunch(wallFragmentPrefab);
        }else if (attackDirection.Equals(Vector2.down)) {
            StartCoroutine(FreezeAttack(2));
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }else {
            
        }
    }
    private void IceAttack(GameObject projectilePrefab, float speed){
        Vector3 playerForward = transform.parent.right;
        var spawnPos = new Vector3[3];
        var projectile = new GameObject[3];
        var angle = 0;
        spawnPos[0] = transform.position + playerForward*-1f*2 + new Vector3(0,1,0);
        spawnPos[1] = transform.position + playerForward*-1f*4;
        spawnPos[2] = transform.position + playerForward*-1f*2 + new Vector3(0,-1,0);
        
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

    IEnumerator FreezeAttack(float freezeDuration)
    {
        var hit = Physics2D.CircleCast(transform.position,3,new Vector2(1,0),1,LayerMask.GetMask("MagicTriggers","Enemies"));
        if (!hit) yield break;
        if (hit.collider.CompareTag("Water")) {
            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            hit.collider.gameObject.tag = "Ice";
        }
        else if (hit.collider.CompareTag("Enemy")) {
            hit.collider.gameObject.GetComponent<Animator>().SetBool(Stunned, true);
            var ice = Instantiate(icePrison, hit.transform.position,quaternion.identity);
            ice.transform.parent = hit.transform;
            yield return new WaitForSeconds(freezeDuration);
            hit.collider.gameObject.GetComponent<Animator>().SetBool(Stunned, false);
            Destroy(ice);
        }
    } //Prevents the casting of a a water spell from casting if therea spell has already been casted
    private Vector2 RandomVector2(float angle, float angleMin){
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    private void EarthPunch(GameObject wallFragment) {
        float raycastLength = 4;
        var hit = Physics2D.Raycast(transform.position, transform.right, raycastLength, LayerMask.GetMask("Platforms"));
        Debug.DrawRay(transform.position, transform.right*raycastLength,Color.red);
        if (hit) {
            for (var index = 0; index < 3; index++) {
                var fragment = Instantiate(wallFragment, hit.collider.transform.position, Quaternion.identity);
                fragment.GetComponent<Rigidbody2D>().velocity =
                    RandomVector2(20 * (3.1415f / 180f), -10 * (3.1415f / 180f)) * 50;
            }
            Destroy(hit.collider.gameObject);
        }else{
            var fragment = Instantiate(wallFragment, transform.position, Quaternion.identity);
            fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.right.x*50,0);
        }
    }
}

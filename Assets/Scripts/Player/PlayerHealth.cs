using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints;
    private Collider2D col;
    public delegate void DeathEventDelegate();
    public event DeathEventDelegate DeathEvent; //Handles death of player, i.e stop camera, stop enemies
    private Animator animator;
    private PlayerData playerData;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (healthPoints > 0) return;
        DeathEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("EnemyProjectile")) return;
        
        healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
        var direction = collision.transform.GetComponent<Rigidbody2D>().velocity.normalized.x;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(playerData.knockback.x*direction,playerData.knockback.y),ForceMode2D.Impulse);
        StartCoroutine(Invulnerable());
        if(collision.gameObject.GetComponent<Projectile>().destructible){
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        
    }

    private IEnumerator Invulnerable(){
        col.enabled = false;
        yield return new WaitForSeconds(3);
        col.enabled = true;
    }
}

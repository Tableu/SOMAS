using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints;
    private Collider2D col;
    public delegate void DeathEventDelegate();
    public event DeathEventDelegate DeathEvent; //Handles death of player, i.e stop camera, stop enemies
    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerInput playerInput;
    public Vector2 knockback;
    private Vector3 boundSize;
    private Vector3 boundCenterOffset;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        boundSize = col.bounds.size;
        boundCenterOffset = transform.position - col.bounds.center;
    }

    // Update is called once per frame
    private void Update()
    {
        if (healthPoints > 0) return;
        DeathEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Invulnerable());
            StartCoroutine(LockPlayerInput());
            healthPoints -= 10;
            var direction = (gameObject.transform.position - collision.gameObject.transform.position).normalized.x;
            rigidBody.velocity = Vector2.zero;
            if (PlayerRaycasts.Grounded(transform.position, boundCenterOffset,boundSize)) {
                rigidBody.AddForce(new Vector2(knockback.x * direction, knockback.y), ForceMode2D.Impulse);
            }
            else {
                rigidBody.AddForce(new Vector2(knockback.x * direction, 0), ForceMode2D.Impulse);
            }
        }else if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            StartCoroutine(Invulnerable());
            StartCoroutine(LockPlayerInput());
            healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
            var direction = collision.transform.GetComponent<Rigidbody2D>().velocity.normalized.x;
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(knockback.x*direction,knockback.y),ForceMode2D.Impulse);
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision){
        
    }

    private IEnumerator LockPlayerInput()
    {
        playerInput.inputLocked = true;
        yield return new WaitForSeconds(0.2f);
        playerInput.inputLocked = false;
    }
    private IEnumerator Invulnerable(){
        gameObject.layer = 14;
        yield return new WaitForSeconds(1f);
        gameObject.layer = 12;
    }
}

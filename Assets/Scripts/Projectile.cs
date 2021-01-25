using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damagePoints;
    public bool destructible;
    public int healthPoints;
    public bool timeLimit;
    public int lifetime;

    public int manaCost;
    // Start is called before the first frame update
    private void Start()
    {
        if (timeLimit){
            StartCoroutine(deathCoroutine());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(destructible){
            Destroy(gameObject);
        }
    }

    IEnumerator deathCoroutine()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

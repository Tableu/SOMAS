using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damagePoints;
    public bool destructible;
    public int healthPoints;
    // Start is called before the first frame update
    private void Start()
    {
        
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
}

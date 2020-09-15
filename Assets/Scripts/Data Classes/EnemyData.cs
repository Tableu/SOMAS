using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Holds variables for enemy scripts to use. Sets initial values
public class EnemyData : MonoBehaviour
{
    //Stats
    public float speed;
    public Vector2 forward;
    public bool playerFound;
    public bool grounded;
    public Vector2 distanceToPlayer;
    public int mask;
    //Raycasts
    public Vector2[] raycastDirections;
    public RaycastHit2D[] raycastArray;
    //Boundaries
    public float[] boundary;
    public float attackLimit;
    public float followLimit;
    //Other
    private GameObject player;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Spawner spawner = transform.parent.GetComponent<Spawner>();
        boundary = new float[2];
        boundary[0] = spawner.leftBoundary.position.x;
        boundary[1] = spawner.rightBoundary.position.x;
        raycastArray = new RaycastHit2D[raycastDirections.Length];
        grounded = false;
        mask = LayerMask.GetMask("Player", "Platforms");
    }

    void Update()
    {
        animator.SetBool("PlayerFound",playerFound);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Platform"){
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == "Platform"){
            grounded = false;
        }
    }
}

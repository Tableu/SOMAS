using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public float[] boundary;
    public Vector2 forward;
    public float attackLimit;
    public float followLimit;
    public bool playerFound;
    public bool grounded;
    public Vector2 distanceToPlayer;
    public int mask;
    public Vector2[] raycastDirections;
    public RaycastHit2D[] raycastArray;
    private GameObject player;
    
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
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

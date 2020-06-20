using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 velocity;
    public Transform player;
    public float horizontalFollowDistance;
    public float verticalFollowDistance;
    private float horizontalOffset;
    public float[] horizontalLimits = new float[2];
    public float[] verticalLimits = new float[2];
    public Transform leftBoundary;
    public Transform rightBoundary;
    // Start is called before the first frame update
    void Awake(){
        leftBoundary = transform.GetChild(1).transform;
        rightBoundary = transform.GetChild(2).transform;
    }
    void Start()
    {
        horizontalLimits[0] = leftBoundary.position.x;
        verticalLimits[0] = leftBoundary.position.y;
        horizontalLimits[1] = rightBoundary.position.x;
        verticalLimits[1] = rightBoundary.position.y;
        velocity = Vector3.zero;
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().deathEvent += OnDeathEvent;
    }
    void OnDeathEvent(){
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().deathEvent -= OnDeathEvent;
        gameObject.GetComponent<CameraScript>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer(){
        float horizontalDist = transform.position.x - player.position.x;
        float verticalDist = transform.position.y - player.position.y;
        Vector3 target;
        if(Mathf.Abs(horizontalDist) > horizontalFollowDistance){    
            target = new Vector3(player.position.x, transform.position.y,transform.position.z);
            if(player.position.x < horizontalLimits[0]){
                target = new Vector3(horizontalLimits[0], transform.position.y, transform.position.z);
            }else if(player.position.x > horizontalLimits[1]){
                target = new Vector3(horizontalLimits[1], transform.position.y, transform.position.z);
            }
            transform.position = Vector3.SmoothDamp(transform.position, target,ref velocity, 0.1F);
        }
        if(Mathf.Abs(verticalDist) > verticalFollowDistance){
            target = new Vector3(transform.position.x, player.position.y,transform.position.z);
            if(player.position.y < verticalLimits[0]){
                target = new Vector3(transform.position.x, verticalLimits[0], transform.position.z);
            }else if(player.position.y > verticalLimits[1]){
                target = new Vector3(transform.position.x, verticalLimits[1], transform.position.z);
            }
            transform.position = Vector3.SmoothDamp(transform.position, target,ref velocity, 0.3F);
        }
    }
    void OnEnable(){
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().deathEvent += OnDeathEvent;
    }
}

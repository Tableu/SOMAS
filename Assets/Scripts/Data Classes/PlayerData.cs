using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerData : MonoBehaviour
{
    //Stats
    public Vector2 forward;
    public bool grounded;
    public float speed;
    public float jumpVelocity;
    public float maxSpeed;
    public Vector2 knockback;
    //Raycast
    public bool forwardRaycastHit;
    //Boundaries
    public Vector3 boundSize;
    public Vector3 boundCenterOffset;
    ///Events
    public delegate void rotateEventDelegate();
    public event rotateEventDelegate rotateEvent;
    public float previous;
    void Start(){
        boundSize = GetComponent<Collider2D>().bounds.size;
        boundCenterOffset = transform.position - GetComponent<Collider2D>().bounds.center;
        previous = -1;
    }
    void Update(){
        updateRotation();
    }
    private void updateRotation(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal < 0 && previous > 0){
            transform.rotation = Quaternion.Euler(0,0,0);
            forward = Vector2.left;
            rotateEvent.Invoke();
            previous = horizontal;
        }else if(horizontal > 0 && previous < 0){
            transform.rotation = Quaternion.Euler(0,180,0);
            forward = Vector2.right;
            rotateEvent.Invoke();
            previous = horizontal;
        }
    }
}

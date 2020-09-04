using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerData : MonoBehaviour
{
    public Vector2 forward;
    public bool grounded;
    public bool forwardRaycastHit;
    public float speed;
    public float jumpVelocity;
    public float maxSpeed;
    public Vector2 knockback;
    public Vector3 boundSize;
    public Vector3 boundCenterOffset;
    void Start(){
        boundSize = gameObject.GetComponent<Collider2D>().bounds.size;
        boundCenterOffset = gameObject.transform.position - gameObject.GetComponent<Collider2D>().bounds.center;
    }
    void Update(){
        updateRotation();
    }
    private void updateRotation(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal < 0){
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            forward = Vector2.left;
        }else if(horizontal > 0){
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
            forward = Vector2.right;
        }
    }
}

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
    private SpriteRenderer spriteRenderer;
    private PlayerData playerData;
    void Start(){
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        playerData = this.gameObject.GetComponent<PlayerData>();
    }
    void Update(){
        updateRotation();
    }
    private void updateRotation(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal < 0){
            spriteRenderer.flipX = false;
            forward = Vector2.left;
        }else if(horizontal > 0){
            spriteRenderer.flipX = true;
            forward = Vector2.right;
        }
    }
}

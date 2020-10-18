﻿using UnityEngine;
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
    public delegate void RotateEventDelegate();
    public event RotateEventDelegate RotateEvent;
    public float previous;
    public PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
    private void Start(){
        boundSize = GetComponent<Collider2D>().bounds.size;
        boundCenterOffset = transform.position - GetComponent<Collider2D>().bounds.center;
        previous = -1;
    }

    private void Update(){
        UpdateRotation();
    }
    private void UpdateRotation() //Changes which side the player faces and invokes the relevant events
    {
        var horizontal = playerInputActions.Player.Move.ReadValue<float>();
        if(horizontal < 0 && previous > 0){
            transform.rotation = Quaternion.Euler(0,0,0);
            forward = Vector2.left;
            RotateEvent?.Invoke();
            previous = horizontal;
        }else if(horizontal > 0 && previous < 0){
            transform.rotation = Quaternion.Euler(0,180,0);
            forward = Vector2.right;
            RotateEvent?.Invoke();
            previous = horizontal;
        }
    }
}

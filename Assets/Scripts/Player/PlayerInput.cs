using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput: MonoBehaviour
{
    private PlayerMovement playerMovement;
    public delegate void RotateEventDelegate();
    public event RotateEventDelegate RotateEvent;
    public float previous;
    public PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputActions.Enable();
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }
    private void Start(){
        previous = -1;
    }
    // Update is called once per frame
    private void Update(){
        UpdateRotation();
    }
    public void LockInput() {
        playerInputActions.Player.Disable();
    }

    public void UnlockInput() {
        playerInputActions.Player.Enable();
    }
    private void UpdateRotation() //Changes which side the player faces and invokes the relevant events
    {
        var horizontal = playerInputActions.Player.Move.ReadValue<float>();
        if(horizontal < 0 && previous > 0){
            transform.rotation = Quaternion.Euler(0,180,0);
            RotateEvent?.Invoke();
            previous = horizontal;
        }else if(horizontal > 0 && previous < 0){
            transform.rotation = Quaternion.Euler(0,0,0);
            RotateEvent?.Invoke();
            previous = horizontal;
        }
    }
}

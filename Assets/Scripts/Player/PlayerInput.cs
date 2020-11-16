using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput: MonoBehaviour
{
    private PlayerMovement playerMovement;
    public bool inputLocked;
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
        inputLocked = false;
        //playerInputActions.Player.Water.Disable();
        //playerInputActions.Player.Ice.Disable();
        //playerInputActions.Player.Earth.Disable();
    }
    // Update is called once per frame
    private void Update(){
        UpdateRotation();
    }
    public void LockInput() {
        playerInputActions.Player.Jump.Disable();
        inputLocked = true;
    }

    public void UnlockInput() {
        playerInputActions.Player.Jump.Enable();
        inputLocked = false;
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

    private void OnTriggerEnter2D(Collider2D other) {
        /*var element = other.gameObject.tag;
        switch (element) {
            case "Water":
                playerInputActions.Player.Water.Enable();
                break;
            case "Ice":
                playerInputActions.Player.Ice.Enable();
                break;
            case "Earth":
                playerInputActions.Player.Earth.Enable();
                break;
        }*/
    }

    private void OnTriggerExit2D(Collider2D other) {
        /*var element = other.gameObject.tag;
        switch (element) {
            case "Water":
                playerInputActions.Player.Water.Disable();
                break;
            case "Ice":
                playerInputActions.Player.Ice.Disable();
                break;
            case "Earth":
                playerInputActions.Player.Earth.Disable();
                break;
        }*/
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput: MonoBehaviour
{
    public Vector3 playerPos;
    public bool inputLocked;
    public WaterMagic waterMagic;
    public EarthMagic earthMagic;
    public FireMagic fireMagic;
    public MetalSummon metalSummon;
    private PlayerMovement playerMovement;
    public delegate void RotateEventDelegate();
    public event RotateEventDelegate RotateEvent;
    public float previous;
    public PlayerInputActions playerInputActions;
    // Start is called before the first frame update
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
    private void Start()
    {
        inputLocked = false;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        previous = -1;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateRotation();
        if(!inputLocked){
            CheckInputs();
            playerMovement.Move();
        }
        //setMagicCore();
    }
    private void CheckInputs(){
        if(playerInputActions.Player.Core1.triggered){
            playerPos = transform.position;
            earthMagic.CastSpell();
        }else if(playerInputActions.Player.Core2.triggered){
            playerPos = transform.position;
            fireMagic.CastSpell();
        }else if(playerInputActions.Player.Core3.triggered){
            playerPos = transform.position;
            metalSummon.CastSpell();
        }
    }
    public void LockInput() {
        inputLocked = true;
    }

    public void UnlockInput()
    {
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
}

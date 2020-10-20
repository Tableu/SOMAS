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
    private PlayerData playerData;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    private void Start()
    {
        inputLocked = false;
        playerData = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!inputLocked){
            CheckInputs();
            playerMovement.Move();
        }
        //setMagicCore();
    }
    private void CheckInputs(){
        if(playerData.playerInputActions.Player.Core1.triggered){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            earthMagic.CastSpell();
        }else if(playerData.playerInputActions.Player.Core2.triggered){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            fireMagic.CastSpell();
        }else if(playerData.playerInputActions.Player.Core3.triggered){
            playerPos = GameObject.FindWithTag("Player").transform.position;
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
}

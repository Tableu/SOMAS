using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInputHandler : MonoBehaviour
{
    public GameObject waterOrb;
    public GameObject waterWave;
    public GameObject flamethrower;
    public GameObject earthWall;
    public GameObject wallFragmentPrefab;
    public GameObject icePrison;
    public GameObject iceSpike;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private bool cast; //Prevents a water spell from casting if a spell has already been casted
    private static readonly int Stunned = Animator.StringToHash("Stunned");
    private SpellCommand spellCommands;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = playerInput.playerInputActions;
        cast = false;
        playerInputActions.Player.BasicSpell.started += context => {
            cast = false;
            BasicSpell();
        };
        playerInputActions.Player.BasicSpell.performed += context => {
            WaterSpells(context.duration);
        };
        playerInputActions.Player.FormSpell.started += context => {
            FormSpell();
        };
        spellCommands = new Spellbook();
    }
    private void BasicSpell(){
        var attackDirection = playerInputActions.Player.HoldAttack.ReadValue<Vector2>();
        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            spellCommands.Flamethrower(flamethrower, gameObject);
            cast = true;
        }else if (attackDirection.Equals(Vector2.down)) {
            spellCommands.EarthCreation(earthWall, gameObject);
            cast = true;
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }
    }
    public void FormSpell(){
        var attackDirection = playerInputActions.Player.TapAttack.ReadValue<Vector2>();

        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            spellCommands.EarthPunch(wallFragmentPrefab, gameObject);
        }else if (attackDirection.Equals(Vector2.down)) {
            StartCoroutine(spellCommands.FreezeAttack(icePrison, gameObject));
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }else {
            
        }
    }
    private void WaterSpells(double holdDuration){
        if(cast)
            return;
        if (holdDuration > 1) {
            spellCommands.WaterWave(waterWave, gameObject);
        }else if (holdDuration > 0.5) {
            spellCommands.WaterOrb(waterOrb, gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

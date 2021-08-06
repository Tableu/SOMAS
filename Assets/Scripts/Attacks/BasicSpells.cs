using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpells : MonoBehaviour
{
    public GameObject waterOrb;
    public GameObject waterWave;
    public GameObject flamethrower;
    public GameObject earthWall;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private bool cast; //Prevents a water spell from casting if a spell has already been casted

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

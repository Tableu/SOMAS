using System.Collections;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class FormSpells : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    public GameObject wallFragmentPrefab;
    public GameObject icePrison;
    public GameObject iceSpike;
    private static readonly int Stunned = Animator.StringToHash("Stunned");
    private SpellCommand spellCommands;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = playerInput.playerInputActions;
        playerInputActions.Player.FormSpell.started += context => {
            CastSpell();
        };
        spellCommands = new Spellbook();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CastSpell(){
        var attackDirection = playerInputActions.Player.TapAttack.ReadValue<Vector2>();

        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right)) {
            spellCommands.EarthPunch(wallFragmentPrefab, gameObject);
        }else if (attackDirection.Equals(Vector2.down)) {
            StartCoroutine(spellCommands.FreezeAttack(icePrison, gameObject));
        }else if (attackDirection.Equals(Vector2.up)) {
            
        }else {
            
        }
    }
}

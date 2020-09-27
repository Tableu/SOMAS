using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;
    public int magicCoreCount;
    public Vector3 playerPos;
    public bool lockInput;
    private int magicCore;
    public WaterMagic waterMagic;
    public EarthMagic earthMagic;
    public FireMagic fireMagic;
    public MetalSummon metalSummon;
    // Start is called before the first frame update
    private void Start()
    {
        lockInput = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!lockInput){
            CheckInputs();
        }
        //setMagicCore();
    }
    private void CheckInputs(){
        if(Input.GetButtonDown("Fire1")){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            earthMagic.CastSpell();
        }else if(Input.GetButtonDown("Fire2")){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            fireMagic.CastSpell();
        }else if(Input.GetButtonDown("Fire3")){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            metalSummon.CastSpell();
        }
    }
    private void SetMagicCore(){
        if(Input.GetKeyDown("q")){
            magicCore--;
        }else if(Input.GetKeyDown("e")){
            magicCore++;
        }
        //Loop back to first or last element
        if(magicCore > magicCoreCount-1){
            magicCore = 0;
        }else if(magicCore < 0){
            magicCore = magicCoreCount-1;
        }
    }
}

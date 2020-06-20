using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;
    public int magicCoreCount;
    public Vector3 playerPos;
    public bool castingSpell;
    // Start is called before the first frame update
    void Start()
    {
        castingSpell = false;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!castingSpell){
            checkInputs();
        }
        setMagicCore();
    }
    private void checkInputs(){
        if(Input.GetButtonDown("Fire1")){
            animator.SetTrigger("Fire1");
            playerPos = GameObject.FindWithTag("Player").transform.position;
        }else if(Input.GetButtonDown("Fire2")){
            animator.SetTrigger("Fire2");
            playerPos = GameObject.FindWithTag("Player").transform.position;
        }else if(Input.GetButtonDown("Fire3")){
            animator.SetTrigger("Fire3");
            playerPos = GameObject.FindWithTag("Player").transform.position;
        }
    }
    private void setMagicCore(){
        int magicCore = animator.GetInteger("MagicCore");
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
        animator.SetInteger("MagicCore", magicCore);
    }
}

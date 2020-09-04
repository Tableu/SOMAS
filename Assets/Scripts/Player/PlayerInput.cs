﻿using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        lockInput = false;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockInput){
            checkInputs();
        }
        //setMagicCore();
    }
    private void checkInputs(){
        if(Input.GetButtonDown("Fire1")){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            waterMagic.castSpell();
        }else if(Input.GetButtonDown("Fire2")){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            fireMagic.castSpell();
        }else if(Input.GetButtonDown("Fire3")){
            playerPos = GameObject.FindWithTag("Player").transform.position;
            earthMagic.castSpell();
        }
    }
    private void setMagicCore(){
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

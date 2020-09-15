using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSummon : MonoBehaviour
{
    public const int LIQUID = 0; public const int SHIELD = 1; public const int SWORD = 2;
    public const int LEFT = -1; public const int RIGHT = 1;
    public int form; // 0 is liquid. 1 is shield. 2 is sword.
    public Sprite[] sprites;
    public GameObject player;
    public int metalRotation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().rotateEvent += OnRotateEvent;
        form = 1;
        metalRotation = LEFT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void castSpell(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
            if(form == SHIELD)
                SwitchSides();
            if(form == SWORD)
                Attack();
        }else if(vertical > 0){
            ChangeForm(SWORD);
            form = SWORD;
        }else if(vertical < 0){
            ChangeForm(SHIELD);
            form = SHIELD;
        }
    }
    void OnDestroy(){
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().rotateEvent -= OnRotateEvent;
    }
    public void ChangeForm(int form){ //Switch to a different metal form (liquid, shield, sword)
        GetComponent<SpriteRenderer>().sprite = sprites[form];
        switch(form){
            case LIQUID:
                GetComponent<Collider2D>().enabled = false;
                Debug.Log("Metal changed to liquid");
                break;
            case SHIELD:
                GetComponent<Collider2D>().enabled = true;
                Debug.Log("Metal changed to shield");
                break;
            case SWORD:
                GetComponent<Collider2D>().enabled = true;
                Debug.Log("Metal changed to sword");
                break;
        }
    }
    public void ReturnToPlayer(){ //Lerp the metal back to the player

    }
    public void Attack(){
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0){
            transform.Translate(Vector2.left*Time.deltaTime);
        }else if(horizontal > 0){
            transform.Translate(Vector2.right*Time.deltaTime);
        }
    }
    //Switch the shield to the direction indicated by the horizontal axis
    public void SwitchSides(){
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0){
            transform.rotation = Quaternion.Euler(0,180,0);
            transform.localPosition = new Vector3(transform.localPosition.x*(-1),transform.localPosition.y,transform.localPosition.z);
            metalRotation = LEFT;
            Debug.Log("Metal switched to left side");
        }else if(horizontal > 0){
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.localPosition = new Vector3(transform.localPosition.x*(-1),transform.localPosition.y,transform.localPosition.z);
            metalRotation = RIGHT;
            Debug.Log("Metal switched to right side");
        }
    }
    void OnRotateEvent(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(metalRotation == LEFT){
            transform.rotation = Quaternion.Euler(0,180,0);
        }else if(metalRotation == RIGHT){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        transform.localPosition = new Vector3(transform.localPosition.x*(-1),transform.localPosition.y,transform.localPosition.z);
        Debug.Log("Rotated Metal");
    }
}

using System;
using System.Collections;
using UnityEngine;

public class MetalSummon : MonoBehaviour
{
    private const int Liquid = 0; private const int Shield = 1; private const int Sword = 2;
    private const int Left = 180; private const int Right = 0;
    public int currentForm; // 0 is liquid. 1 is shield. 2 is sword.
    public Sprite[] sprites;
    public GameObject player;
    public int metalRotation;
    private SpriteRenderer spriteRenderer;
    public int healthPoints;
    private PlayerInput playerInput;
    private Rigidbody2D rigidBody;
    private Vector3 shieldPos;
    
    
    private Collider2D col;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerInput>().RotateEvent += OnRotateEvent;
        currentForm = Shield;
        metalRotation = Right;
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        playerInput = player.GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        shieldPos = transform.localPosition;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0,metalRotation,0);
    }
    public void CastSpell(){
        var horizontal = playerInput.playerInputActions.Player.TapAttack.ReadValue<Vector2>().x;
        var attackDirection = playerInput.playerInputActions.Player.TapAttack.ReadValue<Vector2>();
        if (attackDirection.Equals(Vector2.left) || attackDirection.Equals(Vector2.right))
        {
            if ((horizontal < 0 && metalRotation == Right) || (horizontal > 0 && metalRotation == Left)) {
                SwitchSides(horizontal);
            }else {
                Attack();
            }
        }else if (attackDirection.Equals(Vector2.down) && currentForm != Shield) {
            ReturnToPlayer();
            ChangeForm(Shield);
        }else if (attackDirection.Equals(Vector2.up)){
            
        }
    }
    private void OnDestroy(){
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().RotateEvent -= OnRotateEvent;
    }
    private void ChangeForm(int form){ //Switch to a different metal form (liquid, shield, sword)
        spriteRenderer.sprite = sprites[form];
        spriteRenderer.enabled = true;
        currentForm = form;
        switch(form){
            case Liquid:
                col.enabled = false;
                Debug.Log("Metal changed to liquid");
                break;
            case Shield:
                col.enabled = true;
                Debug.Log("Metal changed to shield");
                break;
            case Sword:
                col.enabled = true;
                Debug.Log("Metal changed to sword");
                break;
        }
    }
    private void ReturnToPlayer(){ //Reattaches the summon to the player. Updates summon attributes accordingly
        var playerDirection = player.transform.right.x;
        gameObject.transform.parent = player.transform;
        playerInput.RotateEvent += OnRotateEvent;
        rigidBody.velocity = new Vector2(0,0);
        transform.localPosition = new Vector3((-1)*shieldPos.x,shieldPos.y,shieldPos.z);
        gameObject.layer = 13;
        if (playerDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0,Right,0);
            metalRotation = Right;
        }else if(playerDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0,Left,0);
            metalRotation = Left;
        }
    }

    private void Attack(){
        var horizontal = playerInput.playerInputActions.Player.TapAttack.ReadValue<Vector2>().x;
        gameObject.transform.parent = null;
        playerInput.RotateEvent -= OnRotateEvent;
        ChangeForm(Sword);
        gameObject.layer = 11; //Change layer to playerprojectiles
        if(horizontal < 0){
            rigidBody.velocity = new Vector2(-10,0);
        }else if(horizontal > 0){
            rigidBody.velocity = new Vector2(10,0);
        }
    }

    
    //Switch the shield to the direction indicated by the horizontal axis. Changes the sign of the local position of the metal summon if the shield is on the opposite side.
    private void SwitchSides(float horizontal)
    {
        var position = transform.localPosition;
        if(horizontal < 0)
        {
            playerInput.LockInput();
            Transform transform1;
            (transform1 = transform).rotation = Quaternion.Euler(0,Left,0);
            transform1.localPosition = new Vector3((-1)*shieldPos.x,position.y,position.z);
            metalRotation = Left;
            Debug.Log("Metal switched to left side");
            playerInput.UnlockInput();
        }else if(horizontal > 0)
        {
            playerInput.LockInput();
            Transform transform1;
            (transform1 = transform).rotation = Quaternion.Euler(0,Right,0);
            transform1.localPosition = new Vector3((-1)*shieldPos.x,position.y,position.z);
            metalRotation = Right;
            Debug.Log("Metal switched to right side");
            playerInput.UnlockInput();
        }
    }

    private void OnRotateEvent(){
        var position = transform.localPosition;
        transform.localPosition = new Vector3(position.x*(-1),position.y,position.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (currentForm)
        {
            case Shield:
                ShieldCollision(collision);
                break;
            case Sword:
                SwordCollision(collision);
                break;
        }
    }

    private void ShieldCollision(Collision2D collision)
    {
        healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
        if (healthPoints > 0) return;
        //When health points reach zero delete shield
        ChangeForm(Liquid);
        spriteRenderer.enabled = false;
    }
    private void SwordCollision(Collision2D collision)
    {
        col.enabled = false;
    }
}

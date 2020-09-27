using UnityEngine;

public class MetalSummon : MonoBehaviour
{
    private const int Liquid = 0; private const int Shield = 1; private const int Sword = 2;
    private const int Left = -1; private const int Right = 1;
    public int currentForm; // 0 is liquid. 1 is shield. 2 is sword.
    public Sprite[] sprites;
    public GameObject player;
    public int metalRotation;
    private SpriteRenderer spriteRenderer;
    public int healthPoints;
    private int previous; 
    
    private Collider2D col;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().RotateEvent += OnRotateEvent;
        currentForm = Shield;
        previous = Left;
        metalRotation = Left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void CastSpell(){
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
            if(currentForm == Shield)
                SwitchSides();
            if(currentForm == Sword)
                Attack();
        }else if(vertical > 0){
            ChangeForm(Sword);
        }else if(vertical < 0)
        {
            ChangeForm(Shield);
        }
    }

    private void OnDestroy(){
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().RotateEvent -= OnRotateEvent;
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
    public void ReturnToPlayer(){ //Lerp the metal back to the player

    }
    public void Attack(){
        var horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0){
            transform.Translate(Vector2.left*Time.deltaTime);
        }else if(horizontal > 0){
            transform.Translate(Vector2.right*Time.deltaTime);
        }
    }
    //Switch the shield to the direction indicated by the horizontal axis
    public void SwitchSides(){
        var horizontal = Input.GetAxis("Horizontal");
        var position = transform.localPosition;
        if(horizontal < 0 && previous > 0){
            Transform transform1;
            (transform1 = transform).rotation = Quaternion.Euler(0,180,0);
            transform1.localPosition = new Vector3(position.x*(-1),position.y,position.z);
            metalRotation = Left;
            previous = Left;
            Debug.Log("Metal switched to left side");
        }else if(horizontal > 0 && previous < 0){
            Transform transform1;
            (transform1 = transform).rotation = Quaternion.Euler(0,0,0);
            transform1.localPosition = new Vector3(position.x*(-1),position.y,position.z);
            metalRotation = Right;
            previous = Right;
            Debug.Log("Metal switched to right side");
        }
    }

    private void OnRotateEvent(){
        var horizontal = Input.GetAxisRaw("Horizontal");
        var position = transform.localPosition;
        switch (metalRotation)
        {
            case Left:
                transform.rotation = Quaternion.Euler(0,180,0);
                break;
            case Right:
                transform.rotation = Quaternion.Euler(0,0,0);
                break;
        }
        transform.localPosition = new Vector3(position.x*(-1),position.y,position.z);
        Debug.Log("Rotated Metal");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        healthPoints -= collision.gameObject.GetComponent<Projectile>().damagePoints;
        if (healthPoints > 0) return;
        //When health points reach zero delete shield
        ChangeForm(Liquid);
        spriteRenderer.enabled = false;
    }
}

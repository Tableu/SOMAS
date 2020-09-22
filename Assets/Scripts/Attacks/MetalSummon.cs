using UnityEngine;

public class MetalSummon : MonoBehaviour
{
    public const int Liquid = 0; public const int Shield = 1; public const int Sword = 2;
    public const int Left = -1; public const int Right = 1;
    public int currentForm; // 0 is liquid. 1 is shield. 2 is sword.
    public Sprite[] sprites;
    public GameObject player;
    public int metalRotation;
    private SpriteRenderer spriteRenderer;

    private Collider2D col;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().RotateEvent += OnRotateEvent;
        currentForm = 1;
        metalRotation = Left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void CastSpell(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player = GameObject.FindWithTag("Player");
        if(horizontal != 0){
            if(currentForm == Shield)
                SwitchSides();
            if(currentForm == Sword)
                Attack();
        }else if(vertical > 0){
            ChangeForm(Sword);
            currentForm = Sword;
        }else if(vertical < 0){
            ChangeForm(Shield);
            currentForm = Shield;
        }
    }

    private void OnDestroy(){
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().RotateEvent -= OnRotateEvent;
    }
    private void ChangeForm(int form){ //Switch to a different metal form (liquid, shield, sword)
        spriteRenderer.sprite = sprites[form];
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
        Vector3 position = transform.localPosition;
        if(horizontal < 0){
            Transform transform1;
            (transform1 = transform).rotation = Quaternion.Euler(0,180,0);
            transform1.localPosition = new Vector3(position.x*(-1),position.y,position.z);
            metalRotation = Left;
            Debug.Log("Metal switched to left side");
        }else if(horizontal > 0){
            Transform transform1;
            (transform1 = transform).rotation = Quaternion.Euler(0,0,0);
            transform1.localPosition = new Vector3(position.x*(-1),position.y,position.z);
            metalRotation = Right;
            Debug.Log("Metal switched to right side");
        }
    }

    private void OnRotateEvent(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 position = transform.localPosition;
        if(metalRotation == Left){
            transform.rotation = Quaternion.Euler(0,180,0);
        }else if(metalRotation == Right){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        transform.localPosition = new Vector3(transform.localPosition.x*(-1),position.y,position.z);
        Debug.Log("Rotated Metal");
    }
}

using UnityEngine;

public class EnemyRaycasts : MonoBehaviour
{
    private Collider2D col;
    public int mask;
    public bool grounded;
    public float speed;
    //private Vector2[] positions;
    private int raycastIndex;
    //Raycasts
    public Vector2[] raycastDirections;
    public RaycastHit2D[] raycastArray;
    //Boundaries
    public float[] boundary;
    public float attackLimit;
    public float followLimit;
    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider2D>();
        var spawner = transform.parent.GetComponent<Spawner>();
        boundary = new float[2];
        boundary[0] = spawner.leftBoundary.position.x;
        boundary[1] = spawner.rightBoundary.position.x;
        raycastArray = new RaycastHit2D[raycastDirections.Length];
        mask = LayerMask.GetMask("Player", "Platforms");
        grounded = false;
    }

    // Update is called once per frame
    private void Update()
    {
        DrawRaycasts(raycastArray,ref raycastIndex, raycastDirections, 5f, mask, col.bounds.center);
    }

    private void DrawRaycasts(RaycastHit2D[] raycastArray, ref int rayIndex, Vector2[] direction, float raycastLength, int layerMask, Vector2 position){
        for(var index = 0; index < raycastArray.Length; index++){
            raycastArray[index] = Physics2D.Raycast(position, direction[index], raycastLength, layerMask);
            Debug.DrawRay(position, transform.TransformDirection(direction[index])*raycastLength,Color.red);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = false;
        }
    }
}

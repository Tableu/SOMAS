using UnityEngine;

public class PlayerRaycasts : MonoBehaviour
{
    private RaycastHit2D[] forwardRaycasts = new RaycastHit2D[3];
    private RaycastHit2D[] downRaycasts = new RaycastHit2D[3];
    private int forwardRayIndex = 0;
    private int downRayIndex = 0;
    private Vector2[] downRaycastPos;
    private Vector2[] forwardRaycastPos;
    private Collider2D col;
    private PlayerData playerData;
    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider2D>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetRaycastPositions();
        DrawRaycasts(forwardRaycasts, ref forwardRayIndex, playerData.forward, 1f, LayerMask.GetMask("Platforms"), forwardRaycastPos);
        DrawRaycasts(downRaycasts, ref downRayIndex, Vector2.down, 0.1f, LayerMask.GetMask("Platforms"), downRaycastPos);
        SetGrounded();
        SetForwardRaycastHit();
    }
    private void SetForwardRaycastHit(){
        if(forwardRaycasts[0] || forwardRaycasts[1] || forwardRaycasts[2]){
            playerData.forwardRaycastHit = true;
        }else{
            playerData.forwardRaycastHit = false;
        }
    }
    private void SetGrounded(){
        if(downRaycasts[downRayIndex] && downRaycasts[downRayIndex].transform.CompareTag("Platform")){
            playerData.grounded = true;
        }else{
            playerData.grounded = false;
        }
    }
    private void SetRaycastPositions(){
        var colliderBounds = new Bounds(gameObject.transform.position - playerData.boundCenterOffset,playerData.boundSize);
        downRaycastPos = new Vector2[] {new Vector2(colliderBounds.center.x, colliderBounds.min.y),
                                        new Vector2(colliderBounds.max.x, colliderBounds.min.y),
                                        colliderBounds.min};
        if(playerData.forward.x == 1){
            forwardRaycastPos = new Vector2[] {colliderBounds.max,
                                         new Vector2(colliderBounds.max.x,colliderBounds.center.y),
                                         new Vector2(colliderBounds.max.x,colliderBounds.min.y)};
        }else{
            forwardRaycastPos = new Vector2[] {colliderBounds.min,
                                        new Vector2(colliderBounds.min.x,colliderBounds.center.y), 
                                        new Vector2(colliderBounds.min.x,colliderBounds.max.y)};
        }
    }
    private void DrawRaycasts(RaycastHit2D[] raycastArray, ref int rayIndex, Vector2 direction, float raycastLength, int layerMask, Vector2[] positions){
        for(var index = 0; index < raycastArray.Length; index++){
            raycastArray[index] = Physics2D.Raycast(positions[index], direction, raycastLength, layerMask);
            Debug.DrawRay(positions[index], direction*raycastLength,Color.red);
        }

        for(var index = 0; index < raycastArray.Length; index++){
            if(raycastArray[index]){
                rayIndex = index;
                break;
            }
        }
    }
}

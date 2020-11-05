using UnityEngine;

public class PlayerRaycasts
{
    public static bool Grounded(Vector3 position, Vector3 boundCenterOffset, Vector3 boundSize)
    {
        var downRaycasts = new RaycastHit2D[3];
        var downRayIndex = 0;
        var colliderBounds = new Bounds(position - boundCenterOffset,boundSize);
        var downRaycastPos = new Vector2[] {new Vector2(colliderBounds.center.x, colliderBounds.min.y),
            new Vector2(colliderBounds.max.x, colliderBounds.min.y),
            colliderBounds.min};
        DrawRaycasts(downRaycasts, ref downRayIndex, Vector2.down, 0.1f, LayerMask.GetMask("Platforms"), downRaycastPos);
        if(downRaycasts[downRayIndex] && downRaycasts[downRayIndex].transform.CompareTag("Platform"))
            return true;
        
        return false;
    }

    public static bool ForwardHit(Transform transform, Vector3 boundCenterOffset, Vector3 boundSize, Vector2 forward)
    {
        var forwardRaycasts = new RaycastHit2D[3];
        Vector2[] forwardRaycastPos;
        var colliderBounds = new Bounds(transform.position - boundCenterOffset,boundSize);
        var forwardRayIndex = 0;
        if(forward.Equals(Vector2.right)){
            forwardRaycastPos = new Vector2[] {colliderBounds.max,
                new Vector2(colliderBounds.max.x,colliderBounds.center.y),
                new Vector2(colliderBounds.max.x,colliderBounds.min.y)};
            DrawRaycasts(forwardRaycasts, ref forwardRayIndex,Vector2.right, 1f, LayerMask.GetMask("Platforms"), forwardRaycastPos);
        }else{
            forwardRaycastPos = new Vector2[] {colliderBounds.min,
                new Vector2(colliderBounds.min.x,colliderBounds.center.y), 
                new Vector2(colliderBounds.min.x,colliderBounds.max.y)};
            DrawRaycasts(forwardRaycasts, ref forwardRayIndex, Vector2.left, 1f, LayerMask.GetMask("Platforms"), forwardRaycastPos);
        }
        if(forwardRaycasts[0] || forwardRaycasts[1] || forwardRaycasts[2])
            return true; 
        return false;
    }
    private static void DrawRaycasts(RaycastHit2D[] raycastArray, ref int rayIndex, Vector2 direction, float raycastLength, int layerMask, Vector2[] positions){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycasts : MonoBehaviour
{
    private Collider2D col;
    private EnemyData enemyData;
    private Vector2[] positions;
    private int raycastIndex;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        enemyData = GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        drawRaycasts(enemyData.raycastArray,ref raycastIndex, enemyData.raycastDirections, 5f, enemyData.mask, col.bounds.center);
    }

    private void drawRaycasts(RaycastHit2D[] raycastArray, ref int rayIndex, Vector2[] direction, float raycastLength, int layerMask, Vector2 position){
        for(int index = 0; index < raycastArray.Length; index++){
            raycastArray[index] = Physics2D.Raycast(position, direction[index], raycastLength, layerMask);
            Debug.DrawRay(position, transform.TransformDirection(direction[index])*raycastLength,Color.red);
        }
    }
}

using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement
{
    private static Type t = typeof(EnemyMovement);
    public static void InvokeMethod(string methodName, List<object> args){
        t.GetMethod(methodName)?.Invoke(null, args.ToArray());
    }
    public static void Walk(GameObject gameObject){
        Transform transform = gameObject.GetComponent<Transform>();
        EnemyData enemyData = gameObject.GetComponent<EnemyData>();
        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        
        if(transform.position.x > enemyData.boundary[1]){
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            enemyData.forward = Vector2.left;
        }else if(transform.position.x < enemyData.boundary[0]){
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            enemyData.forward = Vector2.right;
        }
        rigidBody.velocity = new Vector2(enemyData.forward.x*enemyData.speed,rigidBody.velocity.y);
    }
    public static void FlipCharacter(GameObject gameObject){
        EnemyData enemyData = gameObject.GetComponent<EnemyData>();
        Transform transform = gameObject.GetComponent<Transform>();
        Quaternion localRotation = transform.localRotation;
        if(enemyData.distanceToPlayer.x < 0){
            localRotation = Quaternion.Euler(localRotation.x, 0, localRotation.z);
            transform.localRotation = localRotation;
            enemyData.forward = Vector2.left;
        }else if(enemyData.distanceToPlayer.x > 0){
            localRotation = Quaternion.Euler(localRotation.x, 180, localRotation.z);
            transform.localRotation = localRotation;
            enemyData.forward = Vector2.right;
        }
    }

    public static void StopCharacter(GameObject gameObject){
        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        
        rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
    }

    public static void FollowPlayer(GameObject gameObject){
        EnemyData enemyData = gameObject.GetComponent<EnemyData>();
        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Vector2 distanceToPlayer = enemyData.distanceToPlayer;
        
        distanceToPlayer = new Vector2(distanceToPlayer.x,0).normalized;
        rigidBody.velocity = new Vector2(distanceToPlayer.x*enemyData.speed, rigidBody.velocity.y);
    }
}

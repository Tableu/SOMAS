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
        var transform = gameObject.GetComponent<Transform>();
        var enemyRaycasts = gameObject.GetComponent<EnemyRaycasts>();
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        
        if(transform.position.x > enemyRaycasts.boundary[1]){
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }else if(transform.position.x < enemyRaycasts.boundary[0]){
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Mathf.Abs(rigidBody.velocity.x) < enemyRaycasts.speed) {
            rigidBody.AddForce(new Vector2(gameObject.transform.right.x * enemyRaycasts.speed, 0), ForceMode2D.Impulse);
        }
    }
    public static void FlipCharacter(GameObject gameObject){
        var enemyDetection = gameObject.GetComponent<EnemyDetection>();
        var transform = gameObject.GetComponent<Transform>();
        var localRotation = transform.localRotation;
        if(enemyDetection.distanceToPlayer.x < 0){
            localRotation = Quaternion.Euler(localRotation.x, 180, localRotation.z);
            transform.localRotation = localRotation;
        }else if(enemyDetection.distanceToPlayer.x > 0){
            localRotation = Quaternion.Euler(localRotation.x, 0, localRotation.z);
            transform.localRotation = localRotation;
        }
    }

    public static void StopCharacter(GameObject gameObject){
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if(Mathf.Abs(rigidBody.velocity.x) > 0)
            rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
    }

    public static void FollowPlayer(GameObject gameObject){
        var enemyDetection = gameObject.GetComponent<EnemyDetection>();
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var distanceToPlayer = enemyDetection.distanceToPlayer;
        
        distanceToPlayer = new Vector2(distanceToPlayer.x,0).normalized;
        if (Mathf.Abs(rigidBody.velocity.x) < enemyDetection.speed) {
            rigidBody.AddForce(new Vector2(distanceToPlayer.x * enemyDetection.speed, 0), ForceMode2D.Impulse);
        }
    }
}

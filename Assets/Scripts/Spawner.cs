using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform leftBoundary;
    public Transform rightBoundary;
    
    void Awake(){
        leftBoundary = transform.GetChild(0).transform;
        rightBoundary = transform.GetChild(1).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float startingY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > startingY || transform.position.y < startingY){
            transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
        }
    }
}

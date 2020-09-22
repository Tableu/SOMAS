using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float startingY;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 position = transform.position;
        if(position.y > startingY || position.y < startingY){
            transform.position = new Vector3(position.x, startingY, position.z);
        }
    }
}

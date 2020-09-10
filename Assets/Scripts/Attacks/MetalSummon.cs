using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSummon : MonoBehaviour
{
    public int form;
    public Sprite liquid;
    public Sprite shield;
    public Sprite sword;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().rotateEvent += OnRotateEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy(){
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().rotateEvent -= OnRotateEvent;
    }
    public void ChangeForm(int form){ //Switch to a different metal form (offence, defense, liquid)

    }
    public void ReturnToPlayer(){ //Lerp the metal back to the player

    }
    public void Attack(){
        
    }
    void OnRotateEvent(){
        transform.rotation = Quaternion.Euler(0,180,0);
        transform.localPosition = new Vector3(transform.localPosition.x*(-1),transform.localPosition.y,transform.localPosition.z);
        Debug.Log("Rotated Metal");
    }
}

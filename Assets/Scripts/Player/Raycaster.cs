using UnityEngine;

namespace Player {
    public class Raycaster : MonoBehaviour
    {
        public Vector2 direction;
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            var raycastPos = new Vector2(gameObject.transform.position.x + 7.65f,gameObject.transform.position.y - 2.27f);
            Debug.DrawRay(raycastPos, transform.TransformDirection(direction)*7,Color.red);
            if(Physics2D.Raycast(raycastPos,direction,7) == false){
                Destroy(gameObject);
            }
        }
    }
}

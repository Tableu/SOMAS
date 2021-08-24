using Player;
using UnityEngine;

namespace Level {
    public class CameraScript : MonoBehaviour
    {
        private Vector3 velocity;
        public Transform player;
        public float horizontalFollowDistance;
        public float verticalFollowDistance;
        private float horizontalOffset;
        public float[] horizontalLimits = new float[2];
        public float[] verticalLimits = new float[2];
        public Transform leftBoundary;
        public Transform rightBoundary;
        // Start is called before the first frame update
        private void Awake(){
            leftBoundary = transform.GetChild(1).transform;
            rightBoundary = transform.GetChild(2).transform;
        }

        private void Start()
        {
            horizontalLimits[0] = leftBoundary.position.x;
            verticalLimits[0] = leftBoundary.position.y;
            horizontalLimits[1] = rightBoundary.position.x;
            verticalLimits[1] = rightBoundary.position.y;
            velocity = Vector3.zero;
            GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().DeathEvent += OnDeathEvent;
        }

        private void OnDeathEvent(){
            GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().DeathEvent -= OnDeathEvent;
            gameObject.GetComponent<CameraScript>().enabled = false;
        }
        // Update is called once per frame
        private void LateUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            var position = transform.position;
            var playerPosition = player.position;
            var horizontalDist = position.x - playerPosition.x;
            var verticalDist = position.y - playerPosition.y;
            Vector3 target;
            if(Mathf.Abs(horizontalDist) > horizontalFollowDistance){    
                target = new Vector3(playerPosition.x, position.y, position.z);
                if(playerPosition.x < horizontalLimits[0]){
                    target = new Vector3(horizontalLimits[0], position.y, position.z);
                }else if(playerPosition.x > horizontalLimits[1]){
                    target = new Vector3(horizontalLimits[1], position.y, position.z);
                }
                transform.position = Vector3.SmoothDamp(transform.position, target,ref velocity, 0.1F);
            }
            if(Mathf.Abs(verticalDist) > verticalFollowDistance){
                target = new Vector3(position.x, playerPosition.y,position.z);
                if(playerPosition.y < verticalLimits[0]){
                    target = new Vector3(position.x, verticalLimits[0], position.z);
                }else if(playerPosition.y > verticalLimits[1]){
                    target = new Vector3(position.x, verticalLimits[1], position.z);
                }
                transform.position = Vector3.SmoothDamp(transform.position, target,ref velocity, 0.3F);
            }
        }

        private void OnEnable(){
            GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().DeathEvent += OnDeathEvent;
        }
    }
}

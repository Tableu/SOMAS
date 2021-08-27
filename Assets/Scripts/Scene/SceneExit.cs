using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Level {
    public class SceneExit : MonoBehaviour {
        public String nextScene;
        public Vector3 nextPosition;
        public CinemachineVirtualCamera virtualCamera;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Destroy(GameObject.FindWithTag("MainCamera"));
            Destroy(GameObject.FindWithTag("Canvas"));
            virtualCamera.enabled = false;
            GameObject.FindWithTag("SceneLoad").GetComponent<SceneLoad>().playerPos = nextPosition;
            SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("Player"), SceneManager.GetSceneByName(nextScene));
            SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("SceneLoad"), SceneManager.GetSceneByName(nextScene));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
        }
    }
}

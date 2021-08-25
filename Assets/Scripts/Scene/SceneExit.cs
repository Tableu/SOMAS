using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level {
    public class SceneExit : MonoBehaviour {
        public String nextScene;
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
            //Destroy(GameObject.FindWithTag("Player"));
            SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("Player"), SceneManager.GetSceneByName(nextScene));
            SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("SceneLoad"), SceneManager.GetSceneByName(nextScene));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
        }
    }
}

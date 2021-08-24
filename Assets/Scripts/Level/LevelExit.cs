using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level {
    public class LevelExit : MonoBehaviour {
        public String nextLevel;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D other) {
            SceneManager.LoadScene(nextLevel);
        }
    }
}

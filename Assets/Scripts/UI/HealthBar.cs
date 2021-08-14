using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthBar : MonoBehaviour {
        private PlayerHealth playerHealth;
        private Slider slider;
        // Start is called before the first frame update
        void Start()
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update() {
            slider.value = playerHealth.healthPoints*0.01f;
        }
    }
}

using UnityEngine;

namespace Player {
    public class PlayerPrefs : MonoBehaviour {
        private PlayerHealth playerHealth;
        private PlayerInput playerInput;
        // Start is called before the first frame update
        void Start() {
            playerHealth = GetComponent<PlayerHealth>();
            playerInput = GetComponent<PlayerInput>();
            playerHealth.healthPoints = UnityEngine.PlayerPrefs.GetInt("Health");
            playerInput.manaPoints = UnityEngine.PlayerPrefs.GetFloat("Mana");
        }
    
        void OnDestroy() {
            UnityEngine.PlayerPrefs.SetInt("Health", playerHealth.healthPoints);
            UnityEngine.PlayerPrefs.SetFloat("Mana", playerInput.manaPoints);
        }
    }
}

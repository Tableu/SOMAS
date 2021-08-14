using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ManaBar : MonoBehaviour
    {
        private PlayerInput playerInput;
        private Slider slider;
        // Start is called before the first frame update
        void Start()
        {
            playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update() {
            slider.value = playerInput.manaPoints*0.01f;
        }
    }
}

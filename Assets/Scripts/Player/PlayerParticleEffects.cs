using UnityEngine;

namespace Player {
    public class PlayerParticleEffects : MonoBehaviour
    {
        // Start is called before the first frame update
        private PlayerInput playerInput;
        private PlayerInputActions playerInputActions;
        private bool waterCharge;
        public GameObject iceCrystal;
        public GameObject crystalHubPrefab;
        public Vector3[] crystalPositions;
        private GameObject crystalHub;
        private int timer;
        private int crystalIndex;
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInputActions = playerInput.playerInputActions;
            waterCharge = false;
            playerInputActions.Player.BasicSpell.started += context => {
                if (playerInputActions.Player.HoldAttack.ReadValue<Vector2>().Equals(Vector2.zero)) {
                    waterCharge = true;
                    timer = 0;
                    crystalIndex = 0;
                    crystalHub = Instantiate(crystalHubPrefab, transform);
                }
            };
            playerInputActions.Player.BasicSpell.canceled += context => {
                waterCharge = false;
                Destroy(crystalHub);
            };
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (waterCharge) {
                if (timer == 20) {
                    if (crystalIndex >= crystalPositions.Length)
                        return;
                    var crystal = Instantiate(iceCrystal, crystalHub.transform);
                    crystal.transform.rotation = Quaternion.Euler(0,0,90);
                    crystal.transform.localScale = new Vector3(2.5f,2.5f,2.5f);
                    crystal.transform.localPosition = crystalPositions[crystalIndex];
                    crystalIndex++;
                    timer = 0;
                }
                else {
                    timer++;
                }
            }
        }
    }
}

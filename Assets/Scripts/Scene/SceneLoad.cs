using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {
    public GameObject Canvas;
    public GameObject Camera;
    public Vector3 playerPos;
    // Start is called before the first frame update
    void Awake() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        DontDestroyOnLoad(gameObject);
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        Instantiate(Canvas, playerPos, Quaternion.identity);
        Instantiate(Camera, new Vector3(playerPos.x,playerPos.y,-100), Quaternion.identity);
        GameObject.FindWithTag("Player").transform.position = playerPos;
        DontDestroyOnLoad(GameObject.FindWithTag("Player"));
        DontDestroyOnLoad(gameObject);
    }
}

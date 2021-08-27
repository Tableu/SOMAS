using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableVirtualCam : MonoBehaviour {
    public CinemachineVirtualCamera virtualCamera;

    void Awake() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    private void ChangedActiveScene(Scene current, Scene next) {
        if (current == gameObject.scene) return;
        virtualCamera.enabled = true;
        virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Follow = GameObject.FindWithTag("Player").transform;
    }
}

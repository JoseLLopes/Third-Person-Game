using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ThirdPersonGame.PlayerMovement;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] Transform cameraFollow;
    [SerializeField] Transform cameraLookAt;

    private void Start() {
        
        var playerCamObj = FindObjectOfType<CinemachineCollider>();
        Debug.Log(playerCamObj.gameObject.name);
        CinemachineFreeLook cinemachineFreeLook = playerCamObj.GetComponent<CinemachineFreeLook>();
        cinemachineFreeLook.Follow = cameraFollow;
        cinemachineFreeLook.LookAt = cameraLookAt;
        Camera.main.gameObject.SetActive(true);
        gameObject.GetComponent<PlayerController>().playerCamera = Camera.main.transform;
    }
}

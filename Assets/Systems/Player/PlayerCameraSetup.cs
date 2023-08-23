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
        var playerCamObj = Instantiate(playerCamera,Vector3.zero,default);
        CinemachineFreeLook cinemachineFreeLook = playerCamObj.GetComponent<CinemachineFreeLook>();
        cinemachineFreeLook.Follow = cameraFollow;
        cinemachineFreeLook.LookAt = cameraLookAt;
        gameObject.GetComponent<PlayerController>().playerCamera = Camera.main.transform;
    }
}

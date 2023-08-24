using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.InputSystem;
using UnityEngine;

public class PlayerAudioPlay : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpAudio;

    [SerializeField] PlayerInputEvents playerInputEvents;

    private void Start() {
        playerInputEvents.OnReleaseJump += playJumpAudio;
    }

    private void OnDisable() {
        playerInputEvents.OnReleaseJump -= playJumpAudio;
    }

    private void playJumpAudio()
    {
        audioSource.PlayOneShot(jumpAudio);
    }
}

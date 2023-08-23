using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ThirdPersonGame.InputSystem;
using System;

public class NetworkAnimRPC : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    PlayerInputEvents playerInputEvents;
    [SerializeField] Animator animator;

    private void Start() {
        if(photonView.IsMine){
            playerInputEvents = FindObjectOfType<PlayerInputEvents>();
            playerInputEvents.OnHoldJump += HoldJumpAnimation;
            playerInputEvents.OnReleaseJump += ReleaseJumpAnimation;
        }
    }

    private void ReleaseJumpAnimation()
    {
        if(photonView.IsMine){
            photonView.RPC("RPC_PlayReleaseJumpAnim", RpcTarget.Others);
        }
    }

    private void HoldJumpAnimation()
    {
        if(photonView.IsMine){
            photonView.RPC("RPC_PlayHoldJumpAnim", RpcTarget.Others);
        }
    }

    [PunRPC]
    void RPC_PlayHoldJumpAnim() {
        animator.SetBool("changingJump",true);
    }

    [PunRPC]
    void RPC_PlayReleaseJumpAnim() {
        animator.SetTrigger("releaseJump");
        animator.SetBool("changingJump",false);
    }
}

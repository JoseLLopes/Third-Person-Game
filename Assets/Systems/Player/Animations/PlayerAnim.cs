using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.InputSystem;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator animator;
    PlayerInputEvents playerInputEvents;

    private void Start() {
        playerInputEvents = FindObjectOfType<PlayerInputEvents>();
        playerInputEvents.OnHoldJump += HoldJumpAnimation;
        playerInputEvents.OnReleaseJump += ReleaseJumpAnimation;
    }

    void ReleaseJumpAnimation(){
        animator.SetTrigger("releaseJump");
        animator.SetBool("changingJump",false);
    }

    void HoldJumpAnimation(){
        animator.SetBool("changingJump",true);
    }

}

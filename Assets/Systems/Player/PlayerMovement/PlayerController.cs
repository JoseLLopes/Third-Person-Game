using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.InputSystem;
using System;
using UnityEngine.EventSystems;

namespace ThirdPersonGame.PlayerMovement{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] Animator animator;
        Rigidbody myRigidbody;
        private PlayerInputEvents playerInputEvents;
        Camera mainCamera;


        [Header ("Movement")]
        [SerializeField] float movementSpeed;

        [Header ("Jump")]
        [SerializeField] float chargeSpeed;
        [SerializeField] float jumpMinForce;
        [SerializeField] float jumpMaxForce;
        float currentJumpForce;
        bool isChargingJump;
        
        [Header("Particles")]
        [SerializeField] GameObject jumpParticle;
        

        //UTILS
        MoveDirectionCalculator MoveDirectionCalculator;



        void Awake()
        {
            MoveDirectionCalculator = new MoveDirectionCalculator(); 
            playerInputEvents = GetComponent<PlayerInputEvents>();
            myRigidbody = GetComponent<Rigidbody>();
            playerInputEvents = GetComponent<PlayerInputEvents>();

            mainCamera = Camera.main;
            
        }

        private void Start() 
        {
            playerInputEvents.OnJump += ChargeJump;
            playerInputEvents.OnReleaseJump += HandleJump;
        }

        private void OnDisable()
        {
            playerInputEvents.OnJump -= ChargeJump;
            playerInputEvents.OnReleaseJump -= HandleJump;
        }

        private void FixedUpdate() {
            
            if (isChargingJump && currentJumpForce < jumpMaxForce)
            {
                currentJumpForce = Mathf.Lerp(currentJumpForce, jumpMaxForce, Time.fixedDeltaTime * chargeSpeed);
                return;
            }

            Vector2 movementInput = playerInputEvents.GetMovementInput();
            Vector3 moveDir = MoveDirectionCalculator.CalculateRelativeMoveDir(mainCamera.transform, movementInput);
            
            if(movementInput != default){
                Move(moveDir);

                Rotate(moveDir);
            }
            
        }

        //MOVE
        private void Move(Vector3 movementDirection){

            Vector3 moveVelocity = new Vector3(movementDirection.x, myRigidbody.velocity.y,movementDirection.z) * movementSpeed;
              myRigidbody.velocity = new Vector3(moveVelocity.x, myRigidbody.velocity.y, moveVelocity.z);
        }

        //ROTATE
        private void Rotate(Vector3 movementDirection){
            Vector3 lookDirection = transform.position + movementDirection;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, movementSpeed * Time.deltaTime);
        }

        //JUMP
        private void HandleJump()
        {
            animator.SetTrigger("releaseJump");
            isChargingJump = false;
            Vector3 jumpVelocity = new Vector3(0,transform.up.y,0) * currentJumpForce;
            myRigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            animator.SetBool("changingJump",false);
        }

        private void ChargeJump()
        {
            isChargingJump = true;
            currentJumpForce = jumpMinForce;
            animator.SetBool("changingJump",true);
        }


    }
}
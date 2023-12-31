using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.InputSystem;
using ThirdPersonGame.PlayerMovement.Utils;

namespace ThirdPersonGame.PlayerMovement{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody myRigidbody;
        private PlayerInputEvents playerInputEvents;
        [HideInInspector] public Transform playerCamera;


        [Header ("Movement")]
        [SerializeField] float movementSpeed;

        [Header ("Jump")]
        [SerializeField] float chargeSpeed;
        [SerializeField] float jumpMinForce;
        [SerializeField] float jumpMaxForce;
        float currentJumpForce;
        bool isChargingJump;

        //UTILS
        MoveDirectionCalculator MoveDirectionCalculator;



        void Awake()
        {
            MoveDirectionCalculator = new MoveDirectionCalculator(); 
            playerInputEvents = GetComponent<PlayerInputEvents>();
            myRigidbody = GetComponent<Rigidbody>();
            playerInputEvents = GetComponent<PlayerInputEvents>();

            
        }

        private void Start() 
        {
            playerInputEvents.OnHoldJump += HandleChargeJump;
            playerInputEvents.OnReleaseJump += HandleHoldJump;
        }

        private void OnDisable()
        {
            playerInputEvents.OnHoldJump -= HandleChargeJump;
            playerInputEvents.OnReleaseJump -= HandleHoldJump;
        }

        private void FixedUpdate() {
            if(!playerCamera)
                return;

            if (isChargingJump && currentJumpForce < jumpMaxForce)
            {
                currentJumpForce = Mathf.Lerp(currentJumpForce, jumpMaxForce, Time.fixedDeltaTime * chargeSpeed);
                return;
            }

            Vector2 movementInput = playerInputEvents.GetMovementInput();
            Vector3 moveDir = MoveDirectionCalculator.CalculateRelativeMoveDir(playerCamera.transform, movementInput);
            
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
        private void HandleHoldJump()
        {
            if(Mathf.Abs(myRigidbody.velocity.y) < 0.1f){
                isChargingJump = false;
                Vector3 jumpVelocity = new Vector3(0,transform.up.y,0) * currentJumpForce;
                myRigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            }
            
        }

        private void HandleChargeJump()
        {
            if(Mathf.Abs(myRigidbody.velocity.y) < 0.1f){
                isChargingJump = true;
                currentJumpForce = jumpMinForce;
            }
            
        }


    }
}
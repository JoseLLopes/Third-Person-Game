using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonGame.InputSystem{
    public class PlayerInputEvents : MonoBehaviour
    {
        PlayerInputActions inputActions;

        //Events
        public event Action OnJump;
        public event Action OnReleaseJump;


        private void Awake() {
            inputActions = new PlayerInputActions();
            inputActions.PlayerActions.Enable();
            inputActions.PlayerActions.Jump.performed += jumpPerformed;
            inputActions.PlayerActions.Jump.canceled += jumpCanceled;
        }

        private void jumpCanceled(InputAction.CallbackContext context)
        {
            if(OnReleaseJump != null)
                OnReleaseJump();
        }

        private void jumpPerformed(InputAction.CallbackContext context)
        {
            
            if(OnJump != null)
                OnJump();
        }

        //Get player input movement
        public Vector2 GetMovementInput()
        {
            return inputActions.PlayerActions.Movement.ReadValue<Vector2>();
        }

        //Get Mouse Axis
        public Vector2 GetRotateAxis(){
            return inputActions.PlayerActions.Rotate.ReadValue<Vector2>();
        }


    }
}
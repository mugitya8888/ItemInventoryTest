using System;
using TestUI;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs2:MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private PlayerInput _playerInput;
        public CanvasInventory2 canvasInventory2;

        private string mapPlayer = "Player";
        private string mapUI = "UI";

        private GameObject bulletSpawnPoint;
        public GameObject bulletPrefab;
        private PlayerInventory playerInventory;

        private void Start()
        {
           
            //canvasInventory2.ShowHidMenuParent(false);
            playerInventory = GetComponent<PlayerInventory>();
            _playerInput = GetComponent<PlayerInput>();
            bulletPrefab = transform.GetChild(0).gameObject;
        }


#if ENABLE_INPUT_SYSTEM       

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook) {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnPause()
        {
            if (canvasInventory2 == null) {
                Debug.Log("canvasInventory2 = null");
                return;
            }

            if (_playerInput.currentActionMap.name == mapPlayer) {

                canvasInventory2.ShowHidMenuParent(true);
                _playerInput.SwitchCurrentActionMap(mapUI);
            }
            else if (_playerInput.currentActionMap.name == mapUI) {               

                canvasInventory2.ShowHidMenuParent(false);                
                _playerInput.SwitchCurrentActionMap(mapPlayer);
            }
        }

        public void OnFire()
        {
            if (EventFlag.GetHasHandgun()) {

                int amount = playerInventory.GetCurrentItemAmount(2);
                if (amount > 0) {
                    Instantiate(bulletPrefab, bulletSpawnPoint.transform);
                    playerInventory.DecleaseBulletsAmount();
                }
                else if (amount == -100) {
                    Debug.Log("不正なインデックス");
                }
                else {
                    Debug.Log("弾がありません");
                }

            }

        }


#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}
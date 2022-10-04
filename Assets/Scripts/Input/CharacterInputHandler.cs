using Metaverse.Camera;
using Metaverse.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Character
{
    public class CharacterInputHandler : MonoBehaviour
    {
        Vector2 moveInputVector = Vector2.zero;
        Vector2 viewInputVector = Vector2.zero;
        bool isJumpButtonPressed = false;
        bool isFireButtonPressed = false;
        bool isGrenadeFireButtonPressed = false;
        bool isRocketLauncherFireButtonPressed = false;

        //Other components
        LocalCameraHandler localCameraHandler;
        CharacterMovementHandler characterMovementHandler;

        // Cursor is locked?
        bool cursorLocked = true;

        private void Awake ()
        {
            localCameraHandler = GetComponentInChildren<LocalCameraHandler> ();
            characterMovementHandler = GetComponent<CharacterMovementHandler> ();
        }

        // Start is called before the first frame update
        void Start ()
        {
            CursorLock (true);
        }

        // Update is called once per frame
        void Update ()
        {

            // Show or not cursor
            if (Input.GetKeyDown (KeyCode.Tab)) {
                CursorLock (!cursorLocked);
            }

            if (!characterMovementHandler.Object.HasInputAuthority || !cursorLocked) {
                // Reset all inputs

                viewInputVector.x = 0;
                viewInputVector.y = 0;

                moveInputVector.x = 0;
                moveInputVector.y = 0;

                isJumpButtonPressed = false;

                isFireButtonPressed = false;

                isRocketLauncherFireButtonPressed = false;

                isGrenadeFireButtonPressed = false;

                localCameraHandler.SetViewInputVector (viewInputVector);

                return;
            }

            //View input
            viewInputVector.x = Input.GetAxis ("Mouse X");
            viewInputVector.y = Input.GetAxis ("Mouse Y") * -1; //Invert the mouse look

            //Move input
            moveInputVector.x = Input.GetAxis ("Horizontal");
            moveInputVector.y = Input.GetAxis ("Vertical");

            //Jump
            if (Input.GetButtonDown ("Jump"))
                isJumpButtonPressed = true;

            //Fire
            if (Input.GetButtonDown ("Fire1"))
                isFireButtonPressed = true;

            //Fire
            if (Input.GetButtonDown ("Fire2"))
                isRocketLauncherFireButtonPressed = true;

            //Throw grenade
            if (Input.GetKeyDown (KeyCode.G))
                isGrenadeFireButtonPressed = true;

            //Set view
            localCameraHandler.SetViewInputVector (viewInputVector);

        }

        public NetworkInputData GetNetworkInput ()
        {
            NetworkInputData networkInputData = new NetworkInputData ();

            //Aim data
            networkInputData.aimForwardVector = localCameraHandler.transform.forward;

            //Move data
            networkInputData.movementInput = moveInputVector;

            //Jump data
            networkInputData.isJumpPressed = isJumpButtonPressed;

            //Fire data
            networkInputData.isFireButtonPressed = isFireButtonPressed;

            //Rocket data
            networkInputData.isRocketLauncherFireButtonPressed = isRocketLauncherFireButtonPressed;

            //Grenade fire data
            networkInputData.isGrenadeFireButtonPressed = isGrenadeFireButtonPressed;

            //Reset variables now that we have read their states
            isJumpButtonPressed = false;
            isFireButtonPressed = false;
            isGrenadeFireButtonPressed = false;
            isRocketLauncherFireButtonPressed = false;

            return networkInputData;
        }

        /// <summary>
        /// Locks or unlock the cursor
        /// </summary>
        /// <param name="locked"></param>
        public void CursorLock (bool locked)
        {
            cursorLocked = locked;

            if (locked) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private void OnApplicationFocus (bool focus)
        {
            if (!focus) {
                CursorLock (false);
            }
            else {
                CursorLock (true);
            }
        }
    }
}

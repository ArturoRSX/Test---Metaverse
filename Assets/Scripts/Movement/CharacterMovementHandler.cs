using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Metaverse.Utilities;
using Metaverse.Character;

namespace Metaverse.Game
{
    public class CharacterMovementHandler : NetworkBehaviour
    {
        bool isRespawnRequested = false;

        // Other components
        NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
        HPHandler hpHandler;
        NetworkInGameMessages networkInGameMessages;
        NetworkPlayer networkPlayer;

        private void Awake ()
        {
            networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom> ();
            hpHandler = GetComponent<HPHandler> ();
            networkInGameMessages = GetComponent<NetworkInGameMessages> ();
            networkPlayer = GetComponent<NetworkPlayer> ();
        }

        public override void FixedUpdateNetwork ()
        {
            if (Object.HasStateAuthority) {
                if (isRespawnRequested) {
                    Respawn ();
                    return;
                }

                // Don't update the clients position when they are dead
                if (hpHandler.isDead)
                    return;
            }

            // Get the input from the network
            if (GetInput (out NetworkInputData networkInputData)) {
                // Rotate the transform according to the client aim vector
                transform.forward = networkInputData.aimForwardVector;

                // Cancel out rotation on X axis as we don't want our character to tilt
                Quaternion rotation = transform.rotation;
                rotation.eulerAngles = new Vector3 (0, rotation.eulerAngles.y, rotation.eulerAngles.z);
                transform.rotation = rotation;

                // Move
                Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
                moveDirection.Normalize ();

                networkCharacterControllerPrototypeCustom.Move (moveDirection);

                // Jump
                if (networkInputData.isJumpPressed)
                    networkCharacterControllerPrototypeCustom.Jump ();

                // Check if we've fallen off the world.
                CheckFallRespawn ();
            }

        }

        /// <summary>
        /// Checks if the player has fall outside the map.
        /// </summary>
        void CheckFallRespawn ()
        {
            if (transform.position.y < -12) {
                if (Object.HasStateAuthority) {
                    Debug.Log ($"{Time.time} Respawn due to fall outside of map at position {transform.position}");

                    networkInGameMessages.SendInGameRPCMessage (networkPlayer.nickName.ToString (), "fell off the world");

                    Respawn ();
                }

            }
        }

        public void RequestRespawn ()
        {
            isRespawnRequested = true;
        }

        /// <summary>
        /// Respawns a player in the desired position.
        /// </summary>
        void Respawn ()
        {
            networkCharacterControllerPrototypeCustom.TeleportToPosition (Utils.GetRandomSpawnPoint ().position);

            hpHandler.OnRespawned ();

            isRespawnRequested = false;
        }

        /// <summary>
        /// Enabled/Disables the character controller.
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetCharacterControllerEnabled (bool isEnabled)
        {
            networkCharacterControllerPrototypeCustom.Controller.enabled = isEnabled;
        }

    }
}

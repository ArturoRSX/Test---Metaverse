using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Metaverse.Game
{
    public class NetworkInGameMessages : NetworkBehaviour
    {
        InGameMessagesUIHander inGameMessagesUIHander;


        /// <summary>
        /// Sends a RPC Message
        /// </summary>
        /// <param name="userNickName"></param>
        /// <param name="message"></param>
        public void SendInGameRPCMessage (string userNickName, string message)
        {
            RPC_InGameMessage ($"<b>{userNickName}</b> {message}");
        }

        /// <summary>
        /// Rpc Call In Game message procedurew
        /// </summary>
        /// <param name="message"></param>
        /// <param name="info"></param>
        [Rpc (RpcSources.StateAuthority, RpcTargets.All)]
        void RPC_InGameMessage (string message, RpcInfo info = default)
        {
            Debug.Log ($"[RPC] InGameMessage {message}");

            if (inGameMessagesUIHander == null)
                inGameMessagesUIHander = NetworkPlayer.Local.localCameraHandler.GetComponentInChildren<InGameMessagesUIHander> ();

            if (inGameMessagesUIHander != null)
                inGameMessagesUIHander.OnGameMessageReceived (message);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;


/// <summary>
/// Controls all the player logic
/// </summary>
public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public TextMeshProUGUI playerNickNameTM;
    public static NetworkPlayer Local { get; set; }
    public Transform playerModel;

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }

    // Remote Client Token Hash
    [Networked] public int token { get; set; }

    bool isPublicJoinMessageSent = false;

    public LocalCameraHandler localCameraHandler;
    public GameObject localUI;

    // Other components
    NetworkInGameMessages networkInGameMessages;

    void Awake()
    {
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            // Sets the layer of the local players model
            Utils.SetRenderLayerInChildren(playerModel, LayerMask.NameToLayer("LocalPlayerModel"));

            // Disable main camera
            if (Camera.main != null)
                Camera.main.gameObject.SetActive(false);

            // Enable 1 audio listner
            AudioListener audioListener = GetComponentInChildren<AudioListener>(true);
            audioListener.enabled = true;


            // Enable the local camera
            localCameraHandler.localCamera.enabled = true;

            // Detach camera if enabled
            localCameraHandler.transform.parent = null;

            // Enable UI for local player
            localUI.SetActive(true);

            RPC_SetNickName(GameManager.instance.playerNickName);

            Debug.Log("Spawned local player");
        }
        else
        {
            // Disable the local camera for remote players
            localCameraHandler.localCamera.enabled = false;

            // Disable UI for remote player
            localUI.SetActive(false);

            // Only 1 audio listner is allowed in the scene so disable remote players audio listner
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log($"{Time.time} Spawned remote player");  
        }

        // Set the Player as a player object
        Runner.SetPlayerObject(Object.InputAuthority, Object);

        // Make it easier to tell which player is which.
        transform.name = $"Player_{Object.Id}";
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject playerLeftNetworkObject))
            {
                if (playerLeftNetworkObject == Object)
                    Local.GetComponent<NetworkInGameMessages>().SendInGameRPCMessage(playerLeftNetworkObject.GetComponent<NetworkPlayer>().nickName.ToString(), "left");
            }

        }


        if (player == Object.InputAuthority)
            Runner.Despawn(Object);

    }
    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.nickName}");

        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log($"Nickname changed for player to {nickName} for player {gameObject.name}");

        playerNickNameTM.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {

        // Set "Player" as default name if nickname is null
        if (string.IsNullOrEmpty(nickName)) {
            nickName = "Player";
        }

        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;

        if (!isPublicJoinMessageSent)
        {
            networkInGameMessages.SendInGameRPCMessage(nickName, "joined");

            isPublicJoinMessageSent = true;
        }
    }

    void OnDestroy()
    {
        // Get rid of the local camera if we get destroyed as a new one will be spawned with the new Network player
        if (localCameraHandler != null)
            Destroy(localCameraHandler.gameObject);
    }
}

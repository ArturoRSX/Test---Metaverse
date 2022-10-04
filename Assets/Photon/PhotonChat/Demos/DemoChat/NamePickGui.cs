// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH"/>
// <summary>Demo code for Photon Chat in Unity.</summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------


using Metaverse;
using Metaverse.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Photon.Chat.Demo
{
    [RequireComponent(typeof(ChatGui))]
    public class NamePickGui : MonoBehaviour
    {
        private const string UserNamePlayerPref = "PlayerNickname";

        public ChatGui chatNewComponent;

        public TMP_InputField idInput;

        public void Start()
        {
            this.chatNewComponent = FindObjectOfType<ChatGui>();

            string prefsName = PlayerPrefs.GetString(UserNamePlayerPref);
            if (!string.IsNullOrEmpty(prefsName))
            {
                this.idInput.text = prefsName;
            }
        }


        // new UI will fire "EndEdit" event also when loosing focus. So check "enter" key and only then StartChat.
        public void EndEditOnEnter()
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                this.StartChat();
            }
        }

        public void StartChat()
        {
            // Trim name
            var idInputTrim = idInput.text.Trim ();

            // Save name
            PlayerPrefs.SetString ("PlayerNickname", idInputTrim);
            PlayerPrefs.Save ();

            // Set in Game Manager
            GameManager.instance.playerNickName = idInputTrim;

            ChatGui chatNewComponent = FindObjectOfType<ChatGui>();
            chatNewComponent.UserName = idInputTrim;
            chatNewComponent.Connect();

            NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler> ();
            networkRunnerHandler.StartNow ();

            this.enabled = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Metaverse.Game
{
    public class InGameMessagesUIHander : MonoBehaviour
    {
        public TextMeshProUGUI [] textMeshProUGUIs;

        Queue messageQueue = new Queue ();

        public void OnGameMessageReceived (string message)
        {
            Debug.Log ($"InGameMessagesUIHander {message}");

            messageQueue.Enqueue (message);

            if (messageQueue.Count > 3)
                messageQueue.Dequeue ();

            int queueIndex = 0;
            foreach (string messageInQueue in messageQueue) {
                textMeshProUGUIs [queueIndex].text = messageInQueue;
                queueIndex++;
            }

        }
    }
}

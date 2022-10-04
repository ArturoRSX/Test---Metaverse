using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Metaverse.Utilities
{
    /// <summary>
    /// Manages enable or disable the microphone.
    /// </summary>
    public class MicrophoneEnabledDisabled : MonoBehaviour
    {

        public Image image;

        /// <summary>
        /// Enables or Disables the microphone depending the current state
        /// </summary>
        public void EnableDisable ()
        {
            if (FindObjectOfType<Recorder> ().TransmitEnabled) {
                DisableMicrophone ();
            }
            else {
                EnableMicrophone ();
            }
        }

        /// <summary>
        /// Disables the microphone
        /// </summary>
        public void DisableMicrophone ()
        {
            image.sprite = Resources.Load<Sprite> ("Icons/mute-microphone");

            FindObjectOfType<Recorder> ().TransmitEnabled = false;
        }

        /// <summary>
        /// Enables the microphone
        /// </summary>
        public void EnableMicrophone ()
        {
            image.sprite = Resources.Load<Sprite> ("Icons/microphone");

            FindObjectOfType<Recorder> ().TransmitEnabled = true;
        }

    }
}

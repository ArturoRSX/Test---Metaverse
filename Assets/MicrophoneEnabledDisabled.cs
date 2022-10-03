using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneEnabledDisabled : MonoBehaviour
{

    public Image image;

    public void EnableDisable ()
    {
        if (FindObjectOfType<Recorder> ().TransmitEnabled) {
            DisableMicrophone ();
        } else {
            EnableMicrophone ();
        }
    }

    public void DisableMicrophone ()
    {
        image.sprite = Resources.Load<Sprite> ("Icons/mute-microphone");

        FindObjectOfType<Recorder> ().TransmitEnabled = false;
    }
    public void EnableMicrophone ()
    {
        image.sprite = Resources.Load<Sprite> ("Icons/microphone");

        FindObjectOfType<Recorder> ().TransmitEnabled = true;
    }

}

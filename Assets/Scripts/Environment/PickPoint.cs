using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Game
{
    public class PickPoint : MonoBehaviour
    {

        void OnTriggerEnter (Collider col)
        {
            if (col.CompareTag ("Player")) {
                if (col.GetComponent<NetworkPlayer> ().isLocal) {
                    Debug.Log ("Is the local player.");
                }
            }
        }
        void OnTriggerExit ()
        {

        }

    }
}

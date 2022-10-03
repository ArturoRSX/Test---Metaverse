using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPoint : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("LocalPlayer")) {
            col.GetComponentInParent<WeaponHandler> ().hasWeapon = false;
        }
    }
    void OnTriggerExit ()
    {

    }

}

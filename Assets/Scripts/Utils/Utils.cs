using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Utilities
{
    public static class Utils
    {
        public static Transform GetRandomSpawnPoint ()
        {
            // Get an array of the spawn points
            var spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");

            // Spawn randomly in one of the spawn points
            if (spawnPoints != null) {
                return spawnPoints [Random.Range (0, spawnPoints.Length - 1)].transform;
            }
            else {
                Debug.LogError ("No spawn points assigned in the world");
                return null;
            }
        }

        public static void SetRenderLayerInChildren (Transform transform, int layerNumber)
        {
            foreach (Transform trans in transform.GetComponentsInChildren<Transform> (true)) {
                if (trans.CompareTag ("IgnoreLayerChange"))
                    continue;

                trans.gameObject.layer = layerNumber;
            }
        }
    }
}

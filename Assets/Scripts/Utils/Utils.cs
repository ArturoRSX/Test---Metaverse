using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint()
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
        if (spawnPoints != null) {
            return spawnPoints [Random.Range (0, spawnPoints.Length - 1)].transform.position;
        }
        else {
            return new Vector3 (Random.Range (-20, 20), 4, Random.Range (-20, 20));
        }
    }

    public static void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
        {
            if (trans.CompareTag("IgnoreLayerChange"))
                continue;

            trans.gameObject.layer = layerNumber;
        }
    }
}

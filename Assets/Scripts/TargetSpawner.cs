using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnArea = new Vector3(3, 3, 3);

    [SerializeField]
    private int targetsToSpawn = 3;

    public static int TotalTargets;

    [SerializeField]
    private GameObject targetPrefab;



    // Update is called once per frame
    void Update()
    {
        if (TotalTargets == 0 && GameManager.GameStarted)
        {
            SpawnTargets();
        }

        if (!GameManager.GameStarted && TotalTargets > 0)
        {
            DestroyTragets();
        }
    }

    void SpawnTargets()
    {

        if (TotalTargets == 0 && GameManager.GameStarted)
        {
            float xMin = transform.position.x - spawnArea.x / 2;
            float yMin = transform.position.y - spawnArea.y / 2;
            float zMin = transform.position.z - spawnArea.z / 2;

            float xMax = transform.position.x + spawnArea.x / 2;
            float yMax = transform.position.y + spawnArea.y / 2;
            float zMax = transform.position.z + spawnArea.z / 2;

            for (int i = 0; i < targetsToSpawn; i++)
            {
                float xRandom = UnityEngine.Random.Range(xMin, xMax);
                float yRandom = UnityEngine.Random.Range(yMin, yMax);
                float zRandom = UnityEngine.Random.Range(zMin, zMax);

                TargetFactory.Instance.GetProduct(new Vector3(xRandom, yRandom, zRandom), TargetFactory.Instance.TargetPrefab.transform.rotation);
                TotalTargets++;
            }
        }
    }

    void DestroyTragets()
    {
        TotalTargets = 0;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < targets.Length; i++)
        {
            TargetBehaviour targetBehaviour = targets[i].GetComponent<TargetBehaviour>();
            targetBehaviour.CleanUp();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}

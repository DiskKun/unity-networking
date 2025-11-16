using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System;
using System.Collections.Generic;


public class GameManager : NetworkBehaviour
{
    public GameObject goalObjectPrefab;
    public GameObject goalObjectSpawnContainer;
    public NetworkManager nm;

    Transform[] goalObjectSpawnLocations;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalObjectSpawnLocations = goalObjectSpawnContainer.GetComponentsInChildren<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGoalObject()
    {
        if (nm.LocalClient.IsSessionOwner)
        {
            Debug.Log("Spawning...");
            NetworkSpawn(goalObjectPrefab, goalObjectSpawnLocations[UnityEngine.Random.Range(0, goalObjectSpawnLocations.Length - 1)]);
            StartCoroutine(WaitForSeconds(5, SpawnGoalObject));
        }
        
    }

    public static IEnumerator WaitForSeconds(float timeToWait, Action methodRunOnCompletion = null)
    {
        float t = 0;
        while (t < timeToWait)
        {
            t += Time.deltaTime;
            yield return null;
        }
        methodRunOnCompletion();
    }



    public void StartRound()
    {
        if (nm.LocalClient.IsSessionOwner)
        {
            SpawnGoalObject();
        }
    }

    public static GameObject NetworkSpawn(GameObject gameObject, Transform position)
    {
        
        // instantiates a prefab, and spawns it across the network
        var instance = Instantiate(gameObject, position.position, Quaternion.identity);
        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
        return instance;
    }
}

using UnityEngine;
using Unity;
using Unity.Netcode;
using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;


public class GameManager : NetworkBehaviour
{
    public GameObject goalObjectPrefab;
    public GameObject goalObjectSpawnContainer;
    public NetworkManager nm;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timerText;

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
            NetworkSpawn(goalObjectPrefab, goalObjectSpawnLocations[UnityEngine.Random.Range(0, goalObjectSpawnLocations.Length - 1)]);
            StartCoroutine(WaitForSeconds(5, SpawnGoalObject));
        }

    }

    public void PlayerWin(string playerName)
    {
        winText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        winText.text = "Player \"" + playerName + "\" wins!";

        StopAllCoroutines();
        StartCoroutine(WaitForSeconds(5, StartRound));
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
        winText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
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

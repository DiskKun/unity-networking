using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NetworkButtons : MonoBehaviour
{
    public TextMeshProUGUI roleText;
    public GameObject netBallPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartServer()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            roleText.text = "Started as server";
        }
    }
    public void StartClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            roleText.text = "Started as client";
        }
    }

    public void SpawnNetBall()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            var ball = Instantiate(netBallPrefab);
            var ballNetworkObject = ball.GetComponent<NetworkObject>();
            ballNetworkObject.Spawn();
        }
        
    }
}

using UnityEngine;
using Unity.Netcode;
public class NetworkButtons : MonoBehaviour
{
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
        NetworkManager.Singleton.StartServer();
    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}

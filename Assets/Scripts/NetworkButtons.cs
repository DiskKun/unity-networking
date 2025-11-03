using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NetworkButtons : MonoBehaviour
{
    public TextMeshProUGUI roleText;
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
}

using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{

    public GameObject clientBallPrefab;


    private void Update()
    {
        if (IsClient)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ServerOnlyRpc();
            }
        }
        
    }

    //[Rpc(SendTo.ClientsAndHost)]
    //void ClientAndHostRpc(int value, ulong sourceNetworkObjectId)
    //{
    //    //Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
    //    //if (IsOwner) //Only send an RPC to the owner of the NetworkObject
    //    //{
    //    //    ServerOnlyRpc(value + 1, sourceNetworkObjectId);
    //    //}
    //}

    [Rpc(SendTo.Server)]
    void ServerOnlyRpc()
    {
        var ball = Instantiate(clientBallPrefab);
        var ballNetworkObject = ball.GetComponent<NetworkObject>();
        ballNetworkObject.Spawn();
    }

}

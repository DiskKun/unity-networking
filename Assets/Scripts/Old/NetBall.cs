using UnityEngine;
using Unity.Netcode;

public class NetBall : NetworkBehaviour
{
    Rigidbody2D rb;
    Vector2 targetPosition;
    Vector2 startingPosition;
    float moveTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            if (moveTime >= 1)
            {
                targetPosition = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
                //Debug.Log("target position: " + targetPosition);
                startingPosition = rb.position;
                moveTime = 0;
            }
        
        
            moveTime += Time.deltaTime;
            rb.MovePosition(Vector2.Lerp(startingPosition, targetPosition, moveTime));
            //Debug.Log("move: " + Vector2.Lerp(startingPosition, targetPosition, moveTime));
        }
        
    }
}

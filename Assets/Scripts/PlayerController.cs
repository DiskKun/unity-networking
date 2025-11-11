using UnityEngine;
using Unity.Cinemachine;
using Unity.Netcode;
using Unity.Networking;
using Unity.Services.Multiplayer;
public class PlayerController : NetworkBehaviour
{


    public float acceleration;
    public float maxHorizontalSpeed;
    public float minJumpHeight;
    public float maxJumpTime;

    float jumpTimer = 0;
    bool jumping = false;
    Vector2 direction;
    Rigidbody2D rb;
    CinemachineCamera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        cam = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
        rb = GetComponent<Rigidbody2D>();
        if (IsOwner)
        {
            cam.Target.TrackingTarget = transform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // walking
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 0.51f, Vector2.down, 0.1f);
        bool onGround = hit;
        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jumping = true;
        }
        if (Input.GetKey(KeyCode.Space) && jumpTimer < maxJumpTime && jumping)
        {
            jumpTimer += Time.deltaTime;
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimer = 0;
        }
        if (jumpTimer >= maxJumpTime || jumpTimer == 0)
        {
            jumping = false;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * Time.deltaTime * acceleration);
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -maxHorizontalSpeed, maxHorizontalSpeed);
        if (jumping)
        {
            rb.linearVelocityY = minJumpHeight;

        }
    }
}

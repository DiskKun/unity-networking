using UnityEngine;
using Unity.Cinemachine;
using Unity.Netcode;
using TMPro;

public class PlayerController : NetworkBehaviour
{


    public float acceleration;
    public float maxHorizontalSpeed;
    public float minJumpHeight;
    public float maxJumpTime;

    public TextMeshPro playerNameTextMesh;


    float jumpTimer = 0;
    bool jumping = false;
    Vector2 direction;
    Rigidbody2D rb;
    CinemachineCamera cam;
    GameManager gm;
    ConnectionManager cm;

    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (IsOwner)
        {
            score = 0;

            rb = GetComponent<Rigidbody2D>();

            // Since start is called whenever a new player joins, ensure that this object is the owner before setting the camera target
            // else the camera will be set to this player for all connected players
            cam = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
            cm = GameObject.Find("NetworkManager").GetComponent<ConnectionManager>();
            SetNameTextRpc(cm._profileName);

            //playerNameTextMesh.text = cm._profileName;

            cam.Target.TrackingTarget = transform;

            if (IsSessionOwner)
            {
                gm.StartRound();
            }
        }



    }



    [Rpc(SendTo.Everyone)]
    void SetNameTextRpc(string name)
    {
        playerNameTextMesh.text = name;
    }

    [Rpc(SendTo.Everyone)]
    void PlayerWinRpc(string playerName)
    {
        gm.PlayerWin(playerName);
        score = 0;
    }



    // Update is called once per frame
    void Update()
    {
        Movement();
        if (IsOwner)
        {
            SetNameTextRpc(cm._profileName + " " + score + " coins");
        }
    }

    void CheckScore()
    {
        if (score >= 5)
        {
            PlayerWinRpc(cm._profileName);
        }
    }

    void Movement()
    {
        if (IsOwner)
        {
            // walking
            direction = new Vector2(Input.GetAxis("Horizontal"), 0);

            // jumping
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 0.51f, Vector2.down, 0.1f);
            bool onGround = hit;
            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                // inital jump key event
                jumping = true;
            }
            if (Input.GetKey(KeyCode.Space) && jumpTimer < maxJumpTime && jumping)
            {
                // continued presses ticks up the jumptimer
                jumpTimer += Time.deltaTime;

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // drop the jumptimer when the key is lifted
                jumpTimer = 0;
            }
            if (jumpTimer >= maxJumpTime || jumpTimer == 0)
            {
                // quit jumping if the jumptimer is dropped or if it has exceeded the max jump time
                jumping = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            if (IsOwner)
            {
                score += 1;
                CheckScore();
            }
        }

    }

    

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            // horizontal movement
            rb.AddForce(direction * Time.deltaTime * acceleration);
            rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -maxHorizontalSpeed, maxHorizontalSpeed);

            // jumping
            if (jumping)
            {
                rb.linearVelocityY = minJumpHeight;

            }
        }

    }
}

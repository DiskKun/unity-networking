using UnityEngine;
using Unity.Cinemachine;
public class PlayerController : MonoBehaviour
{


    public float speed;
    public float jumpHeight;

    Vector2 direction;
    Rigidbody2D rb;
    CinemachineCamera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
        rb = GetComponent<Rigidbody2D>();
        cam.Target.TrackingTarget = transform;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            rb.linearVelocityY = jumpHeight;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * Time.deltaTime * speed);
    }
}

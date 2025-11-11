using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 direction;
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

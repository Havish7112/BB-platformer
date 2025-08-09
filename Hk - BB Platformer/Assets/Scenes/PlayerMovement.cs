using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float jumpAmount = 10;
    public float speed =10;
    bool touchingGround = true;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.Translate(velocity, 0, 0);
        
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        if (hit.collider != null)
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
    }
}

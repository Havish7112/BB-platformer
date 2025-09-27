using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float jumpAmount = 10;
    public float speed = 15;
    bool touchingGround = true;
    public LayerMask groundLayer;
    public GameObject currentSpawnpoint;
    public bool doubleJump = false;
    public bool usedDoubleJump = false;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float velocity = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.Translate(velocity, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && (touchingGround || (doubleJump && !usedDoubleJump)) )
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            usedDoubleJump = true;
        }

        if (touchingGround)
        {
            usedDoubleJump = false;
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

    public void Respawn()
    {
        gameObject.transform.position = currentSpawnpoint.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            doubleJump = true;
        }

        if (collision.gameObject.tag == "Flag")
        {
            SceneManager.LoadScene("End Screen");
        }
    }


}

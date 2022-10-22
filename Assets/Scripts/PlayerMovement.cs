using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerBody;
    public float speed;
    bool jumping; // this is so it doesn't interfere if the player is in the air because of sword
    bool jumpReady; // so you can only jump once even after using a sword jump or after clinging onto a wall
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        keyInputs();
    }
    void keyInputs()
    {
        // Vertical
        // Variable Height jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpReady)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, speed*1.5f);
            jumping = true;
            jumpReady = false;
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && playerBody.velocity.y > 0 && jumping)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
            jumping = false;
        }
        // Horizontal
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            // To not change anything when both directions are pressed
        } else 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerBody.velocity = new Vector2(-speed / 1.15f, playerBody.velocity.y);
        } else 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerBody.velocity = new Vector2(speed / 1.15f, playerBody.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Environment"))
        {
            jumpReady = true;
        }
    }
}

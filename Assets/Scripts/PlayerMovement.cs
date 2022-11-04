using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerBody;
    Animator anim;
    CapsuleCollider2D playerCollider;
    [SerializeField] LayerMask groundLayer; // specification of ground using layers
    [SerializeField] LayerMask wallLayer; // specification of wall using layers
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    string flip = "right"; // to determine which way the sprite is facing
    float gravity; // default value of gravity
    float inputCooldown = 0f; // to disable inputs for a millisecond after walljumping
    void Start()
    {
        // Setting the player's attributes into accessible variables
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        gravity = playerBody.gravityScale;
    }
    void Update()
    {
        // sets default animation to idle
        anim.SetBool("run", false);
        if (onWall() && !isGrounded())
        {
            playerBody.gravityScale = 0;
            playerBody.velocity = Vector2.zero;
        } else
        {
            playerBody.gravityScale = gravity;
        }
        keyInputs();
        inputCooldown += Time.deltaTime;
        flipping();
    }
    void keyInputs()
    {
        // Vertical
        // Variable Height jump
        // Key down
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Jump();
        }
        anim.SetBool("grounded", isGrounded());

        // Key up
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && playerBody.velocity.y > 0)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
        }

        // Horizontal
        if (inputCooldown > .15f)
        {
            if (((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                // To not change anything when both directions are pressed\
                playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            }
            else
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("run", true);
                playerBody.velocity = new Vector2(-speed, playerBody.velocity.y);
                flip = "left";
            }
            else
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("run", true);
                playerBody.velocity = new Vector2(speed, playerBody.velocity.y);
                flip = "right";
            }
        }

    }
    private void flipping()
    {
        // flipping sprite
        if (flip.Equals("right"))
        {
            transform.localScale = new Vector3(2, 2, 2);
        } else
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }
    private void Jump()
    {
        if (isGrounded())
        {
            playerBody.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("jump");
        } else if(onWall() && !isGrounded())
        {
            if (flip.Equals("left"))
            {
                flip = "right";
                playerBody.AddForce(new Vector2(jumpForce/2, jumpForce));
            }
            else if (flip.Equals("right"))
            {
                flip = "left";
                playerBody.AddForce(new Vector2(-jumpForce/2, jumpForce));
            }
            anim.SetTrigger("jump");
            inputCooldown = 0;

        }
    }
    private bool isGrounded()
    {
        RaycastHit2D aboveGround = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return aboveGround.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D besideWall = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return besideWall.collider != null;
    }
}

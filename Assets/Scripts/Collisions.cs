using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerBody;
    float mouseForce = 400;
    float maxForce = 900;
    float XVelocity;
    float YVelocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // mouse drag velocity and negative direction into force
        XVelocity = -Input.GetAxis("Mouse X") * mouseForce;
        YVelocity = -Input.GetAxis("Mouse Y") * mouseForce* 1.25f;
        // force limit
        if(XVelocity > maxForce) { XVelocity = maxForce; }
        if (XVelocity < -maxForce) { XVelocity = -maxForce; }
        if (YVelocity > maxForce) { YVelocity = maxForce; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            // adding force to the player's rigidbody
            playerBody.AddForce(new Vector2(XVelocity, YVelocity));
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // ignore player to weapon rigidbody collision
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            //player.transform.position = (((player.transform.position + transform.position) / 2) - (transform.position / 4));
            //playerBody.AddForce(new Vector2(0f, 1f), ForceMode2D.Impulse);
        }
        
    }
}

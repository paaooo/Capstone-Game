using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerBody;
    Animator anim;
    [SerializeField] float mouseForce;
    [SerializeField] float maxForce;
    float XVelocity;
    float YVelocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // mouse drag velocity and negative direction into force
        XVelocity = -Input.GetAxis("Mouse X") * mouseForce;
        YVelocity = -Input.GetAxis("Mouse Y") * mouseForce;
        // force limit
        if (XVelocity > maxForce/1.5) { XVelocity = maxForce/ 1.5f; }
        if (XVelocity < -maxForce/ 1.5) { XVelocity = -maxForce/ 1.5f; }
        if (YVelocity > maxForce) { YVelocity = maxForce; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            // adding force to the player's rigidbody
            playerBody.AddForce(new Vector2(XVelocity, YVelocity));
            anim.SetTrigger("jump");
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // ignore player to weapon rigidbody collision
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}

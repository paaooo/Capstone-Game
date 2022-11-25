using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerBody;
    Animator anim;
    [SerializeField] float mouseForce;
    [SerializeField] float maxVelocity;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip hitSound2;
    [SerializeField] AudioClip hitSound3;
    [SerializeField] float soundForce;
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
        YVelocity = -Input.GetAxis("Mouse Y") * mouseForce * 1.75f;
        if (YVelocity < 0) { YVelocity = 0; } // To prevent going down when you pull mouse back up

        // velocity limit
        if (playerBody.velocity.x > maxVelocity) { playerBody.velocity = new Vector2(maxVelocity, playerBody.velocity.y); }
        if (playerBody.velocity.x < -maxVelocity) { playerBody.velocity = new Vector2(-maxVelocity, playerBody.velocity.y); }
        if (playerBody.velocity.y > maxVelocity * 1.25f) { playerBody.velocity = new Vector2(playerBody.velocity.x, maxVelocity * 1.25f); }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            // adding force to the player's rigidbody
            playerBody.AddForce(new Vector2(XVelocity, YVelocity));
            if (XVelocity + YVelocity > soundForce || XVelocity + YVelocity < -soundForce)
            {
                float rand = (XVelocity + YVelocity) % 3;
                SoundManager.instance.PlaySound(RandomHitSound());
            }
            anim.SetTrigger("jump");
        }
        AudioClip RandomHitSound()
        {
            int rand = Mathf.FloorToInt(Random.Range(0, 3));
            if(rand == 0)
            {
                return hitSound;
            }
            if(rand == 1)
            {
                return hitSound2;
            }
                return hitSound3;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // ignore player to weapon rigidbody collision
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}

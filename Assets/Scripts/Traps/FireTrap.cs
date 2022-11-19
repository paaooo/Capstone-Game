using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("FireTrap Timers")]
    [SerializeField] float activationDelay;
    [SerializeField] float activationTime;
    Animator anim;
    GameObject player;

    bool triggered;
    bool active;
    bool touching;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(active && touching)
        {
            player.transform.position = Vector3.zero; // Resets player position at the start when it hits a trap
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            touching = true;
            if(!triggered)
            {
                anim.SetTrigger("trigger");
                StartCoroutine(ActivateFireTrap());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            touching = false;
        }
    }
    IEnumerator ActivateFireTrap()
    {
        // Trigger the firetrap but not activating it yet
        triggered = true;

        // Activate the firetrap
        yield return new WaitForSeconds(activationDelay);
        active = true;
        anim.SetBool("activated", active);

        // Disable the firetrap
        yield return new WaitForSeconds(activationTime);
        active = false;
        anim.SetBool("activated", active);
        triggered = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.position = Vector3.zero; // Resets player position at the start when it hits a trap
        }
    }
}

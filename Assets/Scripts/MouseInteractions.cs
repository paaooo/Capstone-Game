using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class MouseInteractions : MonoBehaviour
{
    Transform player;
    public float maxPos;
    public float thrustPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // rotation
        // fixes the center being stuck in the middle point of the world
        // also makes it rotate from the player instead of the sword for a better feel
        Vector3 centerRotate = mousePos - player.position;

        float angle = Mathf.Atan2(centerRotate.y, centerRotate.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // position + revolving around player
        mousePos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePos;

        Vector3 playerPos = player.position;
        Vector3 maxPosition = mousePos; // modifying this later to be close to player position
        // limits distance from player
        if (mousePos[0] > playerPos.x + thrust()) // max X positive
        {
            maxPosition[0] = playerPos.x + thrust();
            transform.position = maxPosition;
        }
        if (mousePos[0] < playerPos.x - thrust()) // max X negative
        {
            maxPosition[0] = playerPos.x - thrust();
            transform.position = maxPosition;
        }
        if (mousePos[1] > playerPos.y + thrust()) // max Y positive
        {
            maxPosition[1] = playerPos.y + thrust();
            transform.position = maxPosition;
        }
        if (mousePos[1] < playerPos.y - thrust()) // max Y negative
        {
            maxPosition[1] = playerPos.y - thrust();
            transform.position = maxPosition;
        }
    }
    // Thrust function asks if mouse button 1 is held down
    // If it is, maxPosition of weapon from player is increased
    float thrust()
    {
        if(Input.GetMouseButton(0))
        {
            return thrustPos;
        }
        return maxPos;
    }
}

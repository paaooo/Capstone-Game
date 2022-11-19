using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    [SerializeField] float zoomOut;
    float normalZoom;
    private void Start()
    {
        normalZoom = Camera.main.orthographicSize;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 3, -5);
        if(ZoomOut())
        {
            Camera.main.orthographicSize = zoomOut;
        } else
        {
            Camera.main.orthographicSize = normalZoom;
        }
    }
    bool ZoomOut()
    {
        if(Input.GetMouseButton(0))
        {
            return true;
        }
        return false;
    }
}

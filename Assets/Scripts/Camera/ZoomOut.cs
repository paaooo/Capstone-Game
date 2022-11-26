using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] float ZoomValue;
    float normalZoom;
    // Start is called before the first frame update
    void Start()
    {
        normalZoom = Camera.main.orthographicSize;
    }
    // Update is called once per frame
    void Update()
    {
        if (Zoom())
        {
            Camera.main.orthographicSize = ZoomValue;
        }
        else
        {
            Camera.main.orthographicSize = normalZoom;
        }
    }
    bool Zoom()
    {
        if (Input.GetMouseButton(0))
        {
            return true;
        }
        return false;
    }
}

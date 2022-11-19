using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] float moveDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] bool vertical;
    bool movingToFirst;
    float firstEdge;
    float secondEdge;
    float scale;
    // Start is called before the first frame update
    void Start()
    {
        if (vertical)
        {
            firstEdge = transform.position.y - moveDistance;
            secondEdge = transform.position.y + moveDistance;
        }
        else
        {
            firstEdge = transform.position.x - moveDistance;
            secondEdge = transform.position.x + moveDistance;
        }
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingToFirst)
        {
            if((transform.position.x > firstEdge && !vertical) || (transform.position.y > firstEdge && vertical))
            {
                transform.localScale = new Vector3(scale, scale, scale);
                if (vertical)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
                } else
                {
                    transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
            } else
            {
                movingToFirst = false;
            }
        } else
        {
            if((transform.position.x < secondEdge && !vertical) || (transform.position.y < secondEdge && vertical))
            {
                transform.localScale = new Vector3(-scale, scale, scale);
                if (vertical)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
            } else
            {
                movingToFirst = true;
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatrolling : MonoBehaviour
{
    public Transform[] points;
    int curr;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        curr = 0;  
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != points[curr].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[curr].position, speed * Time.deltaTime);
        }
        else
        {
            curr = (curr+1)%points.Length;
        }
    }
}

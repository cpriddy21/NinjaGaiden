using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    float speed;
    public Vector3 pointA;
    public Vector3 pointB;

    void Start()
    {
        double dSpeed = 1/((pointB.x - pointA.x) * .64);
        speed = (float)dSpeed;
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
        if (Vector3.Distance(pointA, transform.position)==0)
        {
            transform.Rotate(new Vector3(0, 0, 0));
            //Debug.Log("Hit A");
        }
        if(Vector3.Distance(pointB, transform.position) == 0)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            //Debug.Log("Hit B");
        }
    }
}
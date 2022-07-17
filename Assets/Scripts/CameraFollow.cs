using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset;
    private Vector3 v = Vector3.zero;
    public Transform target;

    void FixedUpdate()
    {
        if(target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref v, Time.fixedDeltaTime);
            //transform.position = position;
        }
    }
}

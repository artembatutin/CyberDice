using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        target = FindObjectOfType<Target>().gameObject;
    }
    void FixedUpdate()
    {
        if(transform.position.x > target.transform.position.x)
        {
            transform.position -= new Vector3(0.1f, 0);
        }
        if(transform.position.x < target.transform.position.x)
        {
            transform.position += new Vector3(0.1f, 0);
        }
        if(transform.position.y > target.transform.position.y)
        {
            transform.position -= new Vector3(0, 0.1f);
        }
        if(transform.position.y < target.transform.position.y)
        {
            transform.position += new Vector3(0, 0.1f);
        }
    }
}

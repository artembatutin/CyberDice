using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(FindObjectOfType<CompositeCollider2D>().transform); 
    }
}

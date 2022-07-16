using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.eulerAngles = transform.parent.eulerAngles;
        Debug.Log(transform.parent.eulerAngles);
    }
}

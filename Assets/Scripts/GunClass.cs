using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunClass : MonoBehaviour
{
    public string name_;
    public GameObject bullet;
    public GameObject firepoint;

    public void Shoot(float speed)
    {         
        GameObject bulletObject = Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
        Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
        bulletObject.name = name_;
        rb.AddForce(firepoint.transform.up * speed, ForceMode2D.Impulse);        
    }
}

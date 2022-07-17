using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunClass : MonoBehaviour
{
    public string name_;
    public GameObject bullet;

    private AudioSource source = null;

    public void Shoot(Transform parent, float speed, int dimension)
    {
        GameObject bulletObject = Instantiate(bullet, parent.position, parent.rotation);
        Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
        //rb.AddRelativeForce(Vector3.forward * speed * 100, ForceMode2D.Impulse);
        //rb.AddForce(parent.transform.up * speed, ForceMode2D.Impulse);
        rb.AddRelativeForce(Vector3.up * speed, ForceMode2D.Impulse);

        if(!source)
        {
            source = parent.GetComponent<AudioSource>();
        }
        if(!source.isPlaying)
        {
            source.PlayOneShot(Dice.instance.shootClip);
        }

        Bullet destroy = bulletObject.GetComponent<Bullet>();
        destroy.setDimension(dimension);
    }
}
